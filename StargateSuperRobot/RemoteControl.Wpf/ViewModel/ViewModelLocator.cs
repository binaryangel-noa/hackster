/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:RemoteControl.Wpf.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using RemoteControl.Wpf.Model;

namespace RemoteControl.Wpf.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        public static ViewModelLocator Instance = null;
        public ViewModelLocator()
        {
            Instance = this;
        }
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IDataService, Design.DesignDataService>();
            }
            else
            {
                SimpleIoc.Default.Register<IDataService, DataService>();
            }

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<AppShellViewModel>();
            SimpleIoc.Default.Register<AdministrationViewModel>();
            SimpleIoc.Default.Register<EventsReaderViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public AppShellViewModel AppShell
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AppShellViewModel>();
            }
        }

        public AdministrationViewModel Administration
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AdministrationViewModel>();
            }
        }

        public EventsReaderViewModel EventsReader
        {
            get
            {
                return ServiceLocator.Current.GetInstance<EventsReaderViewModel>();
            }
        }
        
        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }
    }
}