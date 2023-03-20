using Microsoft.EntityFrameworkCore;

namespace ContextDB
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) 
            : base(options) 
        { 
        }

        public DbSet<User> user { get; set; }

        internal Task<object> FindAsync(int v)
        {
            throw new NotImplementedException();
        }
    }
}
