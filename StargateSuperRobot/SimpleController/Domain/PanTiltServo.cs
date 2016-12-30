using Microsoft.IoT.Lightning.Providers;
using System;
using System.Threading.Tasks;
using Windows.Devices;
using Windows.Devices.Pwm;

namespace SimpleController.Domain
{
    internal class PanTiltServo
    {
        private PwmPin mLeftPanServoPwm;
        private PwmController mPwmController;
        private PwmPin mBottomPanServo;
        private PwmPin mTopTiltServo;
        private PwmController pwmControlleTilt;

        public PanTiltServo()
        {
            Task.Run(async () =>
            {
                await init();
            }).Wait();
        }

        private async Task init()
        {
            try
            {
                LowLevelDevicesController.DefaultProvider = LightningProvider.GetAggregateProvider();
                var pwmControllers = await PwmController.GetControllersAsync(LightningPwmProvider.GetPwmProvider());
                var pwmControllerPan = pwmControllers[1];
                pwmControllerPan.SetDesiredFrequency(350);
                mPwmController = pwmControllerPan;

                pwmControllers = await PwmController.GetControllersAsync(LightningPwmProvider.GetPwmProvider());
                pwmControlleTilt = pwmControllers[1];
                pwmControlleTilt.SetDesiredFrequency(450);

                mTopTiltServo = pwmControlleTilt.OpenPin(24);
                mBottomPanServo = mPwmController.OpenPin(25);

                mTopTiltServo.SetActiveDutyCyclePercentage(0.5);
                mBottomPanServo.SetActiveDutyCyclePercentage(0.5);

                mTopTiltServo.Start();
                mBottomPanServo.Start();

                Task.Run(() =>
                {
                    System.Threading.Tasks.Task.Delay(250).Wait();
                    mBottomPanServo.Stop();
                    mTopTiltServo.Stop();
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void PanRight()
        {
            mBottomPanServo.SetActiveDutyCyclePercentage(Math.Max(mBottomPanServo.GetActiveDutyCyclePercentage() - 0.01, 0));
            mBottomPanServo.Start();
            Task.Run(() =>
            {
                System.Threading.Tasks.Task.Delay(100).Wait();
                mBottomPanServo.Stop();
            });
        }

        public void PanLeft()
        {
            mBottomPanServo.SetActiveDutyCyclePercentage(Math.Min(mBottomPanServo.GetActiveDutyCyclePercentage() + 0.01, 1));
            mBottomPanServo.Start();
            Task.Run(() =>
            {
                System.Threading.Tasks.Task.Delay(100).Wait();
                mBottomPanServo.Stop();
            });
        }

        public void TiltUp()
        {
            mTopTiltServo.SetActiveDutyCyclePercentage(Math.Max(mBottomPanServo.GetActiveDutyCyclePercentage() - 0.01, 0));
            mTopTiltServo.Start();
            Task.Run(() =>
            {
                System.Threading.Tasks.Task.Delay(100).Wait();
                mTopTiltServo.Stop();
            });
        }

        public void TiltDown()
        {
            mTopTiltServo.SetActiveDutyCyclePercentage(Math.Min(mBottomPanServo.GetActiveDutyCyclePercentage() + 0.01, 1));
            mTopTiltServo.Start();
            Task.Run(() =>
            {
                System.Threading.Tasks.Task.Delay(100).Wait();
                mTopTiltServo.Stop();
            });
        }

        internal void PanToPosition(double value)
        {
            mBottomPanServo.SetActiveDutyCyclePercentage(value);
            mBottomPanServo.Start();
            Task.Run(() =>
            {
                System.Threading.Tasks.Task.Delay(100).Wait();
                mBottomPanServo.Stop();
            });
        }

        internal void TiltToPosition(double value)
        {
            mTopTiltServo.SetActiveDutyCyclePercentage(value);
            mTopTiltServo.Start();
            Task.Run(() =>
            {
                System.Threading.Tasks.Task.Delay(100).Wait();
                mTopTiltServo.Stop();
            });
        }

        internal void Center()
        {
            mTopTiltServo.SetActiveDutyCyclePercentage(0.5);
            mBottomPanServo.SetActiveDutyCyclePercentage(0.5);

            mTopTiltServo.Start();
            mBottomPanServo.Start();

            Task.Run(() =>
            {
                System.Threading.Tasks.Task.Delay(250).Wait();
                mBottomPanServo.Stop();
                mTopTiltServo.Stop();
            });
        }
    }
}