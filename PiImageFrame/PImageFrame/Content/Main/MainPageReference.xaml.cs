using Windows.UI.Core;
using Windows.UI.Xaml.Navigation;

namespace PImageFrame.Content.Main
{
    public sealed partial class MainPageReference
    {
        public MainViewModel Vm => (MainViewModel)DataContext;

        public MainPageReference()
        {
            InitializeComponent();

            SystemNavigationManager.GetForCurrentView().BackRequested += SystemNavigationManagerBackRequested;

            Loaded += (s, e) =>
            {
                Vm.RunClock();
            };
        }

        private void SystemNavigationManagerBackRequested(object sender, BackRequestedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                e.Handled = true;
                Frame.GoBack();
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            Vm.StopClock();
            base.OnNavigatingFrom(e);
        }
    }
}
