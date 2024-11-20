using APIs.Repositories;

namespace APIs.Services
{
    
        public class RevenueService : IRevenueService
        {
            private readonly IRevenueRepository _revenueRepository;

            public RevenueService(IRevenueRepository revenueRepository)
            {
                _revenueRepository = revenueRepository;
            }

            public async Task<decimal> CalculateTotalRevenueAsync(DateTime startDate, DateTime endDate)
            {
                return await _revenueRepository.GetTotalRevenueAsync(startDate, endDate);
            }
        }
    
}
