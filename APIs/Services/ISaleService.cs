using APIs.Model;

namespace APIs.Services
{
    public interface ISaleService
    {
        Task<IEnumerable<Sale>> GetAllSalesAsync();
        Task<IEnumerable<SaleDetailsDto>> GetSaleByKeywordAsync(string keyword);
        Task<int> AddSaleAsync(Sale sale);
        Task<int> UpdateSaleAsync(Sale sale);
        Task<int> DeleteSaleAsync(int saleId);

    }
}
