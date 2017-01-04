using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Imaging;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;

namespace SimpleController.Domain
{
    public class IoTHubCamera
    {
        private DeviceClient mDeviceClient;
        private CancellationTokenSource mCancelaationToken;
        private LowLagPhotoCapture mLowLagCapture;
        private MediaCapture mMediaCapture;
        private DateTime mStartTime;
        private TimeSpan mAutoStopAfter = new TimeSpan(0, 5, 0);

        public IoTHubCamera()
        {
            mDeviceClient = DeviceClient.Create(App.IOTHUB_URI, new DeviceAuthenticationWithRegistrySymmetricKey(MainPage.GetUniqueDeviceId(), App.DEVICE_KEY));
            mMediaCapture = new MediaCapture();
        }

        public async Task Init()
        {
            var initTask = Task.Run(async () =>
            {
                await mMediaCapture.InitializeAsync();
                mLowLagCapture = await mMediaCapture.PrepareLowLagPhotoCaptureAsync(ImageEncodingProperties.CreateJpeg());
            });
        }

        public async Task StartCapture()
        {
            mCancelaationToken?.Cancel();
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
                            var message = new Message(bytes);
                            message.Properties["path"] = "imagefeed";
                            await mDeviceClient.SendEventAsync(message);
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    if ((DateTime.UtcNow - mStartTime) > mAutoStopAfter)
                    {
                        mCancelaationToken.Cancel();
                    }
                }
            }, mCancelaationToken.Token);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }

        public async Task StopCapture()
        {
            mCancelaationToken.Cancel();
            mCancelaationToken = null;
        }
    }
}
