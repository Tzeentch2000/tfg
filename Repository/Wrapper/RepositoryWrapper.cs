using ContextDB;
using tfg.Repository.UserRepository;
using tfg.Repository.Services.IUserRepository;
using Wrapper;
using tfg.Repository.Services.ICategoryRepository;

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
