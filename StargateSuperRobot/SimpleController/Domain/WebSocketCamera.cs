using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace SimpleController.Domain
{
    public class WebSocketCamera
    {
        public class PutRequest
        {
            public string Image { get; set; }
        }

        private CancellationTokenSource mCancelaationToken;
        private LowLagPhotoCapture mLowLagCapture;
        private MediaCapture mMediaCapture;
        private DateTime mStartTime;
        private TimeSpan mAutoStopAfter = new TimeSpan(0, 5, 0);
        private StreamWebSocket mStreamWebSocket;

        private async Task init()
        {
            try
            {
                mMediaCapture = new MediaCapture();
                await mMediaCapture.InitializeAsync();
                mLowLagCapture = await mMediaCapture.PrepareLowLagPhotoCaptureAsync(ImageEncodingProperties.CreateJpeg());
                mStreamWebSocket = new StreamWebSocket();
                mStreamWebSocket.Closed += MStreamWebSocket_Closed;
            }
            catch (Exception ex)
            {
                AppInsights.Client.TrackException(ex);
            }
        }

        private void MStreamWebSocket_Closed(IWebSocket sender, WebSocketClosedEventArgs args)
        {
            closeSocket(sender);
        }

        public async Task StartCapture()
        {
            mCancelaationToken?.Cancel();
            if (mStreamWebSocket != null)
            {
                closeSocket(mStreamWebSocket);
            }

            await init();
            mCancelaationToken = new CancellationTokenSource();
            mStartTime = DateTime.UtcNow;

            try
            {
                await mStreamWebSocket.ConnectAsync(new Uri($"{Globals.WEBSOCKET_ENDPOINT}?device={MainPage.GetUniqueDeviceId()}"));

                var task = Task.Run(async () =>
                {
                    var socket = mStreamWebSocket;
                    while (!mCancelaationToken.IsCancellationRequested)
                    {
                        try
                        {
                            var capturedPhoto = await mLowLagCapture.CaptureAsync();
                            using (var rac = capturedPhoto.Frame.CloneStream())
                            {
                                var dr = new DataReader(rac.GetInputStreamAt(0));
                                var bytes = new byte[rac.Size];
                                await dr.LoadAsync((uint)rac.Size);
                                dr.ReadBytes(bytes);

                                await socket.OutputStream.WriteAsync(bytes.AsBuffer());
                            }
                        }
                        catch (Exception ex)
                        {
                            AppInsights.Client.TrackException(ex);
                        }

                        if ((DateTime.UtcNow - mStartTime) > mAutoStopAfter)
                        {
                            AppInsights.Client.TrackEvent("CameraAutoTurnOff");
                            mCancelaationToken.Cancel();
                        }
                    }
                }, mCancelaationToken.Token);
            }
            catch (Exception ex)
            {
                mStreamWebSocket.Dispose();
                mStreamWebSocket = null;
                AppInsights.Client.TrackException(ex);
            }

        }

        private void closeSocket(IWebSocket webSocket)
        {
            try
            {
                webSocket.Close(1000, "Closed due to user request.");
            }
            catch (Exception ex)
            {
                AppInsights.Client.TrackException(ex);
            }
        }

        private static async Task sendData(byte[] bytes)
        {
            try
            {
                HttpClient http = new HttpClient();
                var response = await http.PutAsJsonAsync<PutRequest>(new Uri("http://sgnexus.azurewebsites.net/api/imagedata/" + "f56bccc1-4075-d40f-7d0d-34c16a1411e0"), new PutRequest
                {
                    Image = Convert.ToBase64String(bytes)
                });

                var code = response.StatusCode;
                AppInsights.Client.TrackEvent("CameraDataSent", new Dictionary<string, string> { { "responsecode", code.ToString() } });
            }
            catch (Exception ex)
            {
                AppInsights.Client.TrackException(ex);
            }
        }

        public async Task StopCapture()
        {
            mCancelaationToken.Cancel();
            mCancelaationToken = null;
            mStreamWebSocket = null;
        }
    }
}