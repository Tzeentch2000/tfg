using ContextDB;
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

        public User GetUserById(Guid userId)
        {
            return FindByCondition(u => u.Id.Equals(userId))
                .FirstOrDefault();
        }

        public User GetUserWithDetails(Guid userId)
        {
            return FindByCondition(user => user.Id.Equals(userId))
                //.Include(ac => ac.Accounts)
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
    }
}
