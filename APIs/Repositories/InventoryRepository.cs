using APIs.Model;
using Dapper;
using System.Data;

namespace APIs.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly IDbConnection _dbConnection;
        public InventoryRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<IEnumerable<Product>> GetAllInventoryAsync()
        {
            string query = @"select p.Id , p.ProductName, p.Price, p.CategoryId, p.imageURL, p.description, i.LastUpdated, i.Quantity from Products p join Inventory i on p.Id = i.ProductID";
            return await _dbConnection.QueryAsync<Product>(query);
        }

        public async Task<Product> GetProductInventoryAsync(int productID)
        {
            string query = @"
                SELECT p.Id, p.ProductName, p.Price, p.imageURL, p.description ,i.Quantity, i.LastUpdated
                FROM Products p
                JOIN Inventory i ON p.Id = i.ProductID
                WHERE p.Id = @Id;
            ";
            var parameters = new {Id  = productID};
            return await _dbConnection.QueryFirstOrDefaultAsync<Product>(query, parameters);
        }

        public async Task UpdateQuantityAsync(int productId, int quantityChange)
        {
            string query = @"
                UPDATE Inventory
                SET Quantity = Quantity + @QuantityChange
                WHERE ProductID = @ProductID;
            ";

            var parameters = new { ProductID = productId, QuantityChange = quantityChange };

            await _dbConnection.ExecuteAsync(query, parameters);
        }
    }
}
