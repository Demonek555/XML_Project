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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace bibKliBudka
{
    //pomocnicza do danych
    class DataGridDataSourceAuthors
    {
        public List<AutorzyAutor> Autorzy=new List<AutorzyAutor>();
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
                Autorzy = dbUWP.AuthorsLst
            };
            this.InitializeComponent();
        }
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            var x=AuthorsViewModel.Autorzy;
            dbUWP.Save();
            base.OnNavigatingFrom(e);
        }
    }
}
