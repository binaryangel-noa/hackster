using RemoteControl.Wpf.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RemoteControl.Wpf.View
{
    /// <summary>
    /// Interaction logic for RemoteControl.xaml
    /// </summary>
    public partial class RemoteControl : Page
    {
        public RemoteControl()
        {
            InitializeComponent();
        }

        private void Slider_DragOver(object sender, DragEventArgs e)
        {
            //ViewModelLocator.Instance.Main.TiltSliderDragEndCommand.Execute(null);
        }

        private void Slider_Drop(object sender, DragEventArgs e)
        {

        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        ////private void Slider_DragOver(object sender, DragEventArgs e)
        ////{
        ////    ViewModelLocator.Instance.Main.TiltSliderDragEndCommand.Execute(null);
        ////}
    }
}
