using ContextDB;
using Microsoft.EntityFrameworkCore;
using tfg.Repository.Base.RepositoryBase;
using tfg.Repository.Services.ICategoryRepository;

namespace tfg.Repository.UserRepository
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository 
    { 
        public CategoryRepository(RepositoryContext repositoryContext) 
            : base(repositoryContext) 
        { 
        }

        public IEnumerable<Category> GetAllCategories() 
        { 
            return FindAll()
                .OrderBy(c => c.Name)
                .ToList(); 
        }

        public Category GetCategoryById(int categoryId)
        {
            return FindByCondition(u => u.Id.Equals(categoryId))
                .FirstOrDefault();
        }

        public Category GetCategoryWithDetails(int categoryId)
        {
            return FindByCondition(category => category.Id.Equals(categoryId))
                .Include(c => c.Books)
                .FirstOrDefault();
        }

        public void CreateCategory(Category category)
        {
            Create(category);
        }

        public void UpdateCategory(Category category)
        {
            Update(category);
        }

        public void DeleteCategory(Category category)
        {
            Delete(category);
        }
    }
}
