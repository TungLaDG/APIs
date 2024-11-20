namespace APIs.Repositories
{
    public interface IRevenueRepository
    {
        Task<decimal> GetTotalRevenueAsync(DateTime startDate, DateTime endDate);

    }
}
