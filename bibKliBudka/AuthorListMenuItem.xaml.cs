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
using Microsoft.Toolkit.Uwp.UI.Controls;
using bibModelBudka.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace bibKliBudka
{
    class DataGridDataSourceAuthors
    {
        public ObservableCollection<AutorzyAutor> AutorzyObservable { get; set; }
    }

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 

    public sealed partial class AuthorListMenuItem : Page
    {
        DataGridDataSourceAuthors AuthorsViewModel;
        BDLibraryUWP dbUWP;
        public AuthorListMenuItem()
        {
            dbUWP = (App.Current as App).db;
            AuthorsViewModel = new DataGridDataSourceAuthors()
            {
                AutorzyObservable = new ObservableCollection<AutorzyAutor>(dbUWP.AuthorsLst)
            };
            this.InitializeComponent();
        }
        private void DodajAutora_Click(object sender, RoutedEventArgs e)
        {
            int nextId = AuthorsViewModel.AutorzyObservable.Count > 0
                ? AuthorsViewModel.AutorzyObservable.Max(a => a.id) + 1
                : 1;

            var nowyAutor = new AutorzyAutor
            {
                id = nextId,
                imie = "Imię",
                nazwisko = "Nazwisko",
                rokUr = 1970
            };

            AuthorsViewModel.AutorzyObservable.Insert(0, nowyAutor);
        }

        private async void UsunAutora_Click(object sender, RoutedEventArgs e)
        {
            int indeks = dataGrid.SelectedIndex;

            if (indeks >= 0 && indeks < AuthorsViewModel.AutorzyObservable.Count)
            {
                AuthorsViewModel.AutorzyObservable.RemoveAt(indeks);
            }
            else
            {
                await new ContentDialog
                {
                    Title = "Błąd",
                    Content = "Zaznacz autora do usunięcia.",
                    CloseButtonText = "OK"
                }.ShowAsync();
            }
        }

        protected override async void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            var bledniAutorzy = AuthorsViewModel.AutorzyObservable
                .Where(a =>
                    string.IsNullOrWhiteSpace(a.imie) ||
                    string.IsNullOrWhiteSpace(a.nazwisko) ||
                    a.rokUr <= 0 || a.rokUr > DateTime.Now.Year)
                .ToList();

            if (bledniAutorzy.Any())
            {
                string msg = string.Join("\n", bledniAutorzy.Select(a => $"- ID {a.id}, imię: \"{a.imie}\", nazwisko: \"{a.nazwisko}\""));

                var dialog = new ContentDialog
                {
                    Title = "Błędne dane autora",
                    Content = $"Niektóre rekordy mają niepoprawne dane:\n{msg}\n\nUzupełnij dane przed kontynuowaniem.",
                    CloseButtonText = "OK"
                };

                await dialog.ShowAsync();
                e.Cancel = true;
                return;
            }

            dbUWP.AuthorsLst = AuthorsViewModel.AutorzyObservable.ToList();
            dbUWP.SaveAuthors();
            base.OnNavigatingFrom(e);
        }

        public async Task<bool> CzyDanePoprawne()
        {
            var komunikaty = new List<string>();

            foreach (var autor in AuthorsViewModel.AutorzyObservable)
            {
                var blad = $"ID {autor.id}:";
                bool maBlad = false;

                if (string.IsNullOrWhiteSpace(autor.imie))
                {
                    blad += " brak imienia";
                    maBlad = true;
                }

                if (string.IsNullOrWhiteSpace(autor.nazwisko))
                {
                    blad += (maBlad ? "," : "") + " brak nazwiska";
                    maBlad = true;
                }

                if (autor.rokUr <= 0 || autor.rokUr > DateTime.Now.Year)
                {
                    blad += (maBlad ? "," : "") + " niepoprawny rok";
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
                    Title = "Błąd walidacji danych autorów",
                    Content = string.Join("\n", komunikaty),
                    CloseButtonText = "OK"
                }.ShowAsync();

                return false;
            }

            return true;
        }

    }
}
