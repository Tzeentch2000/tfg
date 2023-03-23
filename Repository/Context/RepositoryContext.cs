using Microsoft.EntityFrameworkCore;

namespace ContextDB
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) 
            : base(options) 
        { 
        }

        public DbSet<Category> category { get; set; }
        public DbSet<Book> book { get; set; }
        public DbSet<Order> order { get; set; }
        public DbSet<User> user { get; set; }

        internal Task<object> FindAsync(int v)
        {
            throw new NotImplementedException();
        }
    }
}
