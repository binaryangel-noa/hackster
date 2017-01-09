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
            //_instance.D3.Color = new FEZHAT.Color(0, 0, 255);
        }

        internal CrossCuttingRT.Dto.SensorsDataDto ReadSensorData()
        {
            try
            {
                var dto = new CrossCuttingRT.Dto.SensorsDataDto();

                dto.Temperature = _instance.GetTemperature();

                dto.LightLevel = _instance.GetLightLevel();

                //if (_instance.D2.Color.R == 255)
                //{
                //    _instance.D2.Color = new FEZHAT.Color(0, 255, 0);
                //}
                //else
                //{
                //    _instance.D2.Color = new FEZHAT.Color(255, 0, 0);
                //}

                return dto;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}