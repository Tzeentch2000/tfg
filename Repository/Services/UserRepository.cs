using ContextDB;
using Encrypt;
using Microsoft.EntityFrameworkCore;
using tfg.Repository.Base.RepositoryBase;
using tfg.Repository.Services.IUserRepository;

namespace tfg.Repository.UserRepository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository 
    { 
        public UserRepository(RepositoryContext repositoryContext) 
            : base(repositoryContext) 
        { 
        }

        public async Task<IEnumerable<User>> GetAllUsers1(){
            return await RepositoryContext.Set<User>().ToListAsync();
        }

        public IEnumerable<User> GetAllUsers() 
        { 
            return FindAll()
                .OrderBy(u => u.Surname)
                .ToList(); 
        }

        public async Task<IEnumerable<User>> GetAllUsersWithDetails()
        {
             return await RepositoryContext.Set<User>()
             .Include(u => u.Orders).ThenInclude(o => o.Book).ThenInclude(b => b.Categories)
             .Include(u => u.Orders).ThenInclude(o => o.Book).ThenInclude(b => b.State)
             .ToListAsync();
        }

        public User GetUserById(int userId)
        {
            return FindByCondition(u => u.Id.Equals(userId))
                .FirstOrDefault();
        }

        public int Login(User user){
            var encryptPassword = Hash.EncryptPassword(user.Password);
            var lUser = FindByCondition(u => u.Email == user.Email && u.Password == encryptPassword)
            .FirstOrDefault();
            if(lUser == null){
                return -1;
            }
            return lUser.Id;
        }

        public User GetUserWithDetails(int userId)
        {
            return FindByCondition(user => user.Id.Equals(userId))
                  .Include(u => u.Orders).ThenInclude(o => o.Book).ThenInclude(b => b.Categories)
                  .Include(u => u.Orders).ThenInclude(o => o.Book).ThenInclude(b => b.State)
                  .FirstOrDefault();
        }

        public void CreateUser(User user)
        {
            Create(user);
        }

        public void UpdateUser(User user)
        {
            Update(user);
        }

        public void DeleteUser(User user)
        {
            Delete(user);
        }

        public IEnumerable<User> usersByBooks(int bookId)
        {
            return RepositoryContext.Set<User>().Where(u => u.Orders.Any(u => u.Book.Id == bookId));
        }

        public bool isUserAdmin(int id)
        {
            return FindByCondition(u => u.Id.Equals(id))
                .FirstOrDefault().IsAdmin;
        }

        public int CheckUsername(string username)
        {
            var lUser = FindByCondition(u => u.Email == username)
            .FirstOrDefault();
            if(lUser == null){
                return -1;
            }
            return lUser.Id;
        }
    }
}
