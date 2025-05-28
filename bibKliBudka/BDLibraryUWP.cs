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
        //dane odczytane z plików
        public List<AutorzyAutor> AuthorsLst;
        public List<WydawcyWydawca> PublishersLst;
        public List<KsiazkiKsiazka> BooksLst;   
        public BDLibraryUWP(string authorsFile = DefaultFileNames.Authors, string publishersFile = DefaultFileNames.Publishers, string booksFile = DefaultFileNames.Books, StorageFolder pathUWP=null) 
        {
            this.authorsFile = authorsFile;
            this.publishersFile = publishersFile;
            this.booksFile = booksFile;
            this.pathUWP = pathUWP;
            this.pathUWP = KnownFolders.DocumentsLibrary;
        }

        T Deserialize<T>(StorageFile file)
        {
            var xs=new XmlSerializer(typeof(T));
            try
            {
                using (Stream reader = file.OpenStreamForReadAsync().Result)
                {
                    return (T)xs.Deserialize(reader);
                }
            }
            catch (Exception)
            {
                return default;
            }
        }

        public async void TestData()
        {
            //bool jestOK = false;
            string kom = "";
            var itemAF=await pathUWP.TryGetItemAsync(authorsFile);
            if (itemAF == null)
            {
                kom += $"{authorsFile}, "; 
            }
            var itemPF = await pathUWP.TryGetItemAsync(publishersFile);
            if (itemPF == null)
            {
                kom += $"{publishersFile}, ";
            }
            var itemBF = await pathUWP.TryGetItemAsync(booksFile);
            if (itemBF == null)
            {
                kom += $"{booksFile}, ";
            }
            if (kom != "")
            {
                //problem z danymi
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
            //są pliki z danymi
            var lst = Deserialize<Autorzy>(itemAF as StorageFile).Autor;
            AuthorsLst = (from item in lst
                          orderby item.nazwisko
                          select item).ToList();

            return;
        }

        async void Serializuj()
        {
            var aut=new Autorzy() { Autor = AuthorsLst.ToArray() };
            var xs = new XmlSerializer(typeof(Autorzy));
            StorageFile nowy=await pathUWP.CreateFileAsync(authorsFile, CreationCollisionOption.ReplaceExisting);
            try
            {
                using (Stream writer = await nowy.OpenStreamForWriteAsync())
                {
                    xs.Serialize(writer, aut);
                }
            }
            catch (Exception ex)
            {
                //komunikat o błędzie
                var dlg = new ContentDialog()
                {
                    Title = ex.Message,
                    Content = ex,
                    CloseButtonText = "OK"
                };
                await dlg.ShowAsync();
            }
        }

        internal void Save()
        {
            Serializuj();
            //throw new NotImplementedException();
        }
    }
}
