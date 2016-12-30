using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using SimpleController.Domain;
using Windows.Devices.Gpio;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;
using Windows.System.Profile;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
using Windows.Security.Cryptography;
using Windows.Networking.Connectivity;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Text;
using Windows.Foundation.Metadata;
using Microsoft.IoT.Lightning.Providers;
using Windows.Devices;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SimpleController
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private IWheels mWheels;
        private DeviceClient mDeviceClient;
        private ObstacleSensors mObstacleSensors;
        private PanTiltServo mPanTiltServo;
        private Camera mCamera;

        public MainPage()
        {
            if (LightningProvider.IsLightningEnabled)
            {
                LowLevelDevicesController.DefaultProvider = LightningProvider.GetAggregateProvider();
            }

            this.InitializeComponent();
            mDeviceClient = DeviceClient.Create(App.IOTHUB_URI, new DeviceAuthenticationWithRegistrySymmetricKey(GetUniqueDeviceId(), App.DEVICE_KEY), TransportType.Amqp);
            registerKeyEvents();

            var hasGpio = GpioController.GetDefault() != null;
            if (hasGpio)
            {
                mWheels = new Wheels();
                mObstacleSensors = new ObstacleSensors();
                mObstacleSensors.ValueReadingChanged += MObstacleSensors_ValueReading;
                mPanTiltServo = new PanTiltServo();
            }
            else
            {
                mWheels = new FakeWheels();
                mPanTiltServo = new PanTiltServo();
            }

            mCamera = new Camera();
            mCamera.Init();
            mWheels.Init();
            NetworkInformation.NetworkStatusChanged += NetworkInformation_NetworkStatusChanged;
            Task.Run(() =>
            {
                receiveC2dAsync();
            });
        }

        private async void MObstacleSensors_ValueReading(object sender, ObstacleSensors.SensorReading e)
        {
            try
            {
                if (e.LeftValue == GpioPinValue.High && e.RightValue == GpioPinValue.High)
                {
                    if (mWheels.IsMovingForward)
                    {
                        mWheels.Stop();
                    }
                }
                var data = new
                {
                    deviceId = MainPage.GetUniqueDeviceId(),
                    source = "Obstacle Sensor",
                    leftValue = e.LeftValue,
                    rightValue = e.RightValue,
                };

                var messageString = JsonConvert.SerializeObject(data);
                var message = new Message(Encoding.ASCII.GetBytes(messageString));
                await mDeviceClient.SendEventAsync(message);
            }
            catch (Exception ex)
            {
            }
        }

        private void NetworkInformation_NetworkStatusChanged(object sender)
        {
            var internetprofile = NetworkInformation.GetInternetConnectionProfile();
            if (internetprofile == null)
            {
                mWheels.Stop();
            }
        }

        private void registerKeyEvents()
        {
            //http://stackoverflow.com/questions/32781864/get-keyboard-state-in-universal-windows-apps
            Window.Current.CoreWindow.KeyDown += (s, e) =>
            {
                if (e.VirtualKey == VirtualKey.W)
                {
                    mWheels.MoveForward();
                }
                if (e.VirtualKey == VirtualKey.S)
                {
                    mWheels.Stop();
                }
                if (e.VirtualKey == VirtualKey.X)
                {
                    mWheels.MoveBackwards();
                }
                if (e.VirtualKey == VirtualKey.A)
                {
                    mWheels.TurnLeft();
                }
                if (e.VirtualKey == VirtualKey.D)
                {
                    mWheels.TurnRight();
                }
                if (e.VirtualKey == VirtualKey.R)
                {
                    mObstacleSensors?.ReadCurrentValues();
                }
                if (e.VirtualKey == VirtualKey.T)
                {
                    mPanTiltServo?.TiltUp();
                }
                if (e.VirtualKey == VirtualKey.G)
                {
                    mPanTiltServo?.TiltUp();
                }
                if (e.VirtualKey == VirtualKey.V)
                {
                    mPanTiltServo?.PanLeft();
                }
                if (e.VirtualKey == VirtualKey.B)
                {
                    mPanTiltServo?.PanRight();
                }
                if (e.VirtualKey == VirtualKey.M)
                {
                    mCamera?.StartCapture();
                }
                if (e.VirtualKey == VirtualKey.N)
                {
                    mCamera?.StopCapture();
                }
            };
        }

        internal static string GetUniqueDeviceId()
        {
            var deviceInformation = new Windows.Security.ExchangeActiveSyncProvisioning.EasClientDeviceInformation();
            return deviceInformation.Id.ToString();
        }

        private async void receiveC2dAsync()
        {
            while (true)
            {

                try
                {
                    Message receivedMessage = await mDeviceClient.ReceiveAsync();
                    if (receivedMessage == null) continue;
                    if (receivedMessage.Properties["Created"] != null)
                    {
                        try
                        {
                            var rawvalue = receivedMessage.Properties["Created"];
                            var createdDateTime = new DateTime(long.Parse(rawvalue));
                            var now = DateTime.UtcNow;
                            var difference = now - createdDateTime;
                            if (difference > new TimeSpan(0, 0, 0, 0, 500))
                            {
                                await mDeviceClient.RejectAsync(receivedMessage);
                                continue;
                            }
                        }
                        catch (Exception)
                        {
                            await mDeviceClient.RejectAsync(receivedMessage);
                            continue;
                        }
                    }
                    if (receivedMessage.Properties["Action"] == "Forward")
                    {
                        mWheels.MoveForward();
                    }
                    if (receivedMessage.Properties["Action"] == "Backward")
                    {
                        mWheels.MoveBackwards();
                    }
                    if (receivedMessage.Properties["Action"] == "Stop")
                    {
                        mWheels.Stop();
                    }
                    if (receivedMessage.Properties["Action"] == "Left")
                    {
                        mWheels.TurnLeft();
                    }
                    if (receivedMessage.Properties["Action"] == "Right")
                    {
                        mWheels.TurnRight();
                    }
                    if (receivedMessage.Properties["Action"] == "SERVO_TILT")
                    {
                        var value = double.Parse(receivedMessage.Properties["Value"], System.Globalization.CultureInfo.InvariantCulture);
                        mPanTiltServo.TiltToPosition(value);
                    }
                    if (receivedMessage.Properties["Action"] == "SERVO_CENTER")
                    {
                        mPanTiltServo.Center();
                    }
                    if (receivedMessage.Properties["Action"] == "SERVO_PAN")
                    {
                        var value = double.Parse(receivedMessage.Properties["Value"], System.Globalization.CultureInfo.InvariantCulture);
                        mPanTiltServo.PanToPosition(value);
                    }
                    if (receivedMessage.Properties["Action"] == "FEED_START")
                    {
                        mCamera?.StartCapture();
                    }
                    if (receivedMessage.Properties["Action"] == "FEED_STOP")
                    {
                        mCamera?.StopCapture();
                    }
                    await mDeviceClient.CompleteAsync(receivedMessage);
                }
                catch (Exception ex)
                {
                }

            }
        }
    }
}
