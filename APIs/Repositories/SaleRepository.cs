using APIs.Model;
using Dapper;
using System.Data;
using System.Text;

namespace APIs.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly IDbConnection _connection;

        public SaleRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Sale>> GetAllSalesAsync()
        {
            var sql = "SELECT * FROM Sales";
            return await _connection.QueryAsync<Sale>(sql);
        }

        public async Task<IEnumerable<SaleDetailsDto>> GetSaleByKeywordAsync(string keyword)
        {
            var query = @"
        SELECT 
            sd.ID, sd.Quantity, sd.SalePrice, sd.SubTotal,
            s.CustomerName, s.PhoneNumber, s.SaleDate,
            p.ProductName 
        FROM SaleDetails sd
        JOIN Sales s ON sd.SaleID = s.ID
        JOIN Products p ON sd.ProductID = p.ID
        WHERE s.CustomerName LIKE @Keyword OR p.ProductName LIKE @Keyword OR s.PhoneNumber LIKE @Keyword";

            var param = new { Keyword = $"%{keyword}%" };

            return await _connection.QueryAsync<SaleDetailsDto>(query, param);
        }

        public async Task<int> AddSaleAsync(Sale sale)
        {
            var sql = @"INSERT INTO Sales (SaleDate, TotalAmount, PhoneNumber, CustomerName)
                    VALUES (@SaleDate, @TotalAmount, @PhoneNumber, @CustomerName)";
            return await _connection.ExecuteAsync(sql, sale);
        }

        public async Task<int> UpdateSaleAsync(Sale sale)
        {
            var sql = @"UPDATE Sales 
                    SET SaleDate = @SaleDate, 
                        TotalAmount = @TotalAmount, 
                        PhoneNumber = @PhoneNumber, 
                        CustomerName = @CustomerName 
                    WHERE ID = @ID";
            return await _connection.ExecuteAsync(sql, sale);
        }

        public async Task<int> DeleteSaleAsync(int saleId)
        {
            var sql = "DELETE FROM Sales WHERE ID = @ID";
            return await _connection.ExecuteAsync(sql, new { ID = saleId });
        }

    }
}
