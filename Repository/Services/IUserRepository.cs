

namespace tfg.Repository.Services.IUserRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers1();
        IEnumerable<User> GetAllUsers();
        User GetUserById(Guid userId);
        User GetUserWithDetails(Guid userId);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}