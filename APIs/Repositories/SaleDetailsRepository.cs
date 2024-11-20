using APIs.Model;
using Dapper;
using System.Data;

namespace APIs.Repositories
{
    public class SaleDetailsRepository : ISaleDetailsRepository
    {
        private readonly IDbConnection _connection;
        public SaleDetailsRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<int> AddSaleDetailsAsync(SaleDetails salesDetails)
        {
            string query = @"
            INSERT INTO SaleDetails (SaleID, ProductID, Quantity, SalePrice, SubTotal)
            VALUES (@SaleID, @ProductID, @Quantity, @SalePrice, @SubTotal)";

            return await _connection.ExecuteAsync(query, salesDetails);
        }

        public async Task<int> DeleteSaleDetailsAsync(int SaleDetailsId)
        {
            string query = "DELETE FROM SaleDetails WHERE ID = @ID";
            return await _connection.ExecuteAsync(query, new { ID = SaleDetailsId });
        }

        public async Task<IEnumerable<SaleDetails>> GetAllSaleDetailsAsync()
        {
            string query = "SELECT * FROM SaleDetails";
            return await _connection.QueryAsync<SaleDetails>(query);
        }


        //public async Task<IEnumerable<SaleDetailsDto>> GetSaleDetailsByKeywordAsync(string keyword)
        //{
        //    var query = @"
        //SELECT 
        //    sd.ID, sd.Quantity, sd.SalePrice, sd.SubTotal,
        //    s.CustomerName, s.PhoneNumber, s.SaleDate,
        //    p.ProductName 
        //FROM SaleDetails sd
        //JOIN Sales s ON sd.SaleID = s.ID
        //JOIN Products p ON sd.ProductID = p.ID
        //WHERE s.CustomerName LIKE @Keyword OR p.ProductName LIKE @Keyword OR s.PhoneNumber LIKE @Keyword";

        //    var param = new { Keyword = $"%{keyword}%" };

        //    return await _connection.QueryAsync<SaleDetailsDto>(query, param);
        //}
        public async Task<int> UpdateSaleDetailsAsync(SaleDetails SaleDetails)
        {
            string query = @"
            UPDATE SaleDetails 
            SET SaleID = @SaleID, 
                ProductID = @ProductID, 
                Quantity = @Quantity, 
                SalePrice = @SalePrice, 
                SubTotal = @SubTotal
            WHERE ID = @ID";

            return await _connection.ExecuteAsync(query, SaleDetails);
        }
    }
}
