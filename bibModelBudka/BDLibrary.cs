using bibModelBudka.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Linq;

namespace bibModelBudka
{

    public class BDLibrary
    {
        public readonly string authorsFile, publishersFile, booksFile;
        public BDLibrary(string path, string authorsFile = DefaultFileNames.Authors, string publishersFile = DefaultFileNames.Publishers, string booksFile = DefaultFileNames.Books)
        {
            this.authorsFile = path + authorsFile;
            this.publishersFile = path + publishersFile;
            this.booksFile = path + booksFile;
        }

        public bool TestData()
        {
            bool sukces = true;
            var authors = new bibModelBudka.Model.Autorzy();
            var publishers = new bibModelBudka.Model.Wydawcy();
            var books = new bibModelBudka.Model.Ksiazki();
            if (!File.Exists(authorsFile))
            {
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
                id = 6,
                nazwisko = "Kapuściński",
                imie = "Ryszard",
                rokUr = 1789
            },
            new bibModelBudka.Model.AutorzyAutor()
            {
                id = 7,
                nazwisko = "Stanisław Reymont",
                imie = "Władysław",
                rokUr = 1789
            },
            new bibModelBudka.Model.AutorzyAutor()
            {
                id = 8,
                nazwisko = "Sienkiewicz",
                imie = "Henryk",
                rokUr = 1789
            },
            new bibModelBudka.Model.AutorzyAutor()
            {
                id = 9,
                nazwisko = "Budka",
                imie = "Bartosz",
                rokUr = 2000
            }
        };
                sukces = SerializujDane(authors, authorsFile);
                if (!sukces) return sukces;
            }
            if (!File.Exists(publishersFile))
            {
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
            }
            if (!File.Exists(booksFile))
            {
                books.Ksiazka = new bibModelBudka.Model.KsiazkiKsiazka[]
            {
                new bibModelBudka.Model.KsiazkiKsiazka()
                {
                    id=1,
                    IdAutora=1,
                    tytul="Lalka",
                    rok_wydania=2000,
                    IdWydawcy=1,
                    ISBN=1111111111111,
                    cena=1.1m
                },
                new bibModelBudka.Model.KsiazkiKsiazka()
                {
                    id=2,
                    IdAutora=3,
                    tytul="Wesele",
                    rok_wydania=2000,
                    IdWydawcy=1,
                    ISBN=1111111111112,
                    cena=1.2m
                },
                new bibModelBudka.Model.KsiazkiKsiazka()
                {
                    id=3,
                    IdAutora=1,
                    tytul="Placówka",
                    rok_wydania=2000,
                    IdWydawcy=1,
                    ISBN=1111111111113,
                    cena=1.3m
                },
                new bibModelBudka.Model.KsiazkiKsiazka()
                {
                    id=4,
                    IdAutora=4,
                    tytul="Inny świat",
                    rok_wydania=2000,
                    IdWydawcy=1,
                    ISBN=1111111111114,
                    cena=1.4m
                },
                new bibModelBudka.Model.KsiazkiKsiazka()
                {
                    id=5,
                    IdAutora=5,
                    tytul="Powrót z gwiazd",
                    rok_wydania=2000,
                    IdWydawcy=1,
                    ISBN=1111111111115,
                    cena=1.5m
                },
                new bibModelBudka.Model.KsiazkiKsiazka()
                {
                    id=6,
                    IdAutora=6,
                    tytul="Heban",
                    rok_wydania=2000,
                    IdWydawcy=5,
                    ISBN=1111111111116,
                    cena=1.6m
                },
                new bibModelBudka.Model.KsiazkiKsiazka()
                {
                    id=7,
                    IdAutora=1,
                    tytul="Faraon",
                    rok_wydania=2000,
                    IdWydawcy=4,
                    ISBN=1111111111117,
                    cena=1.7m
                },
                new bibModelBudka.Model.KsiazkiKsiazka()
                {
                    id=8,
                    IdAutora=6,
                    tytul="Ziemia obiecana",
                    rok_wydania=2000,
                    IdWydawcy=3,
                    ISBN=1111111111118,
                    cena=1.8m
                },
                new bibModelBudka.Model.KsiazkiKsiazka()
                {
                    id=9,
                    IdAutora=7,
                    tytul="Ogniem i mieczem",
                    rok_wydania=2000,
                    IdWydawcy=2,
                    ISBN=1111111111119,
                    cena=1.9m
                },
                new bibModelBudka.Model.KsiazkiKsiazka()
                {
                    id=10,
                    IdAutora=8,
                    tytul="Dzieła zebrane",
                    rok_wydania=2000,
                    IdWydawcy=2,
                    ISBN=1111111111122,
                    cena=2.2m
                }
            };

                sukces = SerializujDane(books, booksFile);
                if (!sukces) return sukces;
            }


