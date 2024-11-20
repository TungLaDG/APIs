using APIs.Model;
using APIs.Repositories;

namespace APIs.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<IEnumerable<Product>> GetInventoryAsync()
        {
            return await _inventoryRepository.GetAllInventoryAsync();
        }

        public async Task<Product> GetProductInventoryAsync(int productId)
        {
            return await _inventoryRepository.GetProductInventoryAsync(productId);
        }

        public async Task UpdateQuantityAsync(int productId, int quantityChange)
        {
            await _inventoryRepository.UpdateQuantityAsync(productId, quantityChange);
        }
    }
}
