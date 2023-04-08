using tfg.Repository.Services.IBookRepository;
using tfg.Repository.Services.ICategoryRepository;
using tfg.Repository.Services.IStateRepository;
using tfg.Repository.Services.IUserRepository;

namespace Wrapper
{
    public interface IRepositoryWrapper 
    { 
        IUserRepository User { get; } 
        ICategoryRepository Category { get; }
        IStateRepository State { get; }
        IBookRepository Book { get; }
        void Save(); 
    }
}
