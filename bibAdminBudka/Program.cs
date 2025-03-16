// See https://aka.ms/new-console-template for more information
var db = new bibModelBudka.BDLibrary(
        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+"\\");
Console.WriteLine("plik z autorami: {0}", db.authorsFile);
Console.WriteLine("plik z książkami: {0}", db.booksFile);
Console.WriteLine("plik z wydawcami: {0}", db.publishersFile);
if (db.TestData())
{
    Console.WriteLine("pliki danych wygenerowane poprawnie");
}
else
{
    Console.WriteLine("problem z generowaniem plików danych");
}

