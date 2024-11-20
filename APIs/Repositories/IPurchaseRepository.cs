using APIs.Model;

namespace APIs.Repositories
{
    public interface IPurchaseRepository
    {
        Task<IEnumerable<Purchases>> GetAllPurchasesAsync();
        Task<Purchases> GetPurchaseByIdAsync(int id);
        Task<int> AddPurchaseAsync(Purchases purchase);
        Task<int> UpdatePurchaseAsync(Purchases purchase);
        Task<int> DeletePurchaseAsync(int id);
    }
}
