using APIs.Model;
using Dapper;
using System.Data;

namespace APIs.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _connection;
        private readonly IConfiguration _configuration;

        public UserRepository(IDbConnection connection, IConfiguration configuration)
        {
            _connection = connection;
            _configuration = configuration;
        }
        public async Task<int> AddUserAsync(User user)
        {
            user.password = BCrypt.Net.BCrypt.HashPassword(user.password);
            var sql = "insert into Users (userName, phoneNumber , password , role)" + "values (@userName, @phoneNumber, @password, @role)";
            return await _connection.ExecuteAsync(sql, user);
        }

        public async Task<int> DeleteUserAsync(int id)
        {
            var sql = "delete from Users where Id = @Id";
            return await _connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var sql = "select * from Users";
            return await _connection.QueryAsync<User>(sql);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var sql = "select * from Users where Id = Id";
            return await  _connection.QueryFirstOrDefaultAsync<User>(sql, new {Id = id});
        }

        public async Task<int> UpdateUserAsync(User user)
        {
            if(!string.IsNullOrEmpty(user.password))
            {
                user.password = BCrypt.Net.BCrypt.HashPassword(user.password);
            }
            var sql = "update Users set userName = @userName, phoneNumber = @phoneNumber, password = @password, role = @role where Id = @Id";
            return await _connection.ExecuteAsync(sql, user);
        }

        public async Task<User> AuthenticateUserAsync(string userName, string password)
        {
            var sql = "SELECT * FROM Users WHERE UserName = @userName";
            var user = await _connection.QueryFirstOrDefaultAsync<User>(sql, new { userName });

            // Kiểm tra mật khẩu
            if (user != null)
            {
                //Console.WriteLine($"Hash Password: {user.password}"); 
                if (BCrypt.Net.BCrypt.Verify(password, user.password))
                {
                    return user;
                }
            }

            return null;
        }

        public async Task<int> SaveRefreshTokenAsync(int userId, string refreshToken)
        {
            var sql = "UPDATE Users SET RefreshToken = @RefreshToken, RefreshTokenExpiryDate = @ExpiryDate WHERE Id = @UserId";
            return await _connection.ExecuteAsync(sql, new
            {
                RefreshToken = refreshToken,
                ExpiryDate = DateTime.UtcNow.AddDays(int.Parse(_configuration["Jwt:RefreshTokenExpiresInDays"])),
                UserId = userId
            });
        }

        public async Task<bool> ValidateRefreshTokenAsync(string userId, string refreshToken)
        {
            var sql = "SELECT RefreshToken FROM Users WHERE Id = @UserId";
            var storedRefreshToken = await _connection.QuerySingleOrDefaultAsync<string>(sql, new { UserId = userId });

            return storedRefreshToken == refreshToken;
        }
        public async Task RemoveRefreshTokenAsync(string userId)
        {
            var sql = "UPDATE Users SET RefreshToken = NULL WHERE Id = @Id"; // Hoặc xóa nếu bạn lưu refreshToken trong bảng riêng
            await _connection.ExecuteAsync(sql, new { Id = userId });
        }
    }
}
