using APIs.Model;

namespace APIs.Services
{
    public interface IPurchaseService
    {
        Task<IEnumerable<Purchases>> GetAllPurchasesAsync();
        Task<Purchases> GetPurchaseByIdAsync(int id);
        Task<bool> AddPurchaseAsync(Purchases purchase);
        Task<bool> UpdatePurchaseAsync(Purchases purchase);
        Task<bool> DeletePurchaseAsync(int id);
    }
}
