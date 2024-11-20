using APIs.Model;
using APIs.Repositories;

namespace APIs.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;

        public SaleService(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<IEnumerable<Sale>> GetAllSalesAsync()
        {
            return await _saleRepository.GetAllSalesAsync();
        }

        public async Task<IEnumerable<SaleDetailsDto>> GetSaleByKeywordAsync(string keyword)
        {
            //if (string.IsNullOrEmpty(keyword))
            //{
            //    return await _saleRepository.GetAllSalesAsync();
            //}
            return await _saleRepository.GetSaleByKeywordAsync(keyword);
        }

        public async Task<int> AddSaleAsync(Sale sale)
        {
            if (sale == null)
            {
                throw new ArgumentNullException(nameof(sale));
            }

            if (sale.TotalAmount <= 0)
            {
                throw new ArgumentException("TotalAmount must be greater than 0");
            }

            return await _saleRepository.AddSaleAsync(sale);
        }

        public async Task<int> UpdateSaleAsync(Sale sale)
        {
            if (sale == null || sale.ID <= 0)
            {
                throw new ArgumentException("Invalid Sale object or ID");
            }

            return await _saleRepository.UpdateSaleAsync(sale);
        }

        public async Task<int> DeleteSaleAsync(int saleId)
        {
            if (saleId <= 0)
            {
                throw new ArgumentException("Invalid Sale ID");
            }

            return await _saleRepository.DeleteSaleAsync(saleId);
        }

    }
}
