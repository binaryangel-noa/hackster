using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleController.Domain
{
    internal class FakeWheels : IWheels
    {
        private static DeviceClient mDeviceClient;

        private static Queue<string> pendingMessagesQueue = new Queue<string>();

        public FakeWheels()
        {
            mDeviceClient = DeviceClient.Create(App.IOTHUB_URI, new DeviceAuthenticationWithRegistrySymmetricKey(MainPage.GetUniqueDeviceId(), App.DEVICE_KEY));

            Task.Run(() =>
            {
                sendDeviceToCloudMessagesAsync();
            });
        }

        public bool IsMovingForward
        {
            get
            {
                return false;
            }
        }
        public void Init()
        {
        }

        public void MoveBackwards()
        {
            pendingMessagesQueue.Enqueue("moving backwards");
        }

        public void MoveForward()
        {
            pendingMessagesQueue.Enqueue("moving forward");
        }

        public void Stop()
        {
            pendingMessagesQueue.Enqueue("stoping");
        }

        public void TurnLeft()
        {
            pendingMessagesQueue.Enqueue("turning right");
        }

        public void TurnRight()
        {
            pendingMessagesQueue.Enqueue("turning left");
        }

        private static async void sendDeviceToCloudMessagesAsync()
        {
            while (true)
            {
                while (pendingMessagesQueue.Any())
                {
                    try
                    {
                        var messagepart = pendingMessagesQueue.Peek();

                        var data = new
                        {
                            deviceId = MainPage.GetUniqueDeviceId(),
                            action = messagepart
                        };

                        var messageString = JsonConvert.SerializeObject(data);
                        var message = new Message(Encoding.ASCII.GetBytes(messageString));
                        await mDeviceClient.SendEventAsync(message);

                        pendingMessagesQueue.Dequeue();
                    }
                    catch (Exception) { }
                }

                Task.Delay(1000).Wait();
            }
        }
    }
}