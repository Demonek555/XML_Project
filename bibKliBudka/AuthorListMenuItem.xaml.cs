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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace bibKliBudka
{
    //pomocnicza do danych
    class DataGridDataSourceAuthors
    {
        //public List<AutorzyAutor> Autorzy=new List<AutorzyAutor>();
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
                //Autorzy = dbUWP.AuthorsLst,
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


        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            //var x=AuthorsViewModel.Autorzy;
            //var x = AuthorsViewModel.AutorzyObservable;
            dbUWP.AuthorsLst = AuthorsViewModel.AutorzyObservable.ToList();
            dbUWP.SaveAuthors();
            base.OnNavigatingFrom(e);
        }
        
    }
}
