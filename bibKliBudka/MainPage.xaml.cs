using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

        }

        private async void btStronaWWW_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("http://ukw.edu.pl"));
        }

        //private void btUstawienia_Tapped(object sender, TappedRoutedEventArgs e)
        //{
        //    Frame.Navigate(typeof(SettingsPage));
        //}

        private void btPomoc_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(PomocPage));

        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            //powtórny wybór
            var aktStrona = frmMain.CurrentSourcePageType;
            
            
            if(args.IsSettingsInvoked == true)
            {
                if (aktStrona == typeof(SettingsPage))
                {
                    return;
                }
                NavView.Header = "wybrano: " + args.InvokedItemContainer.Name;
                frmMain.Navigate(typeof(SettingsPage));
                //Frame.Navigate(typeof(SettingsPage));
            }
            var wybrane = args.InvokedItemContainer.Name;
            switch (wybrane)
            {
                case "AuthorListMenuItem":
                    //if (aktStrona == typeof(AuthorPage))
                    //{
                    //    return;
                    //}
                    NavView.Header = "wybrano: " + args.InvokedItemContainer.Name;
                    //frmMain.Navigate(typeof(AuthorPage));
                    break;
                case "PublisherListMenuItem":
                    //if (aktStrona == typeof(PublisherPage))
                    //{
                    //    return;
                    //}
                    NavView.Header = "wybrano: " + args.InvokedItemContainer.Name;
                    //frmMain.Navigate(typeof(PublisherPage));
                    break;
                case "BookListMenuItem":
                    //if (aktStrona == typeof(BookPage))
                    //{
                    //    return;
                    //}
                    NavView.Header = "wybrano: " + args.InvokedItemContainer.Name;
                    //frmMain.Navigate(typeof(BookPage));
                    break;
                default:
                break;   
            }
        }

        private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {

                frmMain.GoBack();
        }
    }
}
