using System;
using Windows.Devices.Gpio;

namespace SimpleController.Domain
{
    public class ObstacleSensors
    {
        public class SensorReading : EventArgs
        {
            public GpioPinValue LeftValue { get; set; }
            public GpioPinValue RightValue { get; set; }

            public override bool Equals(object obj)
            {
                var other = obj as SensorReading;
                if (other == null)
                {
                    return false;
                }

                return LeftValue == other.LeftValue && RightValue == other.RightValue;
            }
        }

        public delegate void ValueReadingChangedEventHandler(object sender, SensorReading e);
        public event ValueReadingChangedEventHandler ValueReadingChanged;

        private GpioController mGpioController;
        private GpioPin mLeftSensor;
        private GpioPin mRightSensor;

        public ObstacleSensors()
        {
            mGpioController = GpioController.GetDefault();
            mRightSensor = mGpioController.OpenPin(17);
            mRightSensor.SetDriveMode(GpioPinDriveMode.Input);
            mRightSensor.ValueChanged += MRightSensor_ValueChanged;
            mLeftSensor = mGpioController.OpenPin(4);
            mLeftSensor.SetDriveMode(GpioPinDriveMode.Input);
            mLeftSensor.ValueChanged += MLeftSensor_ValueChanged;
        }

        private void MRightSensor_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args)
        {
            ReadCurrentValues();
        }

        private void MLeftSensor_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args)
        {
            ReadCurrentValues();
        }

        private SensorReading mLastReading = null;

        public SensorReading LastReading
        {
            get
            {
                return mLastReading;
            }
            set
            {
                if (value != null)
                {
                    if (!value.Equals(LastReading))
                    {
                        if (ValueReadingChanged != null)
                        {
                            var del = ValueReadingChanged;
                            del(this, value);
                        }
                    }

                    mLastReading = value;
                }
            }
        }

        public SensorReading ReadCurrentValues()
        {
            var valueLeft = mLeftSensor.Read();
            var valueRight = mRightSensor.Read();
            return LastReading = new SensorReading { LeftValue = valueLeft, RightValue = valueRight };
        }
    }
}