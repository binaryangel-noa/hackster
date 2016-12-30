using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Common.Exceptions;
using RemoteControl.Wpf.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace RemoteControl.Wpf.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class AdministrationViewModel : ViewModelBase
    {
        private RelayCommand _registerDeviceCommand;
        private string mDeviceId;
        private string mDeviceKey = null;

        private ObservableCollection<DataItem> mLog;
        private RegistryManager mRegistryManager;

        /// <summary>
        /// Initializes a new instance of the Administration class.
        /// </summary>
        public AdministrationViewModel()
        {
            DeviceId = Globals.DEVICE_ID;
            Log = new ObservableCollection<DataItem>();
            mRegistryManager = RegistryManager.CreateFromConnectionString(Globals.CONNECTIONSTRING_OWNER);
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
        public RelayCommand RegisterDeviceCommand
        {
            get
            {
                return _registerDeviceCommand
                    ?? (_registerDeviceCommand = new RelayCommand(
                    async () =>
                    {
                        Log.Insert(0, (new DataItem("starting registration")));

                        var devicekey = await addDeviceAsync();
                        Log.Insert(0, (new DataItem($"Device key: {devicekey}")));

                        //do registration logic here.
                        Log.Insert(0, (new DataItem("done registration")));
                    }, () => { return !string.IsNullOrWhiteSpace(DeviceId); }));
            }
        }

        private async Task<string> addDeviceAsync()
        {
            string deviceId = DeviceId;

            Device device;
            try
            {
                device = await mRegistryManager.AddDeviceAsync(new Device(deviceId));
            }
            catch (DeviceAlreadyExistsException)
            {
                device = await mRegistryManager.GetDeviceAsync(deviceId);
            }
            if (device != null)
            {
                mDeviceKey = device.Authentication.SymmetricKey.PrimaryKey;
                return mDeviceKey;
            }
            return null;
        }
    }
}