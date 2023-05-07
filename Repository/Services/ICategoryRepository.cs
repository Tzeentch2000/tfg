namespace tfg.Repository.Services.ICategoryRepository
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAllCategories();
        IEnumerable<Category> GetActiveCategories();
        Category GetCategoryById(int id);
        Category GetCategoryWithDetails(int id);
        void CreateCategory(Category model);
        void UpdateCategory(Category model);
        void DeleteCategory(Category model);
    }
}