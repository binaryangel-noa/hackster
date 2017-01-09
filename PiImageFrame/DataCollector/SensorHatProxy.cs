using GHIElectronics.UWP.Shields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector
{
    internal class SensorHatProxy
    {

        internal FEZHAT _instance = null;

        public void Init()
        {
            var task = FEZHAT.CreateAsync();
            task.Wait();
            
            _instance=  task.Result;
        }

        internal CrossCuttingRT.Dto.SensorsDataDto ReadSensorData()
        {
            try
            {
                var dto = new CrossCuttingRT.Dto.SensorsDataDto();

                dto.Temperature = _instance.GetTemperature();

                dto.LightLevel = _instance.GetLightLevel();

                return dto;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}