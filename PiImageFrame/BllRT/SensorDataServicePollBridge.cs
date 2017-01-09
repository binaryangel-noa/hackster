using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrossCuttingRT.Dto;
using Windows.Foundation;

namespace BllRT
{
    public delegate void SensorDataChangedCallback();

    public sealed class SensorDataServicePollBridge
    {
        static SensorDataServicePoll Bridge = null;

        SensorDataServicePoll _bridge = new SensorDataServicePoll();

        private SensorDataChangedCallback _callback;

        public SensorDataServicePollBridge()
        {
            if (Bridge == null)
            {
                Bridge = new SensorDataServicePoll();
            }
            _bridge = Bridge;
        }

        public CrossCuttingRT.Dto.SensorsDataDto GetSensorData()
        {
            return _bridge.SensorData;
        }

    }
}