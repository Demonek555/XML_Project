// See https://aka.ms/new-console-template for more information
var db = new bibModelBudka.BDLibrary(
        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\");
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

static void ShowData(bibModelBudka.BDLibrary db)
{
    string dane = db.ReportData();
    Console.WriteLine("\n\n{0}", dane);
    Console.WriteLine("\n=========WERSJA II==========\n");
    var authors = db.ReportData2();
    string frm = "{0,4}| {1,-20}| {2,-10}| {3,7}";
    string head = string.Format(frm, "Id", "Nazwisko", "Imie", "Rok ur.");
    string kreska = new string('-',head.Length);
    Console.WriteLine(head+ "\n"+kreska);
    /*
    for (int i = 0; i < authors.Autor.Length; i++)
    {
        var item = authors.Autor[i];
        Console.WriteLine(frm, item.id, item.nazwisko, item.imie, item.rokUr);
    } */
    foreach (var item in authors.Autor)
    {
        Console.WriteLine(frm, item.id, item.nazwisko, item.imie, item.rokUr);
    }
    Console.WriteLine(kreska);
    Console.WriteLine("\n=========WERSJA III==========\n");
    var authorsSort = db.ReportDataLQ();
    if (authorsSort != null)
    {
        Console.WriteLine(head + "\n" + kreska);
        foreach (var item in authorsSort)
        {
            Console.WriteLine(frm, item.id, item.nazwisko, item.imie, item.rokUr);
        }
        Console.WriteLine(head + "\n" + kreska);
    }
    else Console.WriteLine("*** BRAK DANYCH ***");
    Console.WriteLine(head + "\n" + kreska);
}

while (true)
{
    Console.Clear();
    Console.WriteLine("Menu modułu bibAdminBudka\n");
    Console.WriteLine("W - wyświetl dane (wszystkie)");
    Console.WriteLine("A - Dane autora");
    Console.WriteLine("K - książki");
    Console.WriteLine("X - koniec\n");
    Console.Write("  Wybierz opcję: ");

    var key = Console.ReadKey(true).Key;

    switch (key)
    {
        case ConsoleKey.W:
            Console.WriteLine("\nWyświetlam wszystkie dane.");
            ShowData(db);
            break;
        case ConsoleKey.A:
            Console.WriteLine("\nWyświetlam dane autora.");
            break;
        case ConsoleKey.K:
            Console.WriteLine("\nWyświetlam książki.");
            break;
        case ConsoleKey.X:
            Console.WriteLine("\nKoniec programu.");
            return;
        default:
            Console.WriteLine("\nNieprawidłowy klawisz!");
            break;
    }
    Console.WriteLine("\nNaciśnij dowolny przycisk, żeby wrócić do menu");
    Console.ReadKey();
}

//