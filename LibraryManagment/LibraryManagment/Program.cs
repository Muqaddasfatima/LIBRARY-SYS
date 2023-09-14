using System;
using System.Collections.Generic;


class LibraryItem      // Base class for Books
{
    public string Title { get; private set; }  //ENCAPSULATION 
    public bool IsAvailable { get; private set; }

    public LibraryItem(string title ) // CONSTRUCTOR of LibraryItem class 
    {
        this.Title = title;
        this.IsAvailable = true;
    }

    public void Borrow()
    {
        if (IsAvailable)
        {
            IsAvailable = false;
            Console.WriteLine($"{Title} has been borrowed.");
        }
        else
        {
            Console.WriteLine($"{Title} is already borrowed.");
        }
    }

    public void Return()
    {
        if (!IsAvailable)
        {
            IsAvailable = true;
            Console.WriteLine($"{Title} has been returned.");
        }
        else
        {
            Console.WriteLine($"{Title} is already available.");
        }
    }
}


class Book : LibraryItem  // Book class inherits from LibraryItem
{
    public string Author { get; private set; }            //ENCAPSULATED 
    public string ISBN { get; private set; }

    public Book(string title, string author, string isbn) : base(title)
    {
        this.Author = author;
        this.ISBN = isbn;
    }
}

// User class
class User 
{
    public string LibraryCardID { get; private set; }  //ENCAPSULATION 
    public string Name { get; private set; }

    

    public User(string libraryCardID, string name)
    {
        this.LibraryCardID = libraryCardID;
        this.Name = name;
       
    }
}

class LibrarySystem
{
    private Dictionary<string, Book> BooksDictionary = new Dictionary<string, Book>(); // DICTIONARY GENERIC COLLECTION  
    private Dictionary<string, User> UsersDictionary = new Dictionary<string, User>(); //DICTIONARY GENERIC COLLECTION 

    public void AddBook(Book book)
    {
        BooksDictionary[book.ISBN] = book;

    }

    public void AddUser(User user)
    {
        UsersDictionary[user.LibraryCardID] = user;
    }

    public void ViewAvailableBooks()
    {
        Console.WriteLine("Available Books:");
        foreach (var book in BooksDictionary.Values)
        {
            if (book.IsAvailable)
            {
                Console.WriteLine($"{book.Title} by {book.Author} (ISBN: {book.ISBN})");
            }
        }
    }



    public void BorrowBook(string libraryCardID, string isbn)
    {
        if (UsersDictionary.ContainsKey(libraryCardID) && BooksDictionary.ContainsKey(isbn))
        {
            Book book = BooksDictionary[isbn];
            if (book.IsAvailable)
            {
                book.Borrow();
                Console.WriteLine($"{UsersDictionary[libraryCardID].Name} has borrowed {book.Title}.");
            }
            else
            {
                Console.WriteLine($"{book.Title} is already borrowed.");
            }
        }
        else
        {
            Console.WriteLine("Invalid user or book.");
        }
    }

    public void ReturnBook(string libraryCardID, string isbn)
    {
        if (UsersDictionary.ContainsKey(libraryCardID) && BooksDictionary.ContainsKey(isbn))
        {
            Book book = BooksDictionary[isbn];
            if (!book.IsAvailable)
            {
                book.Return();
                Console.WriteLine($"{UsersDictionary[libraryCardID].Name} has returned {book.Title}.");
            }
            else
            {
                Console.WriteLine($"{book.Title} is already available.");
            }
        }
        else
        {
            Console.WriteLine("Invalid user or book.");
        }
    }
}

class Program
{
    static void Main(string[]  Args)
    {
        LibrarySystem librarySystem = new LibrarySystem();

        // Adding books and users ----------------------------
        librarySystem.AddBook(new Book("MATH", "MUQADDAS", "1234"));
        librarySystem.AddBook(new Book("COMPUTER", "FATIMA", "5678"));
        librarySystem.AddUser(new User("ID6836", "ALI"));
        librarySystem.AddUser(new User("ID0076", "TURAB"));

        while (true)
        {
            Console.WriteLine("Library System Menu:");
            Console.WriteLine("1. View available books");
            Console.WriteLine("2. Borrow a book");
            Console.WriteLine("3. Return a book");
            Console.WriteLine("4. Add new book (admin)");

            Console.WriteLine("5. Exit");

            string choice = Console.ReadLine();

            switch (choice)   // CHECKS FOR VALID OR INVALID INPUTS 
            {  
                case "1":
                    librarySystem.ViewAvailableBooks();
                    break;

                case "2":
                    Console.WriteLine("Enter your Library Card ID:");
                    string borrowCardID = Console.ReadLine();
                    Console.WriteLine("Enter the book's ISBN:");
                    string borrowISBN = Console.ReadLine();
                    librarySystem.BorrowBook(borrowCardID, borrowISBN);
                    break;

                case "3":
                    Console.WriteLine("Enter your Library Card ID:");
                    string returnCardID = Console.ReadLine();
                    Console.WriteLine("Enter the book's ISBN:");
                    string returnISBN = Console.ReadLine();
                    librarySystem.ReturnBook(returnCardID, returnISBN);
                    break;

                case "4":
                    // Add new book (admin)
                    Console.WriteLine("Enter the book's title:");
                    string newBookTitle = Console.ReadLine();
                    Console.WriteLine("Enter the author's name:");
                    string newBookAuthor = Console.ReadLine();
                    Console.WriteLine("Enter the book's ISBN:");
                    string newBookISBN = Console.ReadLine();
                    librarySystem.AddBook(new Book(newBookTitle, newBookAuthor, newBookISBN));
                    Console.WriteLine("New book added.");
                    break;


                case "5":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
