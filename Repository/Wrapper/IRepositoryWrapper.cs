using tfg.Repository.Services.ICategoryRepository;
using tfg.Repository.Services.IUserRepository;

namespace Wrapper
{
    public interface IRepositoryWrapper 
    { 
        IUserRepository User { get; } 
        ICategoryRepository Category { get; }
        void Save(); 
    }
}
