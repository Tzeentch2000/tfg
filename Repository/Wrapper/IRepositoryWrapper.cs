using UserServiceInterface;

namespace Wrapper
{
    public interface IRepositoryWrapper 
    { 
        IUserRepository User { get; } 
        void Save(); 
    }
}
