namespace APIs.Services
{
    public interface IRevenueService
    {
        Task<decimal> CalculateTotalRevenueAsync(DateTime startDate, DateTime endDate);

    }
}
