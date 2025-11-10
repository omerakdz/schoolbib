using System.Data;
using System.Runtime.InteropServices;
using static Bibje_Omer_Akdeniz.Book;

namespace Bibje_Omer_Akdeniz
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library("Omer's bibje");
            bool running = true;
            while (running)
            {
                Console.WriteLine("Maak je keuze:");
                Console.WriteLine("1. Voeg een boek toe op basis van titel en auteur.");
                Console.WriteLine("2. Voeg informatie toe aan een boek (ISBN, Genre, Prijs...)");
                Console.WriteLine("3. Toon info van een boek op basis van titel en auteur.");
                Console.WriteLine("4. Zoek een boek op verschillende manieren: ");
                Console.WriteLine("5. Verwijder een boek. ");
                Console.WriteLine("6. Toon alle boeken.");
                Console.WriteLine("7. CSV-bestand inlezen.");
                Console.WriteLine("8. Een krant of maandblad toe te voegen.");
                Console.WriteLine("9. Alle kranten tonen.");
                Console.WriteLine("10. Alle maandbladen tonen.");
                Console.WriteLine("11. De aanwinsten van de leeszaal op te vragen.");
                Console.WriteLine("12. Ontleen een boek.");
                Console.WriteLine("13. Breng een boek terug.");

                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        AddBookToLibrary(library);
                        break;
                    case 2:
                        AddInfoToBook(library);
                        break;
                    case 3:
                        ShowBookInfo(library);
                        break;
                    case 4:
                        SearchBookOptions(library);
                        break;
                    case 5:
                        RemoveBookFromLibrary(library);
                        break;
                    case 6:
                        ShowAllBooks(library);
                        break;
                    case 7:
                        library.DeserialiseBookFromCSV();
                        Console.WriteLine("Boeken zijn ingelezen uit het CSV-bestand.");
                        break;
                    case 8:
                        Console.WriteLine("Wil je een krant(1) of maandblad(2) toevoegen?");
                        int input = Convert.ToInt32(Console.ReadLine());

                        if (input == 1)
                        {
                            library.AddNewspaper();
                        } 
                        else if(input == 2)
                        {
                            library.AddMagazine();
                        }
                        else
                        {
                            Console.WriteLine("ongeligde keuze");
                        }

                        break;
                    case 9:
                        library.ShowAllNewspapers();
                        break;
                    case 10:
                        library.ShowAllMagazines();
                        break;
                    case 11:
                        library.AcquisitionsReadingRoomToday();
                        break;
                    case 12:
                        BorrowBook(library);
                        break;
                    case 13:
                        ReturnBook(library);
                        break;
                    case 14:
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Ongeldige keuze, probeer opnieuw.");
                        break;
                }
            }

            static void AddBookToLibrary(Library library)
            {
                try
                {
                    Console.Clear();
                    Console.Write("Voer de titel in: ");
                    string title = Console.ReadLine();
                    Console.Write("Voer de auteur in: ");
                    string author = Console.ReadLine();

                    if (title == "" || author == "")
                    {
                        throw new ArgumentException("Titel en auteur mogen niet leeg zijn.");
                    }

                    Book newBook = new Book(title, author, library);
                    Console.WriteLine($"Het boek {title} van {author} is toegevoegd.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Er is een onverwachte fout opgetreden: {ex.Message}");
                }
            }

            static void AddInfoToBook(Library library)
            {
                try
                {
                    Console.Clear();
                    Console.Write("Voer de titel van het boek in: ");
                    string title = Console.ReadLine();
                    Console.Write("Voer de auteur in: ");
                    string author = Console.ReadLine();

                    // Boek zoeken
                    Book bookToEdit = null;
                    foreach (var book in library.BookList)
                    {
                        if (book.Title == title && book.Author == author)
                        {
                            bookToEdit = book;
                        }
                    }
                    if (bookToEdit != null)
                    {
                        Console.Write("Geef het ISBN van het boek (5 tekens): ");
                        string isbn = Console.ReadLine();
                        if (isbn.Length != 5)
                        {
                            throw new ArgumentException("ISBN moet  5 tekens lang zijn.");
                        }

                        bookToEdit.Isbn = isbn;

                        Console.Write("Geef het genre van het boek: ");
                        Console.WriteLine();
                        Genre[] genres = (Genre[])Enum.GetValues(typeof(Genre));
                        for (int i = 0; i < genres.Length; i++)
                        {
                            Console.WriteLine($"{i + 1}. {genres[i]}");
                        }

                        string input = Console.ReadLine();
                        int genreIndex = Convert.ToInt32(input);
                        Genre genre;

                        if (genreIndex < 1 || genreIndex > genres.Length)
                        {
                            Console.WriteLine($"Ongeldige keuze. Genre wordt ingesteld op Fiction.");
                            genre = Genre.Fiction;
                        }
                        else
                        {
                            genre = genres[genreIndex - 1];
                        }

                        bookToEdit.BookGenre = genre;
                        Console.WriteLine($"Genre ingesteld op: {genre}");
                    }
                    else
                    {
                        Console.WriteLine("Boek niet gevonden.");
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Ongeldige invoer: {ex.Message}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Fout bij invoer: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Er is een onverwachte fout opgetreden: {ex.Message}");
                }
            }

            static void ShowBookInfo(Library library)
            {
                Console.Clear();
                Console.Write("Geef de titel van het boek: ");
                string title = Console.ReadLine();
                Console.Write("Geef de auteur van het boek: ");
                string author = Console.ReadLine();

                bool bookFound = false;
                foreach (var book in library.BookList)
                {
                    if (book.Title.ToLower() == title.ToLower() && book.Author.ToLower() == author.ToLower())
                    {
                        Console.WriteLine("Gevonden boek:");
                        book.BookInfo();
                        bookFound = true;
                        break;
                    }
                }
                if (!bookFound)
                {
                    Console.WriteLine("Boek niet gevonden.");
                }
            }

            static void SearchBookOptions(Library library)
            {
                Console.Clear();
                Console.WriteLine("Zoek een boek op basis van:");
                Console.WriteLine("1. Titel en Auteur");
                Console.WriteLine("2. ISBN");
                Console.WriteLine("3. Auteur");
                Console.WriteLine("4. Genre");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Geef de titel:  ");
                        string title = Console.ReadLine();
                        Console.Write("Geef de auteur: ");
                        string author = Console.ReadLine();
                        library.SearchBookByTitleAndAuthor(title, author);
                        break;
                    case "2":
                        Console.Write("Geef het ISBN: ");
                        string isbn = Console.ReadLine();
                        Book book = library.SearchBookByIsbn(isbn);
                        if (book == null)
                        {
                            Console.WriteLine("boek bestaat niet");
                        }
                        else
                        {
                            Console.WriteLine("Gevonden boek:");
                            book.BookInfo();
                        }
                        break;

                    case "3":
                        Console.Write("Geef de naam van de auteur: ");
                        string authorSearch = Console.ReadLine();
                        library.SearchAllBooksOfAuthor(authorSearch);
                        break;

                    case "4":
                        Console.Write("Geef het genre:");
                        Console.WriteLine();
                        Genre[] genres = (Genre[])Enum.GetValues(typeof(Genre));
                        for (int i = 0; i < genres.Length; i++)
                        {
                            Console.WriteLine($"{i+1}. {genres[i]}");
                        }
   
                        int keuze = Convert.ToInt32(Console.ReadLine());

                        if (keuze < 1 || keuze > genres.Length) 
                        {
                            Console.WriteLine("Ongeldige keuze. Kies een geldig genre.");
                            return;
                        }

                        Genre genre = genres[keuze - 1];

                        library.SearchBookByGenre(genre);

                        break;
                    default:
                        Console.WriteLine("Ongeldige keuze.");
                        break;
                }
            }

            static void RemoveBookFromLibrary(Library library)
            {
                Console.Clear();
                Console.Write("Voer de titel in van het boek dat je wilt verwijderen: ");
                string title = Console.ReadLine();
                Console.Write("Voer de auteur in: ");
                string author = Console.ReadLine();

                
                Book bookToRemove = null; 
                foreach (var book in library.BookList)
                {
                    if (book.Title == title && book.Author == author)
                    {
                        bookToRemove = book;
                    }
                }

                if (bookToRemove != null)
                {
                    library.RemoveBook(bookToRemove); 
                }
                else
                {
                    Console.WriteLine("Boek niet gevonden.");
                }
            }  

            static void ShowAllBooks(Library library)
            {
                Console.Clear();
                bool foundBook = false;
                foreach (var book in library.BookList)
                {
                    string borrowedStatus;

                    if (book.IsBorrowed)
                    {
                        borrowedStatus = "Uitgeleend";
                    }
                    else
                    {
                        borrowedStatus = "Beschikbaar";
                    }

                    Console.WriteLine($"{book.Title} ~ {book.Author} ~ Prijs: {book.Price:F2} ~ Status: {borrowedStatus}");
                    foundBook = true;
                }

                if (!foundBook)
                {
                    Console.WriteLine("Geen boeken gevonden. ");
                }
                Console.WriteLine();
            }

            static void BorrowBook(Library library)
            {
                Console.Clear();
                Console.Write("Titel van het boek: ");
                string title = Console.ReadLine();
                Console.Write("Auteur van het boek: ");
                string author = Console.ReadLine();

                Book bookToBorrow = null;

                foreach (Book book in library.BookList)
                {
                    if (book.Title == title && book.Author == author)
                    {
                        bookToBorrow = book;
                        break;
                    }
                }

                if (bookToBorrow == null)
                {
                    Console.WriteLine($"Boek niet gevonden.");
                    return;
                }

                if (bookToBorrow.IsBorrowed)
                {
                    Console.WriteLine($"het boek '{bookToBorrow.Title}' is al uitgeleend.");
                }
                else
                {
                    bookToBorrow.IsBorrowed = true;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"het boek '{bookToBorrow.Title}' is succesvol uitgeleend.");
                    Console.ResetColor();
                }
            }

            static void ReturnBook(Library library)
            {
                Console.Clear();
                Console.Write("Titel van het boek: ");
                string title = Console.ReadLine();
                Console.Write("Auteur van het boek: ");
                string author = Console.ReadLine();

                Book bookToReturn = null;

                foreach (Book book in library.BookList)
                {
                    if (book.Title == title && book.Author == author)
                    {
                        bookToReturn = book;
                        break;
                    }
                }

                if (bookToReturn == null)
                {
                    Console.WriteLine("Boek niet gevonden.");
                    return;
                }

                if (!bookToReturn.IsBorrowed)
                {
                    Console.WriteLine("Dit boek was niet uitgeleend.");
                }
                else
                {
                    bookToReturn.IsBorrowed = false;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Het boek '{bookToReturn.Title}' is succesvol teruggebracht.");
                    Console.ResetColor();
                }
            }
        }
    }
}
