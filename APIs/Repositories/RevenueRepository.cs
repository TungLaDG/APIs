using Dapper;
using System.Data;

namespace APIs.Repositories
{
    public class RevenueRepository : IRevenueRepository
    {
        private readonly IDbConnection _dbConnection;

        public RevenueRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<decimal> GetTotalRevenueAsync(DateTime startDate, DateTime endDate)
        {
            string query = @"
                SELECT SUM(sd.SubTotal) AS TotalRevenue
                FROM Sales s
                JOIN SaleDetails sd ON s.Id = sd.SaleID
                WHERE s.SaleDate BETWEEN @StartDate AND @EndDate;
            ";

            var parameters = new { StartDate = startDate, EndDate = endDate };

            return await _dbConnection.ExecuteScalarAsync<decimal>(query, parameters);
        }
    }

}
