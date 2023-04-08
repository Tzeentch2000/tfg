using ContextDB;
using tfg.Repository.UserRepository;
using tfg.Repository.Services.IUserRepository;
using Wrapper;
using tfg.Repository.Services.ICategoryRepository;
using tfg.Repository.Services.IStateRepository;
using tfg.Repository.StateRepository;
using tfg.Repository.Services.IBookRepository;
using tfg.Repository.BookRepository;

namespace RepositoryWrapperContext
{
    public class RepositoryWrapper : IRepositoryWrapper 
    { 
        private RepositoryContext _repoContext; 
        private IUserRepository _user; 
        public IUserRepository User 
        { 
            get 
            { 
                if (_user == null) 
                { 
                    _user = new UserRepository(_repoContext); 
                } 
                return _user; 
            } 
        } 

        private ICategoryRepository _category; 
        public ICategoryRepository Category 
        { 
            get 
            { 
                if (_category == null) 
                { 
                    _category = new CategoryRepository(_repoContext); 
                } 
                return _category; 
            } 
        }

        private IStateRepository _state;
        public IStateRepository State{
            get
            {
                if(_state == null){
                    _state = new StateRepository(_repoContext);
                }
                return _state;
            }
        }

        private IBookRepository _book;
        public IBookRepository Book{
            get
            {
                if(_book == null){
                    _book = new BookRepository(_repoContext);
                }
                return _book;
            }
        }

        public RepositoryWrapper(RepositoryContext repositoryContext) 
        { 
            _repoContext = repositoryContext; 
        } 
        
        public void Save() 
        {
            _repoContext.SaveChanges();
        } 
    }
}
