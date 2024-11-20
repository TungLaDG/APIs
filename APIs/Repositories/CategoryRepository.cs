using APIs.Model;
using Dapper;
using System.Data;
using System.Data.Common;

namespace APIs.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IDbConnection _dbConnection;
        public CategoryRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<IEnumerable<Categories>> GetAllCategoriesAsync()
        {
            var sql = "Select * from Categories";
            return await _dbConnection.QueryAsync<Categories>(sql);
        }
        public async Task<Categories> GetCategoryByIdAsync(int categoryId)
        {
            var sql = "SELECT * FROM Categories WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Categories>(sql, new { Id = categoryId });
        }

        public async Task<int> UpdateCategoryAsync(Categories category)
        {
            var sql = "update categories set categoryName = @categoryName where id = @id";
            return await _dbConnection.ExecuteAsync(sql, category);
        }

        public async Task<int> AddCategoryAsync(Categories category)
        {
            var sql = "insert into categories (categoryName)" + " values(@categoryName)";
            return await _dbConnection.ExecuteAsync(sql, category);
        }

        public Task DeleteCategoryAsync(int id)
        {
            var sql = "delete from categories where id = @id";
            return _dbConnection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
