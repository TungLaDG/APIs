using Dapper;

using APIs.Model;
using System.Data;

namespace APIs.Repositories;
public class ProductRepository : IProductRepository
{
    private readonly IDbConnection _dbConnection;

    public ProductRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        var sql = "SELECT * FROM Products";
        return await _dbConnection.QueryAsync<Product>(sql);
    }
    public async Task<IEnumerable<Product>> GetAllProductsByCategoryIdAsync(int categoryId)
    {
        var sql = "SELECT * FROM Products WHERE CategoryID = @CategoryID";
        return await _dbConnection.QueryAsync<Product>(sql, new { categoryId = categoryId });
    }

    public async Task<Product> GetProductByIdAsync(int productId)
    {
        var sql = "SELECT * FROM Products WHERE Id = @Id";
        return await _dbConnection.QueryFirstOrDefaultAsync<Product>(sql, new { Id = productId });
    }

    public async Task<int> AddProductAsync(Product product)
    {
        var sql = "INSERT INTO Products (ProductName, Price, CategoryId, CreatedAt, UpdatedAt,ImageURL,Description) " +
                  "VALUES (@ProductName, @Price, @CategoryId, @CreatedAt, @UpdatedAt,@ImageURL,@Description)";
        return await _dbConnection.ExecuteAsync(sql, product);
    }

    public async Task<int> UpdateProductAsync(Product product)
    {
        var sql = "UPDATE Products SET ProductName = @ProductName, Price = @Price, " +
                  "CategoryId = @CategoryId,CreatedAt = @CreatedAt ,UpdatedAt = @UpdatedAt, ImageURL = @ImageURL, Description = @Description WHERE Id = @Id";
        return await _dbConnection.ExecuteAsync(sql, product);
    }

    public async Task<int> DeleteProductAsync(int productId)
    {
        var sql = "DELETE FROM Products WHERE Id = @Id";
        return await _dbConnection.ExecuteAsync(sql, new { Id = productId });
    }

    
}