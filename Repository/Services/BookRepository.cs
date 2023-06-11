using ContextDB;
using Microsoft.EntityFrameworkCore;
using tfg.Repository.Base.RepositoryBase;
using tfg.Repository.Services.IBookRepository;


namespace tfg.Repository.BookRepository
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository 
    { 
        public BookRepository(RepositoryContext repositoryContext) 
            : base(repositoryContext) 
        { 
        }

        public IEnumerable<Book> GetAllBooks()
        { 
            return FindAll()
                .OrderBy(s => s.Name)
                .ToList(); 
        }

        public IEnumerable<Book> GetAllBooksWithDetails()
        { 
            return FindAll()
                .OrderBy(s => s.Name)
                .Include(state => state.State)
                .Include(category => category.Categories)
                .ToList(); 
        }

        public IEnumerable<Book> GetActiveBooksWithDetails()
        {
              return FindAll()
                .OrderBy(s => s.Name)
                .Include(state => state.State)
                .Include(category => category.Categories)
                .Where(book => book.IsActive).ToList();
        }

        public Book GetBookById(int bookId)
        {
            return FindByCondition(u => u.Id.Equals(bookId))
                .FirstOrDefault();
        }

        public Book GetBookWithDetails(int bookId)
        {
            return FindByCondition(book => book.Id.Equals(bookId))
                .Include(state => state.State)
                .Include(category => category.Categories)
                .FirstOrDefault();
        }

        public void CreateBook(Book book)
        {
            Create(book);
        }

        public Book CreateBookWithDetails(Book model)
        {
             /*RepositoryContext.Set<Book>();
             RepositoryContext.Entry(model).State = EntityState.Added;
             return model;*/
             RepositoryContext.Set<Book>().Attach(model);
             return model;
        }

        public void UpdateAllBookAtributes(Book book){
        
            Book eyy = RepositoryContext.book.Include(x => x.Categories).Include(x => x.State).Single(x => x.Id == book.Id);
            eyy.Categories.Clear();
            RepositoryContext.SaveChanges();
            RepositoryContext.ChangeTracker.Clear();
            Update(book);
        }

        public void UpdateBook(Book book)
        {

            Update(book);
        }

        public void DeleteBook(Book book)
        {
            Delete(book);
        }

        public IEnumerable<Book> booksByState(int stateId)
        {
            return FindByCondition(b => b.State.Id.Equals(stateId)).ToList(); 
        }

        public IEnumerable<Book> booksByCategories(int categoryId)
        {
            return RepositoryContext.Set<Book>().Where(x => x.Categories.Any(c => c.Id == categoryId));
        }
    }
}
