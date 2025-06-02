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
using System.Collections.ObjectModel;
using bibModelBudka.Model;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace bibKliBudka
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    class DataGridDataSourcePublishers
    {
        public ObservableCollection<WydawcyWydawca> PublishersObservable { get; set; }
    }
    public sealed partial class PublisherListMenuItem : Page
    {
        BDLibraryUWP dbUWP;
        DataGridDataSourcePublishers PublishersViewModel;

        public PublisherListMenuItem()
        {
            this.InitializeComponent();

            dbUWP = (App.Current as App).db;
            PublishersViewModel = new DataGridDataSourcePublishers()
            {
                PublishersObservable = new ObservableCollection<WydawcyWydawca>(dbUWP.PublishersLst)
            };
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            dbUWP.PublishersLst = PublishersViewModel.PublishersObservable.ToList();
            dbUWP.SavePublishers();
            base.OnNavigatingFrom(e);
        }
        private void DodajPublisher_Click(object sender, RoutedEventArgs e)
        {
            int nextId = PublishersViewModel.PublishersObservable.Count > 0
                ? PublishersViewModel.PublishersObservable.Max(p => p.id) + 1
                : 1;

            var nowy = new WydawcyWydawca
            {
                id = nextId,
                nazwa = "Nowe wydawnictwo",
                strona = "http://"
            };

            PublishersViewModel.PublishersObservable.Insert(0, nowy);
        }


        private async void UsunPublisher_Click(object sender, RoutedEventArgs e)
        {
            int indeks = dataGrid.SelectedIndex;

            if (indeks >= 0 && indeks < PublishersViewModel.PublishersObservable.Count)
            {
                PublishersViewModel.PublishersObservable.RemoveAt(indeks);
            }
            else
            {
                await new ContentDialog
                {
                    Title = "Błąd",
                    Content = "Zaznacz wydawnictwo do usunięcia.",
                    CloseButtonText = "OK"
                }.ShowAsync();
            }
        }
        public async Task<bool> CzyDanePoprawne()
        {
            var komunikaty = new List<string>();

            foreach (var wyd in PublishersViewModel.PublishersObservable)
            {
                var blad = $"ID {wyd.id}:";
                bool maBlad = false;

                if (string.IsNullOrWhiteSpace(wyd.nazwa))
                {
                    blad += " brak nazwy";
                    maBlad = true;
                }

                if (string.IsNullOrWhiteSpace(wyd.strona) || !Uri.IsWellFormedUriString(wyd.strona, UriKind.Absolute))
                {
                    blad += (maBlad ? "," : "") + " niepoprawny adres strony";
                    maBlad = true;
                }

                if (maBlad)
                {
                    komunikaty.Add(blad);
                }
            }

            if (komunikaty.Any())
            {
                await new ContentDialog
                {
                    Title = "Błąd walidacji danych wydawnictw",
                    Content = string.Join("\n", komunikaty),
                    CloseButtonText = "OK"
                }.ShowAsync();

                return false;
            }

            return true;
        }






    }

}

