using APIs.Model;
using System.Data;
using Dapper;

namespace APIs.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly IDbConnection _dbConnection;

        public PurchaseRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Purchases>> GetAllPurchasesAsync()
        {
            var query = "SELECT * FROM Purchases";
            return await _dbConnection.QueryAsync<Purchases>(query);
        }

        public async Task<Purchases> GetPurchaseByIdAsync(int id)
        {
            var query = "SELECT * FROM Purchases WHERE ID = @ID";
            var param = new { ID = id };
            return await _dbConnection.QuerySingleOrDefaultAsync<Purchases>(query, param);
        }

        public async Task<int> AddPurchaseAsync(Purchases purchase)
        {
            using var transaction = _dbConnection.BeginTransaction();

            try
            {
                var query = @"
                INSERT INTO Purchases (ProductID, PurchaseDate, PurchasePrice, Quantity, TotalCost) 
                VALUES (@ProductID, @PurchaseDate, @PurchasePrice, @Quantity, @TotalCost)";

                await _dbConnection.ExecuteAsync(query, purchase, transaction);

                var updateInventoryQuery = @"
                IF EXISTS (SELECT 1 FROM Inventory WHERE ProductID = @ProductID)
                BEGIN
                    UPDATE Inventory 
                    SET Quantity = Quantity + @Quantity, LastUpdated = @LastUpdated 
                    WHERE ProductID = @ProductID
                END
                ELSE
                BEGIN
                    INSERT INTO Inventory (ProductID, Quantity, LastUpdated) 
                    VALUES (@ProductID, @Quantity, @LastUpdated)
                END";

                await _dbConnection.ExecuteAsync(updateInventoryQuery, new
                {
                    purchase.ProductID,
                    purchase.Quantity,
                    LastUpdated = DateTime.Now
                }, transaction);

                transaction.Commit();
                return 1; 
            }
            catch
            {
                transaction.Rollback();
                throw; 
            }
        }

        public async Task<int> UpdatePurchaseAsync(Purchases purchase)
        {
            var query = @"
            UPDATE Purchases 
            SET 
                ProductID = @ProductID,
                PurchaseDate = @PurchaseDate,
                PurchasePrice = @PurchasePrice,
                Quantity = @Quantity,
                TotalCost = @TotalCost
            WHERE ID = @ID";

            return await _dbConnection.ExecuteAsync(query, purchase);
        }

        public async Task<int> DeletePurchaseAsync(int id)
        {
            var query = "DELETE FROM Purchases WHERE ID = @ID";
            var param = new { ID = id };
            return await _dbConnection.ExecuteAsync(query, param);
        }
    }
}
