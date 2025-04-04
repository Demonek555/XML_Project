﻿// See https://aka.ms/new-console-template for more information
using bibModelBudka.Model;

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

static void PokazAutorow(bibModelBudka.BDLibrary db)
{
    Console.WriteLine("\n========= AUTORZY ==========\n");
    var authors = db.ReportDataUniwersal<Autorzy>(db.authorsFile);
    string frm = "{0,4}| {1,-20}| {2,-10}| {3,7}";
    string head = string.Format(frm, "Id", "Nazwisko", "Imie", "Rok ur.");
    string kreska = new string('-', head.Length);
    Console.WriteLine(head + "\n" + kreska);
    foreach (var item in authors.Autor)
    {
        Console.WriteLine(frm, item.id, item.nazwisko, item.imie, item.rokUr);
    }
    Console.WriteLine(kreska);
}
static void PokazWydawnictwa(bibModelBudka.BDLibrary db)
{
    Console.WriteLine("\n========= WYDAWNICTWA ==========\n");
    var publishers = db.ReportDataUniwersal<Wydawcy>(db.publishersFile);
    string frm = "{0,4}| {1,-25}| {2,-40}";
    string head = string.Format(frm, "Id", "Nazwa", "Strona");
    string kreska = new string('-', head.Length);
    Console.WriteLine(head + "\n" + kreska);
    foreach (var item in publishers.Wydawca)
    {
        Console.WriteLine(frm, item.id, item.nazwa, item.strona);
    }
    Console.WriteLine(kreska);
}
static void PokazKsiazki(bibModelBudka.BDLibrary db)
{
    Console.WriteLine("\n========= KSIĄŻKI ==========\n");
    var booksExt = db.ReportDataLQ2();
    if (booksExt != null && booksExt.Count > 0)
    {
        string frm = "{0,4}| {1,-30}| {2,-30}| {3,-25}| {4,7}";
        string head = string.Format(frm, "Id", "Tytuł", "Autor", "Wydawnictwo", "Cena");
        string kreska = new string('-', head.Length);
        Console.WriteLine(head + "\n" + kreska);
        foreach (var item in booksExt)
        {
            Console.WriteLine(frm, item.id, item.tytul, item.NazwiskoImie, item.NazwaWydawnictwa, item.cena);
        }
        Console.WriteLine(kreska);
    }
    else
    {
        Console.WriteLine("*** BRAK DANYCH O KSIĄŻKACH ***");
    }
}


static void PokazKsiazkiAutora(bibModelBudka.BDLibrary db)
{
    Console.Write("Podaj nazwisko autora: ");
    string nazwiskoSzukane = Console.ReadLine().Trim().ToLower();

    var booksExt = db.ReportDataLQ2();

    if (booksExt != null && booksExt.Count > 0)
    {
        // Filtrowanie z użyciem operatora '=='
        var wynikDokladny = booksExt
            .Where(b => b.NazwiskoImie.Split(' ')[0].ToLower() == nazwiskoSzukane)
            .ToList();

        // Filtrowanie z użyciem Contains (np. fragment nazwiska)
        var wynikZawiera = booksExt
            .Where(b => b.NazwiskoImie.ToLower().Contains(nazwiskoSzukane))
            .ToList();

        string frm = "{0,4}| {1,-30}| {2,-30}| {3,-25}| {4,7}";
        string head = string.Format(frm, "Id", "Tytuł", "Autor", "Wydawnictwo", "Cena");
        string kreska = new string('-', head.Length);

        Console.WriteLine("\n--- Wyniki z '==' (dokładne dopasowanie) ---");
        if (wynikDokladny.Count > 0)
        {
            Console.WriteLine(head + "\n" + kreska);
            foreach (var item in wynikDokladny)
            {
                Console.WriteLine(frm, item.id, item.tytul, item.NazwiskoImie, item.NazwaWydawnictwa, item.cena);
            }
            Console.WriteLine(kreska);
        }
        else
        {
            Console.WriteLine("*** Brak książek dla dokładnie takiego nazwiska ***");
        }

        Console.WriteLine("\n--- Wyniki z 'Contains()' (częściowe dopasowanie) ---");
        if (wynikZawiera.Count > 0)
        {
            Console.WriteLine(head + "\n" + kreska);
            foreach (var item in wynikZawiera)
            {
                
                Console.WriteLine(frm, item.id, item.tytul, item.NazwiskoImie, item.NazwaWydawnictwa, item.cena);
            }
            Console.WriteLine(kreska);
        }
        else
        {
            Console.WriteLine("*** Brak książek zawierających taką frazę ***");
        }
    }
    else
    {
        Console.WriteLine("*** Brak danych o książkach ***");
    }
}