            return sukces;
        }

        private bool SerializujDane<T>(T data, string fileName)
        {

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



        public string ReportData()
        {
            string wynik = XDocument.Load(authorsFile).ToString();
            return wynik;
        }


        public T ReportDataUniwersal<T>(string fileName) where T : class
        {
            try
            {
                var xs = new XmlSerializer(typeof(T));
                using (var reader = new StreamReader(fileName))
                {
                    var data = xs.Deserialize(reader) as T;
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Autorzy ReportData2()
        {
            try
            {
                var xs = new XmlSerializer(typeof(Autorzy));
                var s = new StreamReader(authorsFile);
                var authors = xs.Deserialize(s) as Autorzy;
                return authors;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public Wydawcy ReportData3()
        {
            try
            {
                var xs = new XmlSerializer(typeof(Wydawcy));
                var s = new StreamReader(publishersFile);
                var wydawcy = xs.Deserialize(s) as Wydawcy;
                return wydawcy;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public IOrderedEnumerable<TItem> ReportDataLQUniwersal<T, TItem, TKey>(string filePath, Func<TItem, TKey> orderBySelector)
    where T : class, new()
        {
            try
            {
                var xs = new XmlSerializer(typeof(T));
                using (var s = new StreamReader(filePath))
                {
                    var data = xs.Deserialize(s) as T;
                    if (data == null)
                        throw new InvalidOperationException("Błąd deserializacji.");

                    var itemsProperty = typeof(T).GetProperties().FirstOrDefault(p => p.PropertyType.IsArray);
                    if (itemsProperty == null)
                        throw new InvalidOperationException("Nie znaleziono tablicy danych.");

                    var items = itemsProperty.GetValue(data) as IEnumerable<TItem>;
                    if (items == null)
                        throw new InvalidOperationException("Nie udało się pobrać danych.");

                    return items.OrderBy(orderBySelector);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
                throw;
            }
        }


        public IOrderedEnumerable<AutorzyAutor> ReportDataLQ()
        {
            try
            {
                var xs = new XmlSerializer(typeof(Autorzy));
                var s = new StreamReader(authorsFile);
                var authors = xs.Deserialize(s) as Autorzy;

                var sortLstAuthors = from item in authors.Autor
                                     orderby item.nazwisko
                                     select item;
                return sortLstAuthors;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public List<KsiazkiKsiazkaExt> ReportDataLQ2()
        {
            try
            {
                var sortLstAuthors = ReportDataLQ();

                var xsBooks = new XmlSerializer(typeof(Ksiazki));
                var sBooks = new StreamReader(booksFile);
                var books = xsBooks.Deserialize(sBooks) as Ksiazki;

                if (books == null || books.Ksiazka == null || books.Ksiazka.Count() == 0)
                    throw new Exception("Brak danych o książkach.");

                var xsPub = new XmlSerializer(typeof(Wydawcy));
                var sPub = new StreamReader(publishersFile);
                var publishers = xsPub.Deserialize(sPub) as Wydawcy;

                if (publishers == null || publishers.Wydawca == null || publishers.Wydawca.Count() == 0)
                    throw new Exception("Brak danych o wydawnictwach.");

                var sortLst = (
                    from book in books.Ksiazka
                    join author in sortLstAuthors on book.IdAutora equals author.id
                    join pub in publishers.Wydawca on book.IdWydawcy equals pub.id
                    orderby book.tytul
                    select new KsiazkiKsiazkaExt()
                    {
                        id = book.id,
                        tytul = book.tytul,
                        NazwiskoImie = author.nazwisko + " " + author.imie,
                        NazwaWydawnictwa = pub.nazwa,
                        cena = book.cena
                    }
                ).ToList();

                return sortLst;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public IOrderedEnumerable<WydawcyWydawca> ReportDataLQ3()
        {
            try
            {
                var xs = new XmlSerializer(typeof(Wydawcy));
                var s = new StreamReader(publishersFile);
                var wydawcy = xs.Deserialize(s) as Wydawcy;

                var sortLstPublishers = from item in wydawcy.Wydawca
                                        orderby item.nazwa
                                        select item;
                return sortLstPublishers;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
