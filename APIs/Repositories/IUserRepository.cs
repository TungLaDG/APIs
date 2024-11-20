using APIs.Model;

namespace APIs.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<int> AddUserAsync(User user);
        Task<int> UpdateUserAsync(User user);
        Task<int> DeleteUserAsync(int id);
        Task<User> AuthenticateUserAsync(string userName, string password);
        Task<int> SaveRefreshTokenAsync(int userId, string refreshToken);
        Task<bool> ValidateRefreshTokenAsync(string userId, string refreshToken);
        Task RemoveRefreshTokenAsync(string userId);

    }
}