static void ShowData(bibModelBudka.BDLibrary db)
{
    /*
    //string dane = db.ReportData();
    //Console.WriteLine("\n=========WERSJA I==========\n");
    //Console.WriteLine("\n\n{0}", dane);

    //Autorzy
    Console.WriteLine("\n=========WERSJA II==========\n");
    //var authors = db.ReportData2();
    var authors = db.ReportDataUniwersal<Autorzy>(db.authorsFile);
    string frm = "{0,4}| {1,-20}| {2,-10}| {3,7}";
    string head = string.Format(frm, "Id", "Nazwisko", "Imie", "Rok ur.");
    string kreska = new string('-',head.Length);
    Console.WriteLine(head+ "\n"+kreska);
    foreach (var item in authors.Autor)
    {
        Console.WriteLine(frm, item.id, item.nazwisko, item.imie, item.rokUr);
    }
    Console.WriteLine(kreska);

    //Wydawnictwa
    Console.WriteLine("\n=========WERSJA II==========\n");
    //var publishers = db.ReportData3();
    var publishers = db.ReportDataUniwersal<Wydawcy>(db.publishersFile);
    string frm2 = "{0,4}| {1,-25}| {2,-40}";
    string head2 = string.Format(frm2, "Id", "Nazwa", "Strona");
    string kreska2 = new string('-', head2.Length);
    Console.WriteLine(head2 + "\n" + kreska2);
    foreach (var item in publishers.Wydawca)
    {
        Console.WriteLine(frm2, item.id, item.nazwa, item.strona);
    }
    Console.WriteLine(kreska2);

    //wersja 3
    Console.WriteLine("\n=========WERSJA III==========\n");
    var authorsSort = db.ReportDataLQ();
    if (authorsSort != null)
    {
        Console.WriteLine(head + "\n" + kreska);
        foreach (var item in authorsSort)
        {
            Console.WriteLine(frm, item.id, item.nazwisko, item.imie, item.rokUr);
        }
        Console.WriteLine(kreska);
    }
    else Console.WriteLine("*** BRAK DANYCH ***");
    Console.WriteLine("\n=========WERSJA III==========\n");
    var publishersSort = db.ReportDataLQ3();
    if (publishersSort != null)
    {
        Console.WriteLine(head2 + "\n" + kreska2);
        foreach (var item in publishersSort)
        {
            Console.WriteLine(frm2, item.id, item.nazwa, item.strona);
        }
        Console.WriteLine(kreska2);
    }
    else Console.WriteLine("*** BRAK DANYCH ***");
    Console.WriteLine("\n=========WERSJA III==========\n");
    var booksExt = db.ReportDataLQ2();
    if (booksExt != null && booksExt.Count > 0)
    {
        string frm3 = "{0,4}| {1,-30}| {2,-30}| {3,-25}| {4,7}";
        string head3 = string.Format(frm3, "Id", "Tytuł", "Autor", "Wydawnictwo", "Cena");
        string kreska3 = new string('-', head3.Length);
        Console.WriteLine(head3 + "\n" + kreska3);
        foreach (var item in booksExt)
        {
            Console.WriteLine(frm3, item.id, item.tytul, item.NazwiskoImie, item.NazwaWydawnictwa, item.cena);
        }
        Console.WriteLine(kreska3);
    }
    else
    {
        Console.WriteLine("*** BRAK DANYCH O KSIĄŻKACH ***");
    }
    */
    PokazAutorow(db);
    PokazWydawnictwa(db);
    PokazKsiazki(db);
}

while (true)
{
    Console.Clear();
    Console.WriteLine("Menu modułu bibAdminBudka\n");
    Console.WriteLine("W - wyświetl dane (wszystkie)");
    Console.WriteLine("A - Autorzy");
    Console.WriteLine("K - Książki");
    Console.WriteLine("P - Wydawnictwa");
    Console.WriteLine("O - Książki Autora");
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
            PokazAutorow(db);
            break;
        case ConsoleKey.K:
            Console.WriteLine("\nWyświetlam książki.");
            PokazKsiazki(db);
            break;
        case ConsoleKey.P:
            Console.WriteLine("\nWyświetlam Wydawnictwa.");
            PokazWydawnictwa(db);
            break;
        case ConsoleKey.O:
            Console.WriteLine("\nWyświetlam książki autora.");
            PokazKsiazkiAutora(db);
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