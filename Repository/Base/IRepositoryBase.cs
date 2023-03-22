using System.Linq.Expressions;

namespace tfg.Repository.Base.IRepositoryBase
{
    public interface IRepositoryBase<T>
    {
        Task<IEnumerable<T>> GetAll();
        IQueryable<T> FindAll(); 
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression); 
        void Create(T entity); 
        void Update(T entity); 
        void Delete(T entity);
    }
}