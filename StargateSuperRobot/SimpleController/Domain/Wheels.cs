using Windows.Devices.Gpio;

namespace SimpleController.Domain
{
    internal class Wheels : IWheels
    {
        private GpioController mGpioController;
        private GpioPin mLeftMotorForward;
        private GpioPin mLeftMotorBackwards;
        private GpioPin mRightMotorForward;
        private GpioPin mRightMotorBackwards;

        public void Init()
        {
            mGpioController = GpioController.GetDefault();

            mLeftMotorForward = mGpioController.OpenPin(16);
            mLeftMotorForward.Write(GpioPinValue.Low);
            mLeftMotorForward.SetDriveMode(GpioPinDriveMode.Output);

            mLeftMotorBackwards = mGpioController.OpenPin(19);
            mLeftMotorBackwards.Write(GpioPinValue.Low);
            mLeftMotorBackwards.SetDriveMode(GpioPinDriveMode.Output);

            mRightMotorForward = mGpioController.OpenPin(13);
            mRightMotorForward.Write(GpioPinValue.Low);
            mRightMotorForward.SetDriveMode(GpioPinDriveMode.Output);

            mRightMotorBackwards = mGpioController.OpenPin(12);
            mRightMotorBackwards.Write(GpioPinValue.Low);
            mRightMotorBackwards.SetDriveMode(GpioPinDriveMode.Output);
        }

        public bool IsMovingForward
        {
            get
            {
                return mRightMotorForward.Read() == GpioPinValue.High && mLeftMotorForward.Read() == GpioPinValue.High;
            }
        }

        public void MoveForward()
        {
            mRightMotorBackwards.Write(GpioPinValue.Low);
            mLeftMotorBackwards.Write(GpioPinValue.Low);
            mRightMotorForward.Write(GpioPinValue.High);
            mLeftMotorForward.Write(GpioPinValue.High);
        }

        public void MoveBackwards()
        {
            mRightMotorForward.Write(GpioPinValue.Low);
            mLeftMotorForward.Write(GpioPinValue.Low);
            mRightMotorBackwards.Write(GpioPinValue.High);
            mLeftMotorBackwards.Write(GpioPinValue.High);
        }

        public void Stop()
        {
            mRightMotorForward.Write(GpioPinValue.Low);
            mLeftMotorForward.Write(GpioPinValue.Low);
            mRightMotorBackwards.Write(GpioPinValue.Low);
            mLeftMotorBackwards.Write(GpioPinValue.Low);
        }

        public void TurnRight()
        {
            mRightMotorForward.Write(GpioPinValue.Low);
            mLeftMotorBackwards.Write(GpioPinValue.Low);
            mRightMotorBackwards.Write(GpioPinValue.High);
            mLeftMotorForward.Write(GpioPinValue.High);
        }

        public void TurnLeft()
        {
            mLeftMotorForward.Write(GpioPinValue.Low);
            mRightMotorBackwards.Write(GpioPinValue.Low);
            mRightMotorForward.Write(GpioPinValue.High);
            mLeftMotorBackwards.Write(GpioPinValue.High);
        }
    }
}