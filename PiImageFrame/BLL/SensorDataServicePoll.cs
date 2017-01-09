using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.Foundation.Collections;
using Windows.System.Threading;

namespace BLL
{
    public class SensorDataServicePoll
    {
        private ThreadPoolTimer _timer;

        private static volatile bool _running;

        public CrossCuttingRT.Dto.SensorsDataDto SensorData { get; set; }

        public SensorDataServicePoll()
        {
            _timer = ThreadPoolTimer.CreatePeriodicTimer(_timerElapsed, TimeSpan.FromSeconds(1));
        }

        private async void _timerElapsed(ThreadPoolTimer timer)
        {
            if (_running) return;
            _running = true;
            try
            {
                var data = await PollData();
                if (data != null)
                {
                    SensorData = data;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex);
            }
            finally
            {
                _running = false;
            }
        }

        private async Task<CrossCuttingRT.Dto.SensorsDataDto> PollData()
        {
            CrossCuttingRT.Dto.SensorsDataDto result = null;

            using (var dataService = new AppServiceConnection())
            {
                var listing = await AppServiceCatalog.FindAppServiceProvidersAsync("DataCollectorInterface");
                var packageName = "";

                // there may be cases where other applications could expose the same App Service Name, in our case
                // we only have the one
                if (listing.Count == 1)
                {
                    packageName = listing[0].PackageFamilyName;
                }

                dataService.AppServiceName = "DataCollectorInterface";
                dataService.PackageFamilyName = packageName;
                var status = await dataService.OpenAsync();

                if (status == AppServiceConnectionStatus.Success)
                {
                    var msg = new ValueSet();
                    msg.Add("Request", "SensorData");
                    AppServiceResponse request = await dataService.SendMessageAsync(msg);

                    if (request.Status == AppServiceResponseStatus.Success)
                    {
                        CrossCuttingRT.Dto.SensorsDataDto data = new CrossCuttingRT.Dto.SensorsDataDto();

                        data.Temperature = request.Message["Temperature"] as double?;
                        data.LightLevel = request.Message["LightLevel"] as double?;
                        result = data;
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Request Response Status: " + status.ToString());
                    }
                }
            }

            return result;
        }

    }
}
