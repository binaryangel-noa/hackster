using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using PImageFrame.Model;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using PImageFrame.Content.Layout;

namespace PImageFrame.Common
{
    public class ImageFrameNavigationService : INavigationService
    {
        public string CurrentPageKey
        {
            get; set;
        }

        public void GoBack()
        {
            var shell = AppShell.Current;
            var frame = shell?.AppFrame as Frame;
            if (frame.CanGoBack) frame.GoBack();
        }

        public void NavigateTo(string pageKey)
        {
            NavigateTo(pageKey, null);
        }

        public void NavigateTo(string pageKey, object parameter)
        {

            var shell = AppShell.Current;
            var frame = shell?.AppFrame as Frame;
            Page page = frame?.Content as Page;
            var type = _configuration[pageKey];

            try
            {
                if (page?.GetType() != type)
                {
                    frame.Navigate(type, parameter);
                }
                CurrentPageKey = pageKey;
            }
            catch (System.Exception)
            {

            }
        }

        Dictionary<string, Type> _configuration = new Dictionary<string, Type>();

        internal void Configure(string secondPageKey, Type type)
        {
            _configuration[secondPageKey] = type;
        }
    }
}
