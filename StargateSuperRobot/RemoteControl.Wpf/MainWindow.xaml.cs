using System.Windows;
using System.Windows.Input;
using RemoteControl.Wpf.ViewModel;

namespace RemoteControl.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();            
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (ViewModelLocator.Instance != null && ViewModelLocator.Instance.AppShell.IsStartPageSelected)
            {
                if (e.Key == Key.W)
                {
                    ViewModelLocator.Instance.Main.MoveForwardDeviceCommand.Execute(null);
                }
                if (e.Key == Key.S)
                {
                    ViewModelLocator.Instance.Main.StopMovmentDeviceCommand.Execute(null);
                }
                if (e.Key == Key.X)
                {
                    ViewModelLocator.Instance.Main.MoveBackwardDeviceCommand.Execute(null);
                }
                if (e.Key == Key.A)
                {
                    ViewModelLocator.Instance.Main.TurnLeftDeviceCommand.Execute(null);
                }
                if (e.Key == Key.D)
                {
                    ViewModelLocator.Instance.Main.TurnRightDeviceCommand.Execute(null);
                }
            }
            
            base.OnKeyDown(e);
        }
    }
}