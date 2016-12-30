using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Azure.Devices;
using RemoteControl.Wpf.Model;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace RemoteControl.Wpf.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService mDataService;

        private RelayCommand _moveBackwardDeviceCommand;
        private RelayCommand _moveForwardDeviceCommand;
        private RelayCommand _stopMovmentDeviceCommand;
        private RelayCommand _turnLeftDeviceCommand;
        private RelayCommand _turnRightDeviceCommand;
        private RelayCommand mCenterServoDeviceCommand;
        private string mDeviceId;

        private ObservableCollection<DataItem> mLog;

        private double mPanSliderValue;

        private RelayCommand mPanSliderValueChangedCommand;

        private double mPanValueToSend = 0.5;
        private Task mSendPanValueTask = null;
        private Task mSendTiltValueTask = null;
        private ServiceClient mServiceClient;

        private double mTiltSliderValue;

        private RelayCommand mTiltSliderValueChangedCommand;

        private double mTiltValueToSend = 0.5;
        private bool mValueChanging;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            mDataService = dataService;
            mDataService.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }
                });
            DeviceId = Globals.DEVICE_ID;
            mServiceClient = ServiceClient.CreateFromConnectionString(Globals.CONNECTIONSTRING_OWNER);
            TiltSliderValue = 50;
            PanSliderValue = 50;
        }

        public RelayCommand CenterServoDeviceCommand
        {
            get
            {
                return mCenterServoDeviceCommand ?? (mCenterServoDeviceCommand = new RelayCommand(
                    async () =>
                    {
                        var commandMessage = new Message();
                        commandMessage.Properties["Action"] = "SERVO_CENTER";
                        commandMessage.Properties["Created"] = DateTime.UtcNow.Ticks.ToString();
                        TiltSliderValue = 50;
                        PanSliderValue = 50;
                        await sendCloudToDeviceMessageAsync(commandMessage);
                    }));
            }
        }

        public RelayCommand StartFeedDeviceCommand
        {
            get
            {
                return mStartFeedDeviceCommand ?? (mStartFeedDeviceCommand = new RelayCommand(
                    async () =>
                    {
                        var commandMessage = new Message();
                        commandMessage.Properties["Action"] = "FEED_START";
                        commandMessage.Properties["Created"] = DateTime.UtcNow.Ticks.ToString();
                        await sendCloudToDeviceMessageAsync(commandMessage);
                    }));
            }
        }

        public RelayCommand StopFeedDeviceCommand
        {
            get
            {
                return mStopFeedDeviceCommand ?? (mStopFeedDeviceCommand = new RelayCommand(
                    async () =>
                    {
                        var commandMessage = new Message();
                        commandMessage.Properties["Action"] = "FEED_STOP";
                        commandMessage.Properties["Created"] = DateTime.UtcNow.Ticks.ToString();
                        await sendCloudToDeviceMessageAsync(commandMessage);
                    }));
            }
        }

        public string DeviceId
        {
            get { return mDeviceId; }
            set { Set(ref mDeviceId, value); }
        }

        public ObservableCollection<DataItem> Log
        {
            get { return mLog; }
            set { Set(ref mLog, value); }
        }

        private BitmapSource mRemoteBitmap;
        private RelayCommand mStartFeedDeviceCommand;
        private RelayCommand mStopFeedDeviceCommand;

        public BitmapSource RemoteBitmap
        {
            get { return mRemoteBitmap; }
            set { Set(ref mRemoteBitmap , value); }
        }
        
        public RelayCommand MoveBackwardDeviceCommand
        {
            get
            {
                return _moveBackwardDeviceCommand ?? (_moveBackwardDeviceCommand = new RelayCommand(
                    async () =>
                    {
                        var commandMessage = new Message();
                        commandMessage.Properties["Action"] = "Backward";
                        commandMessage.Properties["Created"] = DateTime.UtcNow.Ticks.ToString();
                        await sendCloudToDeviceMessageAsync(commandMessage);
                    }));
            }
        }

        public RelayCommand MoveForwardDeviceCommand
        {
            get
            {
                return _moveForwardDeviceCommand ?? (_moveForwardDeviceCommand = new RelayCommand(
                    async () =>
                    {
                        var commandMessage = new Message();
                        commandMessage.Properties["Action"] = "Forward";
                        commandMessage.Properties["Created"] = DateTime.UtcNow.Ticks.ToString();
                        await sendCloudToDeviceMessageAsync(commandMessage);
                    }));
            }
        }

        public double PanSliderValue
        {
            get { return mPanSliderValue; }
            set { Set(ref mPanSliderValue, value); }
        }

        public RelayCommand PanSliderValueChangedCommand
        {
            get
            {
                return mPanSliderValueChangedCommand ?? (mPanSliderValueChangedCommand = new RelayCommand(
                    () =>
                    {
                        mPanValueToSend = 1 + PanSliderValue / 100d * -1;
                        if (mSendPanValueTask == null)
                        {
                            mSendPanValueTask = Task.Run(async () =>
                            {
                                await System.Threading.Tasks.Task.Delay(750);
                                var commandMessage = new Message();
                                commandMessage.Properties["Action"] = "SERVO_PAN";
                                commandMessage.Properties["Value"] = mPanValueToSend.ToString(CultureInfo.InvariantCulture);
                                commandMessage.Properties["Created"] = DateTime.UtcNow.Ticks.ToString();
                                await sendCloudToDeviceMessageAsync(commandMessage);
                                mSendPanValueTask = null;
                            });
                        }
                    }));
            }
        }

        public RelayCommand StopMovmentDeviceCommand
        {
            get
            {
                return _stopMovmentDeviceCommand ?? (_stopMovmentDeviceCommand = new RelayCommand(
                    async () =>
                    {
                        var commandMessage = new Message();
                        commandMessage.Properties["Action"] = "Stop";
                        commandMessage.Properties["Created"] = DateTime.UtcNow.Ticks.ToString();
                        await sendCloudToDeviceMessageAsync(commandMessage);
                    }));
            }
        }

        public double TiltSliderValue
        {
            get { return mTiltSliderValue; }
            set { Set(ref mTiltSliderValue, value); }
        }

        public RelayCommand TiltSliderValueChangedCommand
        {
            get
            {
                return mTiltSliderValueChangedCommand ?? (mTiltSliderValueChangedCommand = new RelayCommand(
                    () =>
                    {
                        mTiltValueToSend = 1 + TiltSliderValue / 100d * -1;
                        if (mSendTiltValueTask == null)
                        {
                            mSendTiltValueTask = Task.Run(async () =>
                            {
                                await System.Threading.Tasks.Task.Delay(750);
                                var commandMessage = new Message();
                                commandMessage.Properties["Action"] = "SERVO_TILT";
                                commandMessage.Properties["Value"] = mTiltValueToSend.ToString(CultureInfo.InvariantCulture);
                                commandMessage.Properties["Created"] = DateTime.UtcNow.Ticks.ToString();
                                await sendCloudToDeviceMessageAsync(commandMessage);
                                mSendTiltValueTask = null;
                            });
                        }
                    }));
            }
        }

        public RelayCommand TurnLeftDeviceCommand
        {
            get
            {
                return _turnLeftDeviceCommand ?? (_turnLeftDeviceCommand = new RelayCommand(
                    async () =>
                    {
                        var commandMessage = new Message();
                        commandMessage.Properties["Action"] = "Left";
                        commandMessage.Properties["Created"] = DateTime.UtcNow.Ticks.ToString();
                        await sendCloudToDeviceMessageAsync(commandMessage);
                    }));
            }
        }

        public RelayCommand TurnRightDeviceCommand
        {
            get
            {
                return _turnRightDeviceCommand ?? (_turnRightDeviceCommand = new RelayCommand(
                    async () =>
                    {
                        var commandMessage = new Message();
                        commandMessage.Properties["Action"] = "Right";
                        commandMessage.Properties["Created"] = DateTime.UtcNow.Ticks.ToString();
                        await sendCloudToDeviceMessageAsync(commandMessage);
                    }));
            }
        }

        public bool ValueChanging
        {
            get { return mValueChanging; }
            set { Set(ref mValueChanging, value); }
        }

        private void addToLog(string message)
        {
            if (Log == null)
            {
                Log = new ObservableCollection<DataItem>();
            }
            Log.Insert(0, new DataItem(message));
        }

        private async Task sendCloudToDeviceMessageAsync(Message message)
        {
            await mServiceClient.SendAsync(DeviceId, message);
        }
    }
}