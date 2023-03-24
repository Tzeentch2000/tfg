namespace tfg.Repository.Services.ICategoryRepository
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAllCategories();
        Category GetCategoryById(int userId);
        Category GetCategoryWithDetails(int userId);
        void CreateCategory(Category user);
        void UpdateCategory(Category user);
        void DeleteCategory(Category user);
    }
}