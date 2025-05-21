using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using bibModelBudka;
using Windows.UI.Xaml.Controls;

namespace bibKliBudka
{
    public class BDLibraryUWP
    {
        readonly string authorsFile, publishersFile, booksFile;
        readonly StorageFolder pathUWP;
        public BDLibraryUWP(string authorsFile = DefaultFileNames.Authors, string publishersFile = DefaultFileNames.Publishers, string booksFile = DefaultFileNames.Books, StorageFolder pathUWP=null) 
        {
            this.authorsFile = authorsFile;
            this.publishersFile = publishersFile;
            this.booksFile = booksFile;
            this.pathUWP = pathUWP;
            this.pathUWP = KnownFolders.DocumentsLibrary;
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

            return;
        }

    }
}
