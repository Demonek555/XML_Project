using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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


        private async void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var aktStrona = frmMain.CurrentSourcePageType;
            Type docStrona = null;
            if (args.IsSettingsInvoked == true)
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
                case "ExitMenuItem":
                    var dialog = new ContentDialog()
                    {
                        Title = "Zamykanie programu",
                        Content = "Czy na pewno chcesz wyjść z programu?\nZmiany zostaną zapisane.",
                        PrimaryButtonText = "Tak",
                        CloseButtonText = "Nie"
                    };

                    var result = await dialog.ShowAsync();

                    if (result == ContentDialogResult.Primary)
                    {
                        frmMain.Navigate(typeof(MainPage));

                        await Task.Delay(100); 
                        App.Current.Exit();
                    }
                    return;

                default:
                    break;
            }
            if (docStrona != null && docStrona != aktStrona)
            {
                if (docStrona != null && docStrona != aktStrona)
                {
                    var currentPage = frmMain.Content;

                    if (currentPage is AuthorListMenuItem authorPage && !await authorPage.CzyDanePoprawne())
                    {
                        ResetujZaznaczenieMenu();
                        return;
                    }

                    if (currentPage is PublisherListMenuItem publisherPage && !await publisherPage.CzyDanePoprawne())
                    {
                        ResetujZaznaczenieMenu();
                        return;
                    }

                    if (currentPage is BookListMenuItem bookPage && !await bookPage.CzyDanePoprawne())
                    {
                        ResetujZaznaczenieMenu();
                        return;
                    }



                    frmMain.Navigate(docStrona);
                }

                frmMain.Navigate(docStrona);
            }
        }
        private void ResetujZaznaczenieMenu()
        {
            var currentPageName = frmMain.CurrentSourcePageType.Name;
            NavView.SelectedItem = NavView.MenuItems
                .OfType<NavigationViewItem>()
                .FirstOrDefault(item => currentPageName.StartsWith(item.Name));
        }

        private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            frmMain.GoBack();
        }
    }
}
