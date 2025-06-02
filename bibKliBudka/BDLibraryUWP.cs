
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using bibModelBudka;
using Windows.UI.Xaml.Controls;
using bibModelBudka.Model;
using Windows.Networking.Vpn;
using System.Xml.Serialization;
using System.IO;

namespace bibKliBudka
{
    public class BDLibraryUWP
    {
        readonly string authorsFile, publishersFile, booksFile;
        readonly StorageFolder pathUWP;
        public List<AutorzyAutor> AuthorsLst;
        public List<WydawcyWydawca> PublishersLst;
        public List<KsiazkiKsiazka> BooksLst;
        public BDLibraryUWP(string authorsFile = DefaultFileNames.Authors, string publishersFile = DefaultFileNames.Publishers, string booksFile = DefaultFileNames.Books, StorageFolder pathUWP = null)
        {
            this.authorsFile = authorsFile;
            this.publishersFile = publishersFile;
            this.booksFile = booksFile;
            this.pathUWP = pathUWP;
            this.pathUWP = KnownFolders.DocumentsLibrary;
        }

        T Deserialize<T>(StorageFile file)
        {
            var xs = new XmlSerializer(typeof(T));
            try
            {
                using (Stream reader = file.OpenStreamForReadAsync().Result)
                {
                    return (T)xs.Deserialize(reader);
                }
            }
            catch
            {
                return default;
            }
        }


        public async void TestData()
        {
            string kom = "";
            var itemAF = await pathUWP.TryGetItemAsync(authorsFile);
            if (itemAF == null)
            {
                kom += $"{authorsFile}, ";
                var autorzy = new Autorzy
                {
                    Autor = new List<AutorzyAutor>
            {
                new AutorzyAutor { id = 1, imie = "Bartosz", nazwisko = "Budka", rokUr = 2000 },
                new AutorzyAutor { id = 2, imie = "Bolesław", nazwisko = "Prus", rokUr = 1847 }
            }.ToArray()
                };
                Serializuj(autorzy, authorsFile);
            }
            var itemPF = await pathUWP.TryGetItemAsync(publishersFile);
            if (itemPF == null)
            {
                kom += $"{publishersFile}, ";
                var wydawcy = new Wydawcy
                {
                    Wydawca = new List<WydawcyWydawca>
            {
                new WydawcyWydawca { id = 1, nazwa = "Rebis", strona = "https://rebis.com" },
                new WydawcyWydawca { id = 2, nazwa = "Albatros", strona = "https://albatros.com" }
            }.ToArray()
                };
                Serializuj(wydawcy, publishersFile);

            }
            var itemBF = await pathUWP.TryGetItemAsync(booksFile);
            if (itemBF == null)
            {
                kom += $"{booksFile}, ";
                var ksiazki = new Ksiazki
                {
                    Ksiazka = new List<KsiazkiKsiazka>
            {
                new KsiazkiKsiazka
                {
                    id = 1,
                    IdAutora = 1,
                    IdWydawcy = 1,
                    tytul = "Pan Tadeusz",
                    rok_wydania = 1834,
                    ISBN = 1234567890123,
                    cena = 29.99m
                },
                new KsiazkiKsiazka
                {
                    id = 2,
                    IdAutora = 2,
                    IdWydawcy = 2,
                    tytul = "Pan Włodzimierz",
                    rok_wydania = 1834,
                    ISBN = 1234567890123,
                    cena = 29.99m
                },
            }.ToArray()
                };
                Serializuj(ksiazki, booksFile);
            }
            if (kom != "")
            {
                var dlg = new ContentDialog()
                {
                    Title = "Problem z plikami danych",
                    Content = kom,
                    CloseButtonText = "OK"
                };
                await dlg.ShowAsync();
                App.Current.Exit();
                return;
            }
            try
            {
                var deserializedAuthors = Deserialize<Autorzy>(itemAF as StorageFile);
                if (deserializedAuthors == null || deserializedAuthors.Autor == null)
                    throw new Exception("Nie udało się odczytać danych autorów.");

                AuthorsLst = deserializedAuthors.Autor.OrderBy(item => item.nazwisko).ToList();

                var deserializedPublishers = Deserialize<Wydawcy>(itemPF as StorageFile);
                if (deserializedPublishers == null || deserializedPublishers.Wydawca == null)
                    throw new Exception("Nie udało się odczytać danych wydawnictw.");

                PublishersLst = deserializedPublishers.Wydawca.OrderBy(p => p.nazwa).ToList();

                var deserializedBooks = Deserialize<Ksiazki>(itemBF as StorageFile);
                if (deserializedBooks == null || deserializedBooks.Ksiazka == null)
                    throw new Exception("Nie udało się odczytać danych książek.");

                BooksLst = deserializedBooks.Ksiazka.OrderBy(b => b.tytul).ToList();
            }
            catch (Exception ex)
            {
                var dlg = new ContentDialog()
                {
                    Title = "Błąd w danych XML",
                    Content = $"Wystąpił problem podczas wczytywania danych.\nMożliwe, że jeden z plików ma nieprawidłową strukturę.\n\nSzczegóły:\n{ex.Message}",
                    CloseButtonText = "Zamknij aplikację"
                };
                await dlg.ShowAsync();
                App.Current.Exit();
            }


        }

        private async void Serializuj<T>(T obj, string fileName)
        {
            var xs = new XmlSerializer(typeof(T));
            try
            {
                StorageFile nowy = await pathUWP.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                using (Stream writer = await nowy.OpenStreamForWriteAsync())
                {
                    xs.Serialize(writer, obj);
                }
            }
            catch (Exception ex)
            {
                var dlg = new ContentDialog()
                {
                    Title = ex.Message,
                    Content = ex,
                    CloseButtonText = "OK"
                };
                await dlg.ShowAsync();
            }
        }

        public async void Save<TContainer, TBase>(List<TBase> dataList, string fileName, Func<TBase[], TContainer> containerFactory)
        {
            var xs = new XmlSerializer(typeof(TContainer));
            try
            {
                var container = containerFactory(dataList.ToArray());
                StorageFile nowy = await pathUWP.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                using (Stream writer = await nowy.OpenStreamForWriteAsync())
                {
                    xs.Serialize(writer, container);
                }
            }
            catch (Exception ex)
            {
                var dlg = new ContentDialog()
                {
                    Title = ex.Message,
                    Content = ex,
                    CloseButtonText = "OK"
                };
                await dlg.ShowAsync();
            }
        }
        internal void SaveAuthors()
        {
            Save<Autorzy, AutorzyAutor>(AuthorsLst, authorsFile, data => new Autorzy { Autor = data });
        }

        internal void SavePublishers()
        {
            Save<Wydawcy, WydawcyWydawca>(
                PublishersLst,
                publishersFile,
                data => new Wydawcy { Wydawca = data }
            );
        }


        internal void SaveBooks()
        {
            Save<Ksiazki, KsiazkiKsiazka>(BooksLst, booksFile, data => new Ksiazki { Ksiazka = data });
        }


    }
}


