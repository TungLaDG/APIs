using APIs.Model;

namespace APIs.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<Product>> GetAllProductsByCategoryIdAsync(int categoryId);
        Task<Product> GetProductByIdAsync(int productId);
        Task<int> AddProductAsync(Product product);
        Task<int> UpdateProductAsync(Product product);
        Task<int> DeleteProductAsync(int productId);
    }
}
