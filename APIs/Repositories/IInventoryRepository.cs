using APIs.Model;

namespace APIs.Repositories
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<Product>> GetAllInventoryAsync();
        Task<Product> GetProductInventoryAsync(int productID);
        Task UpdateQuantityAsync(int productId, int quantityChange);

    }
}
