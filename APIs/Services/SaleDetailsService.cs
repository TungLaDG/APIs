using APIs.Model;
using APIs.Repositories;

namespace APIs.Services
{
    public class SaleDetailsService : ISaleDetailsService
    {
        private readonly ISaleDetailsRepository _saleDetailsRepository;
        public SaleDetailsService (ISaleDetailsRepository saleDetailsRepository)
        {
            _saleDetailsRepository = saleDetailsRepository;
        }

        public async Task<int> AddSaleDetailsAsync(SaleDetails salesDetails)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteSaleDetailsAsync(int salesDetailsId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SaleDetails>> GetAllSaleDetailsAsync()
        {
            return await _saleDetailsRepository.GetAllSaleDetailsAsync();
        }

        //public async Task<IEnumerable<SaleDetailsDto>> GetSaleDetailsByKeywordAsync(string keyword)
        //{
        //    return await _saleDetailsRepository.GetAllSaleDetailsAsync(keyword);
        //}

        public async Task<int> UpdateSaleDetailsAsync(SaleDetails saleDetails)
        {
            throw new NotImplementedException();
        }
    }
}
