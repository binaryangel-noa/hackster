using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using PImageFrame.Content.Layout;
using PImageFrame.Content.Main;
using PImageFrame.Content.Second;
using PImageFrame.Model;

namespace PImageFrame.Common
{
    public class ViewModelLocator
    {
        public static string MainPageKey = typeof(MainPageReference).Name;
        public static string SecondPageKey = typeof(SecondPage).Name;
        
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            var nav = new ImageFrameNavigationService();
            foreach (var item in AppShell.navlist)
            {
                nav.Configure(item.DestPage.Name, item.DestPage);
            }
            //nav.Configure(SecondPageKey, typeof(Content.Second.SecondPage));
            //nav.Configure(MainPageKey, typeof(Content.Main.MainPageReference));

            SimpleIoc.Default.Register<INavigationService>(() => nav);

            SimpleIoc.Default.Register<IDialogService, DialogService>();

            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IDataService, Content.Main.DesignDataService>();
            }
            else
            {
                SimpleIoc.Default.Register<IDataService, DataService>();
            }

            SimpleIoc.Default.Register<Content.Main.MainViewModel>();
            //SimpleIoc.Default.Register<Content.Layout.ShellViewModel>();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public Content.Main.MainViewModel Main => ServiceLocator.Current.GetInstance<Content.Main.MainViewModel>();
        //public Content.Layout.ShellViewModel Shell => ServiceLocator.Current.GetInstance<Content.Layout.ShellViewModel>();
    }
}
