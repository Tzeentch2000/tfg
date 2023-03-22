using tfg.Repository.Services.IUserRepository;

namespace Wrapper
{
    public interface IRepositoryWrapper 
    { 
        IUserRepository User { get; } 
        void Save(); 
    }
}
