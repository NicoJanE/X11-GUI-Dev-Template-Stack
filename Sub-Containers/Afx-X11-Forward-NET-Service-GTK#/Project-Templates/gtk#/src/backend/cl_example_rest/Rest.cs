
// This sample class library contains a simple Rest business logic method. 
// When a library name starts with 'cl_' (e.g., cl_[name]), it becomes compatible 
// with tasks in Visual Studio Code (e.g., Clean tasks). During the build process, 
// the library name (based on the directory name) is transformed by replacing 'cl_' with 'lib_' 
// to generate the target output name (e.g., lib_[name]).
//
// To create a similar library, use the VSC task: '2.1 AFX CREATE: Class Source Library'.



namespace app.src.backend.cl_example_rest           // Our namespace
{

    using System.Collections.ObjectModel;
    using System.Linq;


    public class Book
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
    }



    public class RestViewModel
    {
        // ObservableCollection automatically updates the UI when data changes
        public ObservableCollection<Book> Books { get; set; }

        private int _nextId = 1;
        

        public RestViewModel()
        {
            Books = new ObservableCollection<Book>
            {
                new Book { Id = _nextId++, Title = "1984", Author = "George Orwell" },
                new Book { Id = _nextId++, Title = "To Kill a Mockingbird", Author = "Harper Lee" },
                new Book { Id = _nextId++, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald" },
                new Book { Id = _nextId++, Title = "Pride and Prejudice", Author = "Jane Austen" },
                new Book { Id = _nextId++, Title = "The Catcher in the Rye", Author = "J.D. Salinger" }
            };
        }

        // Create (Add a new book)
        public void AddBook(string title, string author)
        {
            Books.Add(new Book { Id = _nextId++, Title = title, Author = author });
        }

        // Read (Get a book by ID)
        public Book GetBookById(int id)
        {
            return Books.FirstOrDefault(b => b.Id == id)!;
        }

        // Update (Edit a book by ID)
        public bool UpdateBook(int id, string title, string author)
        {
            var book = GetBookById(id);
            if (book == null) return false;

            book.Title = title;
            book.Author = author;
            return true;
        }

        // Delete (Remove a book by ID)
        public bool DeleteBook(int id)
        {
            var book = GetBookById(id);
            if (book == null) return false;

            Books.Remove(book);
            return true;
        }
    }

}