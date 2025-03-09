// See https://aka.ms/new-console-template for more information
var autor1 = new bibModelBudka.Model.AutorzyAutor()
{
    id = 1,
    nazwisko = "Prus",
    imie = "Bolesław",
    rokUr = 1847
};
var autor2 = new bibModelBudka.Model.AutorzyAutor()
{
    id = 2,
    nazwisko = "Mickiewicz",
    imie = "Adam",
    rokUr = 1789
};
var DokumentAutor = new bibModelBudka.Model.Autorzy()
{
   Autor=new bibModelBudka.Model.AutorzyAutor[] {autor1,autor2}
};
var nazw1 = DokumentAutor.Autor[0].nazwisko; //Prus
var nazw2 = DokumentAutor.Autor[1].nazwisko; //Mickiewicz
