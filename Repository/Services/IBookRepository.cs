namespace tfg.Repository.Services.IBookRepository
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAllBooks();
        IEnumerable<Book> GetAllBooksWithDetails();
        Book GetBookById(int id);
        Book GetBookWithDetails(int id);
        void CreateBook(Book model);
        Book CreateBookWithDetails(Book model);
        void tuputamadre(Book book);
        void UpdateBook(Book model);
        void DeleteBook(Book model);
    }
}