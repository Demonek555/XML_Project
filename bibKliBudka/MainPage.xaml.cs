using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace bibKliBudka
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            if (ApplicationData.Current.LocalSettings.Values["tryb"] != null)
            {
                if (Window.Current.Content is FrameworkElement rootElement)
                {
                    rootElement.RequestedTheme = (ElementTheme)ApplicationData.Current.LocalSettings.Values["tryb"];
                }
            }
            
            //test danych
            (App.Current as App).db.TestData();
            
        }

        private async void btStronaWWW_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("http://ukw.edu.pl"));
        }

        private void btPomoc_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(PomocPage));

        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var aktStrona = frmMain.CurrentSourcePageType;
            Type docStrona = null;
            if(args.IsSettingsInvoked == true)
            {

                    NavView.Header = "wybrano: " + args.InvokedItemContainer.Name;
                    docStrona = typeof(SettingsPage);
                   
            }
            var wybrane = args.InvokedItemContainer.Name;
            switch (wybrane)
            {
                case "AuthorListMenuItem":

                    NavView.Header = "wybrano: " + args.InvokedItemContainer.Name;
                    docStrona = typeof(AuthorListMenuItem);
                    break;
                case "PublisherListMenuItem":

                    NavView.Header = "wybrano: " + args.InvokedItemContainer.Name;
                    docStrona = typeof(PublisherListMenuItem);
                    break;
                case "BookListMenuItem":

                    NavView.Header = "wybrano: " + args.InvokedItemContainer.Name;
                    docStrona = typeof(BookListMenuItem);
                    break;
                case "HelpMenuItem":
                    NavView.Header = "wybrano: " + args.InvokedItemContainer.Name;
                    docStrona = typeof(PomocPage);
                    break;
                default:
                break;   
            }
            if (docStrona!=null && docStrona!=aktStrona)
            {
                frmMain.Navigate(docStrona);
            }
        }

        private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
                frmMain.GoBack();
        }
    }
}
