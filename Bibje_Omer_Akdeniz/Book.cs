using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Bibje_Omer_Akdeniz.InvalidTitleException;

namespace Bibje_Omer_Akdeniz
{
    internal class Book : ILendable
    {
        public enum Genre
        {
            Fiction,
            NonFiction,
            Mystery,
            Fantasy,
            ScienceFiction,
            Biography,
            History,
            Romance,
            Horror,
            Schoolbook
        }

        private string isbn;
        public string Isbn
        {
            get { return isbn; }
            set 
            {
                if (value.Length != 5)
                {
                    throw new InvalidIsbnException("ISBN moet uit exact 5 cijfers bestaan.");
                }

                isbn = value;
            }
        }


        private Genre bookGenre;
        public Genre BookGenre
        {
            get { return bookGenre; }
            set { bookGenre = value; }
        }


        private string title;
        public string Title
        {
            get { return title; }
            set 
            {
                if (value.Trim() == "")
                {
                    throw new InvalidTitleException("Titel mag niet leeg zijn");
                }

                title = value;
            }
        }


        private string author;
        public string Author
        {
            get { return author; }
            set 
            {
                if (value.Trim() == "")
                {
                    throw new ArgumentException("Auteur mag niet leeg zijn.");
                }

                author = value;
            }
        }


        private string publisher;
        public string Publisher
        {
            get { return publisher; }
            set 
            {
                if (value.Trim() == "")
                {
                    Console.WriteLine("De uitgeverij mag niet leeg zijn.");
                }

                publisher = value;
            }
        }

        private int publicationYear;
        public int PublicationYear
        {
            get { return publicationYear; }
            set 
            {
                if (value > DateTime.Now.Year)
                {
                    throw new ArgumentException("Kies een geldig jaartal.");
                }

                publicationYear = value;
            }
        }

        private int page;
        public int Page
        {
            get { return page; }
            set 
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Aantal pagina's moet groter dan 0 zijn.");
                }
                page = value;
            }
        }

        private double price;
        public double Price
        {
            get { return price; }
            set 
            {
                if (value < 0)
                {
                    throw new ArgumentException("Geef een geldige bedrag in.");
                }

                price = Math.Round(value,2);
            }
        }

        private bool isAvailable = true;
        public bool IsAvailable
        {
            get { return isAvailable; }
            set { isAvailable = value; }
        }

        private DateTime borrowingDate;
        public DateTime BorrowingDate
        {
            get { return borrowingDate; }
            set { borrowingDate = value; }
        }

        private int borrowDays;
        public int BorrowDays
        {
            get { return borrowDays; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Aantal uitleendagen moet groter zijn dan 0.");
                }
                borrowDays = value;
            }
        }

        private bool isBorrowed = false;
        public bool IsBorrowed
        {
            get { return isBorrowed; }
            set { isBorrowed = value; }
        }



        public Book(string title, string author, Library library)
        {
            Title = title;
            Author = author;
            IsAvailable = true;
            library.AddBook(this); 
        }

        public Book(string title, string author, string isbn, string publisher, int publicationYear, int page, double price, Genre genre, Library library)
    : this(title, author, library)
        {
            Isbn = isbn;
            Publisher = publisher;
            PublicationYear = publicationYear;
            Page = page;
            Price = price;
            BookGenre = genre;
        }



        public void BookInfo()
        {
            Console.WriteLine($"Titel: {Title}");
            Console.WriteLine($"Ateur: {Author}");
            Console.WriteLine($"Uitgeverij: {Publisher}");
            Console.WriteLine($"Genre: {bookGenre}");
            Console.WriteLine($"Publicatie jaar: {publicationYear}");
            Console.WriteLine($"Aantal paginas: {Page}");
            Console.WriteLine($"Prijs: {Price:F2}");
            Console.WriteLine($"ISBN nummer: {Isbn}");
            Console.WriteLine($"Uitgeleend: {(IsBorrowed ? "Ja" : "Nee")}");
            Console.WriteLine();

        }

        public void Borrow()
        {
            if (!IsAvailable)
            {
                Console.WriteLine("Dit boek is momenteel niet beschikbaar.");
                return;
            }

            BorrowingDate = DateTime.Now;


            if (BookGenre == Genre.Schoolbook)
            {
                BorrowDays = 10;
            }
            else
            {
                BorrowDays = 20;
            }

            IsAvailable = false;
            IsBorrowed = true;

            DateTime endDate = BorrowingDate.AddDays(BorrowDays);
            Console.WriteLine($"Je hebt het boek '{Title}' uitgeleend tot {endDate:dd-MM-yyyy}.");
        }

        public void Return()
        {
            if (IsAvailable)
            {
                Console.WriteLine("Deze boek is beschikbaar.");
               
            }

            DateTime returnDate = DateTime.Now;
            DateTime endDate = BorrowingDate.AddDays(BorrowDays);
            IsAvailable = true;
            IsBorrowed = false;

            if (returnDate > endDate)
            {
                Console.WriteLine($"Boek '{Title}' werd te laat teruggebracht op {returnDate:dd-MM-yyyy}. Deadline was {endDate:dd-MM-yyyy}.");
            }
            else
            {
                Console.WriteLine($"Je hebt het boek '{Title}' op tijd teruggebracht.");
            }
        }
    }
}
