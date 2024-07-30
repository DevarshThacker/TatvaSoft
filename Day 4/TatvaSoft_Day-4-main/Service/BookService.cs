using BookProject.Models;

namespace BookProject.Service
{
    public class BookService
    {
        private readonly List<Book> _books;
        public BookService()
        {
            _books = new List<Book>{
              new Book { Id = 1, Title = "1984", Author = "GeorgeOrwell", Genre = "Dystopian" },
              new Book { Id = 2, Title = "To Kill a Mockingbird",
                Author = "Harper Lee", Genre = "Fiction" }
          };

        }

        public List<Book> GetAll() => _books;
        public Book GetById(int id) => _books.FirstOrDefault(b => b.Id == id);
        public void Add(Book book) => _books.Add(book);
        public void Update(Book book)
        {
            var index = _books.FindIndex(b => b.Id == book.Id);
            if (index != -1)
            {
                _books[index] = book;
            }
        }
        public void Delete(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                _books.Remove(book);
            }
        }

    }
}
