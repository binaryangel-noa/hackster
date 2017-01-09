using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using PImageFrame.Model;
using PImageFrame.Common;
using Windows.UI.Xaml.Controls;

namespace PImageFrame.Content.Layout
{
    public class ShellViewModel : ViewModelBase
    {
        private IDataService _dataService;
        private RelayCommand<string> _navigateCommand;
        private RelayCommand _navigateMainCommand;
        private RelayCommand _navigateBackCommand;
        private RelayCommand<object> _ToggleHamburgerCommand;

        private readonly INavigationService _navigationService;

        public ShellViewModel(IDataService dataService,
            INavigationService navigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
        }

        public RelayCommand<string> NavigateCommand
        {
            get
            {
                return _navigateCommand
                       ?? (_navigateCommand = new RelayCommand<string>(
                           p =>
                           {
                               _navigationService.NavigateTo(ViewModelLocator.SecondPageKey);
                           }
                           ));
            }
        }

        public RelayCommand NavigateMainCommand
        {
            get
            {


                return _navigateMainCommand
                       ?? (_navigateMainCommand = new RelayCommand(
                           () =>
                           {
                               _navigationService.NavigateTo(ViewModelLocator.MainPageKey);
                           }
                           ));
            }
        }

        public RelayCommand<object> ToggleHamburgerCommand
        {
            get
            {
                return _ToggleHamburgerCommand
                       ?? (_ToggleHamburgerCommand = new RelayCommand<object>(
                           p =>
                           {
                               var sv = p as SplitView;
                               if (sv != null)
                               {
                                   if (!sv.IsPaneOpen)
                                   {
                                       sv.IsPaneOpen = true;
                                   }
                                   else
                                   {
                                       sv.IsPaneOpen = false;
                                   }
                               }
                           }
                           ));

                //return _navigateCommand
                //       ?? (_navigateCommand = new RelayCommand<string>(
                //           p => _navigationService.NavigateTo(ViewModelLocator.SecondPageKey, p),
                //           p => !string.IsNullOrEmpty(p)));
            }
        }

        public RelayCommand NavigateBackCommand
        {
            get
            {
                return _navigateBackCommand ?? (_navigateBackCommand = new RelayCommand(() => { _navigationService.GoBack(); }));
            }
        }
    }
}
