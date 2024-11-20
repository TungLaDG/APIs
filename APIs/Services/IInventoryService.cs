using APIs.Model;

namespace APIs.Services
{
    public interface IInventoryService
    {
        Task<IEnumerable<Product>> GetInventoryAsync();
        Task<Product> GetProductInventoryAsync(int productId);
        Task UpdateQuantityAsync(int productId, int quantityChange);
    }

}
