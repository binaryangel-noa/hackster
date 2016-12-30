using GalaSoft.MvvmLight;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RemoteControl.Wpf.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class AppShellViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the AppShellViewModel class.
        /// </summary>
        public AppShellViewModel()
        {
            ViewModelLocator.Instance.EventsReader.BitmapAquired += EventsReader_BitmapAquired;
        }

        private void EventsReader_BitmapAquired(object sender, BitmapSource e)
        {
            ViewModelLocator.Instance.Main.RemoteBitmap = e;
        }

        private bool mIsStartPageSelected;

        public bool IsStartPageSelected
        {
            get { return mIsStartPageSelected; }
            set { Set(ref mIsStartPageSelected, value); }
        }

        private bool mIsEventsPageSelected;

        public bool IsEventsPageSelected
        {
            get { return mIsEventsPageSelected; }
            set { Set(ref mIsEventsPageSelected, value); }
        }

        private bool mIsAdministrationPageSelected;

        public bool IsAdministrationPageSelected
        {
            get { return mIsAdministrationPageSelected; }
            set { Set(ref mIsAdministrationPageSelected, value); }
        }
    }
}