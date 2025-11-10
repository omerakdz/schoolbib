using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static Bibje_Omer_Akdeniz.Book;

namespace Bibje_Omer_Akdeniz
{
    internal class Library
    {
        // CW ZOVEEL MOGELIJK IN PROGRAM
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private List<Book> bookList;
        public List<Book> BookList
        {
            get { return bookList; }
            set { bookList = value; }
        }

        private Dictionary<DateTime, ReadingRoomItem> allReadingRoom;
        public Dictionary<DateTime, ReadingRoomItem> AllReadingRoom
        {
            get { return allReadingRoom; }
        }

        public Library(string name) 
        {
            Name = name;
            BookList = new List<Book>();
            allReadingRoom = new Dictionary<DateTime, ReadingRoomItem>();
        }
        public void AddBook(Book book)
        {
            bool isDuplicate = false;

            foreach (var b in BookList)
            {
                if (b.Isbn == book.Isbn)
                {
                    isDuplicate = true;
                    break;  
                }
            }

            if (!isDuplicate)
            {
                BookList.Add(book);
            }
          
        }
        public void RemoveBook(Book book)
        {
            if (BookList.Contains(book))
            {
                BookList.Remove(book);
                Console.WriteLine($"Het boek {book.Title} is succesvol verwijderd.");
            }
            else
            {
                Console.WriteLine("Boek niet gevonden.");
            }
        }

        public void SearchBookByTitleAndAuthor(string title, string author)
        {
            bool bookFound = false;
            foreach (var book in BookList)
            {
                if (book.Title == title && book.Author == author)
                {
                    Console.WriteLine("Gevonden boek:");
                    Console.WriteLine($"{book.Title}  ~{book.Author}");
                    bookFound = true;
                }
            }

            if (!bookFound)
            {
                Console.WriteLine("Boek niet gevonden");
            }
        }

        public Book SearchBookByIsbn(string isbn)
        {
            bool bookFound = false;
            foreach (var book in BookList)
            {
                if (book.Isbn == isbn)
                {

                    bookFound = true;
                    return book;
                }
            }
            return null;
        }

        public void SearchAllBooksOfAuthor(string author)
        {
            int counter = 0;
            bool bookFound = false;
            foreach (var book in BookList)
            {
                if (book.Author == author)
                {
                    counter++;
                    Console.WriteLine("Gevonden boeken:");
                    Console.WriteLine($"{counter}. {book.Title} - {book.Author}");
                    bookFound = true;
                }
            }

            if (!bookFound)
            {
                Console.WriteLine("Boek niet gevonden");
            }
        }

        public void SearchBookByGenre(Book.Genre genre)
        {
            int counter = 0;
            bool bookFound = false;
            foreach (var book in BookList)
            {
                if (book.BookGenre == genre)
                {
                    counter++;
                    Console.WriteLine("Gevonden boeken:");
                    Console.WriteLine($"{counter}. {book.Title} - {book.BookGenre}");
                    bookFound = true;
                }
            }

            if (!bookFound)
            {
                Console.WriteLine("Boek niet gevonden");
            }
        }

        public void DeserialiseBookFromCSV()
        {
            string filePath = "boeken.csv"; 

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Bestand niet gevonden!");
            }

            string[] lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                string[] columns = line.Split(',');

                string title = columns[0];
                string author = columns[1];
                string isbn = columns[2];
                string publisher = columns[3];

                int publicationYear = Convert.ToInt32(columns[4]);
                int pages = Convert.ToInt32(columns[5]);
                double price = Convert.ToDouble(columns[6].Replace(".", ",")); 

                Genre genre = Enum.Parse<Genre>(columns[7], true); 

                Book newBook = new Book(title, author, isbn, publisher, publicationYear, pages, price, genre, this);

                AddBook(newBook);
                
               

                //AddBook(newBook);

            }
        }

        public void AddNewspaper()
        {
            Console.WriteLine("Wat is de naam van de krant?");
            string name = Console.ReadLine();

            Console.WriteLine("Wat is de datum van de krant? (dd/mm/jjjj)");
            DateTime date = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine("Wat is de uitgeverij van de krant?");
            string publisher = Console.ReadLine();

            NewsPaper newspaper = new NewsPaper(name, publisher, date);

            AllReadingRoom.Add(date, newspaper);

            if (AllReadingRoom.ContainsKey(date))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("de krant is succesvol toegevoegd.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Er is een fout opgetreden bij het toevoegen van de krant.");
                Console.ResetColor();
            }
            Console.WriteLine();
        }

        public void AddMagazine()
        {
            Console.WriteLine("Wat is de naam van het maandblad?");
            string name = Console.ReadLine();

            Console.WriteLine("Wat is de maand van het maandblad? (1-12)");
            byte month = Convert.ToByte(Console.ReadLine());

            if (month < 1 || month > 12)
            {
                Console.WriteLine("Kies tussen de 1 en 12");
                return;
            }

            Console.WriteLine("Wat is het jaar van het maandblad?");
            uint year = Convert.ToUInt32(Console.ReadLine());

            Console.WriteLine("Wat is de uitgeverij van het maandblad?");
            string publisher = Console.ReadLine();

            Magazine newMagazine = new Magazine(name,publisher, month, year);

            DateTime date = new DateTime((int)year, month, 1);

            AllReadingRoom.Add(date,newMagazine);

            if (AllReadingRoom.ContainsKey(date))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Het maandblad is succesvol toegevoegd.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Er is een fout opgetreden bij het toevoegen van het maandblad.");
                Console.ResetColor();
            }
            Console.WriteLine();
        }

        public void ShowAllMagazines()
        {
            Console.Clear();
            Console.WriteLine("Alle maandbladen uit de leeszaal:");
            if (AllReadingRoom.Count == 0)
            {
                Console.WriteLine("Geen maandbladen beschikbaar.");
                return;
            }

            foreach (var item in AllReadingRoom)
            {
                if (item.Value is Magazine magazine) // (Magazine)item.Value = Magazine magazine
                {
                    Console.WriteLine($"- Data news van {magazine.Identification} van uitgeverij {magazine.Publisher}");
                }
            }
            Console.WriteLine();
        }

        public void ShowAllNewspapers()
        {
            Console.Clear();
            Console.WriteLine("Alle kranten in de leeszaal:");
            if (AllReadingRoom.Count == 0)
            {
                Console.WriteLine("Geen kranten beschikbaar.");
                return;
            }

            foreach (var item in AllReadingRoom)
            {
                if (item.Value is NewsPaper newspaper)
                {
                    string formattedDate = newspaper.Date.ToString("dd/MM/yyyy");
                    Console.WriteLine($"- {newspaper.Title} van {formattedDate} van uitgeverij {newspaper.Publisher}");
                }
            }
            Console.WriteLine();
        }

        public void AcquisitionsReadingRoomToday()
        {
            Console.Clear();
            DateTime today = DateTime.Today;
            Console.WriteLine($"Aantwinsten in de leeszaal van {today.ToString("dddd dd MMMM yyyy")}");


            if (AllReadingRoom.Count == 0)
            {
                Console.WriteLine("Geen aanwinsten gevonden");
                return;
            }

            foreach (var item in AllReadingRoom)
            {
                if (item.Value is Magazine magazine) 
                {
                    Console.WriteLine($"{magazine.Title} met id {magazine.Identification}");
                }
                if (item.Value is NewsPaper newsPaper)
                {
                    Console.WriteLine($"{newsPaper.Title} met id {newsPaper.Identification}");
                }
            }
            Console.WriteLine();
        }
    }
}
