using APIs.Model;
using APIs.Repositories;

namespace APIs.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _repository;

        public PurchaseService(IPurchaseRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Purchases>> GetAllPurchasesAsync()
        {
            return await _repository.GetAllPurchasesAsync();
        }

        public async Task<Purchases> GetPurchaseByIdAsync(int id)
        {
            return await _repository.GetPurchaseByIdAsync(id);
        }

        public async Task<bool> AddPurchaseAsync(Purchases purchase)
        {
            if (purchase.Quantity <= 0 || purchase.PurchasePrice <= 0)
            {
                throw new ArgumentException("Invalid purchase details.");
            }

            purchase.TotalCost = purchase.Quantity * purchase.PurchasePrice;

            var result = await _repository.AddPurchaseAsync(purchase);
            return result > 0;
        }

        public async Task<bool> UpdatePurchaseAsync(Purchases purchase)
        {
            if (purchase.ID <= 0 || purchase.Quantity <= 0 || purchase.PurchasePrice <= 0)
            {
                throw new ArgumentException("Invalid purchase details.");
            }

            purchase.TotalCost = purchase.Quantity * purchase.PurchasePrice;

            var result = await _repository.UpdatePurchaseAsync(purchase);
            return result > 0;
        }

        public async Task<bool> DeletePurchaseAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID.");
            }

            var result = await _repository.DeletePurchaseAsync(id);
            return result > 0;
        }
    }
}
