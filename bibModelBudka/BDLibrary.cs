using bibModelBudka.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace bibModelBudka
{
    
    public class BDLibrary
    {
        public readonly string authorsFile, publishersFile, booksFile;
        public BDLibrary(string path, string authorsFile=DefaultFileNames.Authors, string publishersFile = DefaultFileNames.Publishers, string booksFile = DefaultFileNames.Books)
        {
            this.authorsFile = path+authorsFile;
            this.publishersFile = path+publishersFile;
            this.booksFile = path+booksFile;
        }

        public bool TestData()
        {
            var authors = new bibModelBudka.Model.Autorzy();
            authors.Autor = new bibModelBudka.Model.AutorzyAutor[] {
            new bibModelBudka.Model.AutorzyAutor()
            {
                id = 1,
                nazwisko = "Prus",
                imie = "Bolesław",
                rokUr = 1847
            },
            new bibModelBudka.Model.AutorzyAutor()
            {
                id = 2,
                nazwisko = "Mickiewicz",
                imie = "Adam",
                rokUr = 1789
            },
            new bibModelBudka.Model.AutorzyAutor()
            {
                id = 3,
                nazwisko = "Wyspiański",
                imie = "Stanisław",
                rokUr = 1789
            },
            new bibModelBudka.Model.AutorzyAutor()
            {
                id = 4,
                nazwisko = "Herling-Grudziński",
                imie = "Gustaw",
                rokUr = 1789
            },
            new bibModelBudka.Model.AutorzyAutor()
            {
                id = 5,
                nazwisko = "Lem",
                imie = "Stanisław",
                rokUr = 1789
            },
            new bibModelBudka.Model.AutorzyAutor()
            {
                id = 5,
                nazwisko = "Kapuściński",
                imie = "Ryszard",
                rokUr = 1789
            },
            new bibModelBudka.Model.AutorzyAutor()
            {
                id = 6,
                nazwisko = "Stanisław Reymont",
                imie = "Władysław",
                rokUr = 1789
            },
            new bibModelBudka.Model.AutorzyAutor()
            {
                id = 7,
                nazwisko = "Sienkiewicz",
                imie = "Henryk",
                rokUr = 1789
            },
            new bibModelBudka.Model.AutorzyAutor()
            {
                id = 8,
                nazwisko = "Budka",
                imie = "Bartosz",
                rokUr = 2000
            }
        };
            bool sukces = SerializujDane(authors, authorsFile);
            if (!sukces) return sukces;
            //wydawnictwa - analogicznie do zrobienia
            var publishers = new bibModelBudka.Model.Wydawcy();
            publishers.Wydawca = new bibModelBudka.Model.WydawcyWydawca[]
            {
                new bibModelBudka.Model.WydawcyWydawca()
                {
                    id=1,
                    nazwa="Dom Wydawniczy Rebis",
                    strona="https://www.rebis.com.pl/"
                },
                new bibModelBudka.Model.WydawcyWydawca()
                {
                    id=2,
                    nazwa="Wydawnictwo Albatros",
                    strona="https://www.wydawnictwoalbatros.com/"
                },
                new bibModelBudka.Model.WydawcyWydawca()
                {
                    id=3,
                    nazwa="Wydawnictwo Czarne",
                    strona="https://czarne.com.pl/"
                },
                new bibModelBudka.Model.WydawcyWydawca()
                {
                    id=4,
                    nazwa="Wydawnictwo Literackie",
                    strona="https://www.wydawnictwoliterackie.pl/"
                },
                new bibModelBudka.Model.WydawcyWydawca()
                {
                    id=5,
                    nazwa="Fabryka Słów",
                    strona="https://fabrykaslow.com.pl/"
                }
            };
            sukces = SerializujDane(publishers, publishersFile);
            if (!sukces) return sukces;
            //książki - nowy też przerobić najlepiej
            var books = new bibModelBudka.Model.Ksiazki();
            books.Ksiazka = new bibModelBudka.Model.KsiazkiKsiazka[]
            {
                new bibModelBudka.Model.KsiazkiKsiazka()
                {
                    id=1,
                    IdAutora=1,
                    tytul="Lalka",
                    rok_wydania=2000,
                    IdWydawcy=1
                },
                new bibModelBudka.Model.KsiazkiKsiazka()
                {
                    id=2,
                    IdAutora=3,
                    tytul="Wesele",
                    rok_wydania=2000,
                    IdWydawcy=1
                },
                new bibModelBudka.Model.KsiazkiKsiazka()
                {
                    id=3,
                    IdAutora=1,
                    tytul="Placówka",
                    rok_wydania=2000,
                    IdWydawcy=1
                },
                new bibModelBudka.Model.KsiazkiKsiazka()
                {
                    id=4,
                    IdAutora=4,
                    tytul="Inny świat",
                    rok_wydania=2000,
                    IdWydawcy=1
                },
                new bibModelBudka.Model.KsiazkiKsiazka()
                {
                    id=5,
                    IdAutora=5,
                    tytul="Powrót z gwiazd",
                    rok_wydania=2000,
                    IdWydawcy=1
                },
                new bibModelBudka.Model.KsiazkiKsiazka()
                {
                    id=6,
                    IdAutora=6,
                    tytul="Heban",
                    rok_wydania=2000,
                    IdWydawcy=5
                },
                new bibModelBudka.Model.KsiazkiKsiazka()
                {
                    id=7,
                    IdAutora=1,
                    tytul="Faraon",
                    rok_wydania=2000,
                    IdWydawcy=4
                },
                new bibModelBudka.Model.KsiazkiKsiazka()
                {
                    id=8,
                    IdAutora=6,
                    tytul="Ziemia obiecana",
                    rok_wydania=2000,
                    IdWydawcy=3
                },
                new bibModelBudka.Model.KsiazkiKsiazka()
                {
                    id=9,
                    IdAutora=7,
                    tytul="Ogniem i mieczem",
                    rok_wydania=2000,
                    IdWydawcy=2
                },
                new bibModelBudka.Model.KsiazkiKsiazka()
                {
                    id=10,
                    IdAutora=8,
                    tytul="Dzieła zebrane",
                    rok_wydania=2000,
                    IdWydawcy=2
                }
            };
            sukces = SerializujDane(books, booksFile);
            if (!sukces) return sukces;
            /*
            var doc = new XDocument(
            new XDeclaration("1.0", "utf-8", "no"),
            new XComment("Przykładowe dane-książki.xml"),
            new XElement("Ksiazki",
                new XAttribute("wersja", "2.0"),
                new XElement("Ksiazka",
                    new XAttribute("id", "1"),
                    new XAttribute("IdAutora", "1"),
                    new XAttribute("tytul", "Lalka"),
                    new XAttribute("rok_wydania", "2010"),
                    new XAttribute("IdWydawcy", "1")
                    )
                )
            );
            doc.Save(booksFile);
            */


            return sukces;
        }

        private bool SerializujDane<T>(T data, string fileName)
        {
            //zapis do pliku  - serializacja
            var xs = new XmlSerializer(typeof(T));
            bool sukces = false;
            try
            {
                using (StreamWriter s = new StreamWriter(fileName))
                {
                    xs.Serialize(s, data);
                }
                ;
                sukces = true;
            }
            catch (Exception ex)
            {

                throw (ex);
            }

            return sukces;
        }
    }
}
