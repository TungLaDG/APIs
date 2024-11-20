using APIs.Model;

namespace APIs.Repositories
{
    public interface ISaleDetailsRepository
    {
        Task<IEnumerable<SaleDetails>> GetAllSaleDetailsAsync();
        //Task<IEnumerable<SaleDetailsDto>> GetSaleDetailsByKeywordAsync(string keyword);

        Task<int> AddSaleDetailsAsync(SaleDetails saleDetails);
        Task<int> UpdateSaleDetailsAsync(SaleDetails saleDetails);
        Task<int> DeleteSaleDetailsAsync(int saleDetailsId);
    }
}
