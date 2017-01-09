using GHIElectronics.UWP.Shields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEZHATProxy
{
    public sealed class SensorHatProxy
    {
        private static readonly Lazy<FEZHAT> lazy =
            new Lazy<FEZHAT>(() =>
           {
               try
               {
                   var task = FEZHAT.CreateAsync();
                   task.Wait();
                   Instance.D3.Color = new FEZHAT.Color(0, 0, 255);
                   return task.Result;
               }
               catch (Exception)
               {
                   return null;
               }

           }, false);

        internal static FEZHAT Instance
        {
            get
            {
                if (_instance == null)
                {
                    var task = FEZHAT.CreateAsync();
                    task.Wait();
                    Instance.D3.Color = new FEZHAT.Color(0, 0, 255);
                    _instance =  task.Result;
                }
                return _instance;
            }
        }

        internal static FEZHAT _instance = null;

        public CrossCuttingRT.Dto.SensorsDataDto ReadSensorData()
        {
            try
            {
                //var InstanceTask = FEZHAT.CreateAsync();
                //InstanceTask.Wait();
                //var Instance = InstanceTask.Result;
                //Instance.D3.Color = new FEZHAT.Color(0, 0, 255);

                if (Instance == null) return null;

                var dto = new CrossCuttingRT.Dto.SensorsDataDto();

                dto.Temperature = Instance.GetTemperature();

                dto.LightLevel = Instance.GetLightLevel();

                if (Instance.D2.Color.R == 255)
                {
                    Instance.D2.Color = new FEZHAT.Color(0, 255, 0);
                }
                else
                {
                    Instance.D2.Color = new FEZHAT.Color(255, 0, 0);
                }

                return dto;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}