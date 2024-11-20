using APIs.Model;

namespace APIs.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Categories>> GetAllCategoriesAsync();
        Task<Categories> GetCategoryByIdAsync(int categoryId);
        Task<int> AddCategoryAsync(Categories category);
        Task<int> UpdateCategoryAsync(Categories category);
        Task DeleteCategoryAsync(int id);
        //Task<int> DeleteCategoryAsync(Categories category);

    }
}
