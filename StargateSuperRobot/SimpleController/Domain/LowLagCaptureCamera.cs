using Microsoft.Azure.Devices.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage.Streams;

namespace SimpleController.Domain
{
    public class LowLagCaptureCamera
    {
        public class PutRequest
        {
            public string Image { get; set; }
        }
        private DeviceClient mDeviceClient;
        private CancellationTokenSource mCancelaationToken;
        private LowLagPhotoCapture mLowLagCapture;
        private MediaCapture mMediaCapture;
        private DateTime mStartTime;
        private TimeSpan mAutoStopAfter = new TimeSpan(0, 5, 0);

        public LowLagCaptureCamera()
        {
            mDeviceClient = DeviceClient.Create(App.IOTHUB_URI, new DeviceAuthenticationWithRegistrySymmetricKey(MainPage.GetUniqueDeviceId(), App.DEVICE_KEY));
            mMediaCapture = new MediaCapture();
        }

        public async Task Init()
        {
            var initTask = Task.Run(async () =>
            {
                try
                {
                    await mMediaCapture.InitializeAsync();
                    mLowLagCapture = await mMediaCapture.PrepareLowLagPhotoCaptureAsync(ImageEncodingProperties.CreateJpeg());
                }
                catch (Exception ex)
                {

                    throw;
                }

            });
        }

        public async Task StartCapture()
        {
            mCancelaationToken?.Cancel();
            await Init();
            mCancelaationToken = new CancellationTokenSource();
            mStartTime = DateTime.UtcNow;

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed - yeah i know :-P
            Task.Run(async () =>
            {
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
                            SendData(bytes);
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
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }

        private static async Task SendData(byte[] bytes)
        {
            try
            {
                HttpClient http = new HttpClient();
                var response = await http.PutAsJsonAsync<PutRequest>(new Uri("http://{yoursite}.azurewebsites.net/api/imagedata/" + MainPage.GetUniqueDeviceId()), new PutRequest
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
        }
    }
}
