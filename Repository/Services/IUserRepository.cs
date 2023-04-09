

namespace tfg.Repository.Services.IUserRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers1();
        IEnumerable<User> GetAllUsers();
        Task<IEnumerable<User>> GetAllUsersWithDetails();
        User GetUserById(int userId);
        User GetUserWithDetails(int userId);
        int Login(User user);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
        IEnumerable<User> usersByBooks(int bookId);
    }
}