using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.AppService;
using Windows.Foundation.Collections;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace DataCollector
{
    public sealed class ServiceStartupTask : IBackgroundTask
    {
        private AppServiceConnection _connection;
        private BackgroundTaskDeferral _deferral;
        private volatile static SensorHatProxy _proxy = null;
        private bool? _state;

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            if (_proxy == null)
            {
                _proxy = new SensorHatProxy();
                _proxy.Init();
            }
           
            var res = _proxy.ReadSensorData();

            _deferral = taskInstance.GetDeferral();
            taskInstance.Canceled += OnTaskCanceled;

            var triggerDetails = taskInstance.TriggerDetails as AppServiceTriggerDetails;
            if (triggerDetails != null)
            {

                _connection = triggerDetails.AppServiceConnection;
                _connection.RequestReceived += Connection_RequestReceived;
            }

            
        }

        private async void Connection_RequestReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args)
        {
            // if you are doing anything awaitable, you need to get a deferral
            var requestDeferral = args.GetDeferral();
            var returnMessage = new ValueSet();
            try
            {
                //obtain and react to the command passed in by the client
                var message = args.Request.Message["Request"] as string;
                switch (message)
                {
                    case "SensorData":

                        CrossCuttingRT.Dto.SensorsDataDto sd = _proxy.ReadSensorData();

                        returnMessage.Add("Temperature", sd.Temperature);
                        returnMessage.Add("LightLevel", sd.LightLevel);
                        break;
                }
                returnMessage.Add("Response", "OK");
            }
            catch (Exception ex)
            {
                returnMessage.Add("Response", "Failed: " + ex.Message);
            }

            await args.Request.SendResponseAsync(returnMessage);

            //let the OS know that the action is complete
            requestDeferral.Complete();
        }

        private void OnTaskCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            if (_deferral != null)
            {
                _deferral.Complete();
                _deferral = null;
            }
        }
    }
}