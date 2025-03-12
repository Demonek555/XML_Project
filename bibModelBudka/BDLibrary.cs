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
            }
        };
            bool sukces = SerializujDane(authors, authorsFile);
            if (!sukces) return sukces;
            //wydawnictwa - analogicznie do zrobienia
            //książki - nowy też przerobić najlepiej
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
