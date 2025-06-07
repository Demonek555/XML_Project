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
using Microsoft.Toolkit.Uwp.UI.Controls;
using System.Threading.Tasks;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace bibKliBudka
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    class DataGridDataSourceBooks
    {
        public ObservableCollection<KsiazkiKsiazkaExt> BooksObservable { get; set; }
    }

    public sealed partial class BookListMenuItem : Page
    {
        BDLibraryUWP dbUWP;
        DataGridDataSourceBooks BooksViewModel;
        public BookListMenuItem()
        {
            this.InitializeComponent();
            dbUWP = (App.Current as App).db;

            var rozszerzone = (dbUWP.BooksLst ?? new List<KsiazkiKsiazka>()).Select(b => new KsiazkiKsiazkaExt
            {
                id = b.id,
                IdAutora = b.IdAutora,
                IdWydawcy = b.IdWydawcy,
                ISBN = b.ISBN,
                tytul = b.tytul,
                rok_wydania = b.rok_wydania,
                NazwiskoImie = dbUWP.AuthorsLst.FirstOrDefault(a => a.id == b.IdAutora)?.nazwisko + " " +
                               dbUWP.AuthorsLst.FirstOrDefault(a => a.id == b.IdAutora)?.imie,
                NazwaWydawnictwa = dbUWP.PublishersLst.FirstOrDefault(p => p.id == b.IdWydawcy)?.nazwa
            }).ToList();

            BooksViewModel = new DataGridDataSourceBooks()
            {
                BooksObservable = new ObservableCollection<KsiazkiKsiazkaExt>(rozszerzone)
            };
        }
        protected override async void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (!await CzyDanePoprawne())
            {
                e.Cancel = true;
                return;
            }

            dbUWP.BooksLst = BooksViewModel.BooksObservable
                .Select(b =>
                {
                    var autor = dbUWP.AuthorsLst.FirstOrDefault(a =>
                        $"{a.nazwisko} {a.imie}".ToUpperInvariant() == (b.NazwiskoImie ?? "").Trim().ToUpperInvariant());

                    var wydawnictwo = dbUWP.PublishersLst.FirstOrDefault(w =>
                        w.nazwa.ToUpperInvariant() == (b.NazwaWydawnictwa ?? "").Trim().ToUpperInvariant());

                    return new KsiazkiKsiazka
                    {
                        id = b.id,
                        tytul = b.tytul,
                        ISBN = b.ISBN,
                        rok_wydania = b.rok_wydania,
                        IdAutora = autor?.id ?? 0,
                        IdWydawcy = wydawnictwo?.id ?? 0
                    };
                }).ToList();

            dbUWP.SaveBooks();
            base.OnNavigatingFrom(e);
        }




        private async void DodajKsiazke_Click(object sender, RoutedEventArgs e)
        {
            if (dbUWP.AuthorsLst.Count == 0 || dbUWP.PublishersLst.Count == 0)
            {
                await new ContentDialog
                {
                    Title = "Brak danych",
                    Content = "Nie można dodać książki — brak autorów lub wydawnictw.",
                    CloseButtonText = "OK"
                }.ShowAsync();
                return;
            }

            int nextId = BooksViewModel.BooksObservable.Count > 0
                ? BooksViewModel.BooksObservable.Max(b => b.id) + 1
                : 1;

            var nowa = new KsiazkiKsiazkaExt
            {
                id = nextId,
                IdAutora = dbUWP.AuthorsLst.First().id,
                IdWydawcy = dbUWP.PublishersLst.First().id,
                ISBN = 0,
                tytul = "Nowa książka",
                rok_wydania = (ushort)DateTime.Now.Year,
                NazwiskoImie = "",
                NazwaWydawnictwa = ""
            };

            BooksViewModel.BooksObservable.Insert(0, nowa);
        }


        private async void UsunKsiazke_Click(object sender, RoutedEventArgs e)
        {
            int indeks = dataGrid.SelectedIndex;
            if (indeks >= 0 && indeks < BooksViewModel.BooksObservable.Count)
            {
                BooksViewModel.BooksObservable.RemoveAt(indeks);
            }
            else
            {
                await new ContentDialog
                {
                    Title = "Błąd",
                    Content = "Zaznacz książkę do usunięcia.",
                    CloseButtonText = "OK"
                }.ShowAsync();
            }
        }
        public async Task<bool> CzyDanePoprawne()
        {
            var komunikaty = new List<string>();

            foreach (var ks in BooksViewModel.BooksObservable)
            {
                var linia = $"ID {ks.id}:";
                bool maBlad = false;

                if (string.IsNullOrWhiteSpace(ks.tytul))
                {
                    linia += " brak tytułu";
                    maBlad = true;
                }

                if (ks.ISBN.ToString().Length != 13)
                {
                    linia += (maBlad ? "," : "") + " ISBN musi mieć 13 cyfr";
                    maBlad = true;
                }

                var autor = dbUWP.AuthorsLst.FirstOrDefault(a =>
                    $"{a.nazwisko} {a.imie}".ToUpperInvariant() == (ks.NazwiskoImie ?? "").Trim().ToUpperInvariant());

                if (autor == null)
                {
                    linia += (maBlad ? "," : "") + " autor nie istnieje";
                    maBlad = true;
                }

                var wydawnictwo = dbUWP.PublishersLst.FirstOrDefault(w =>
                    w.nazwa.ToUpperInvariant() == (ks.NazwaWydawnictwa ?? "").Trim().ToUpperInvariant());

                if (wydawnictwo == null)
                {
                    linia += (maBlad ? "," : "") + " wydawnictwo nie istnieje";
                    maBlad = true;
                }

                if (maBlad)
                {
                    komunikaty.Add(linia);
                }
            }

            if (komunikaty.Any())
            {
                await new ContentDialog
                {
                    Title = "Niepoprawne dane książek",
                    Content = string.Join("\n", komunikaty),
                    CloseButtonText = "OK"
                }.ShowAsync();

                return false;
            }

            return true;
        }



    }
}
