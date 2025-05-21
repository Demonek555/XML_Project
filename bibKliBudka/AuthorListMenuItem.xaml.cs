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
        public AuthorListMenuItem()
        {
            AuthorsViewModel= new DataGridDataSourceAuthors()
            {
                Autorzy = new List<AutorzyAutor>()
                {
                    new AutorzyAutor()
                    {
                        id=1,
                        nazwisko="Kowalski",
                        imie="Jan",
                        rokUr=1980
                    },
                    new AutorzyAutor()
                    {
                        id=2,
                        nazwisko="Nowak",
                        imie="Anna",
                        rokUr=1990
                    },
                     new AutorzyAutor()
                    {
                        id=3,
                        nazwisko="Kowalski",
                        imie="Jan3",
                        rokUr=1990
                    },
                }
            };
            this.InitializeComponent();
        }
    }
}
