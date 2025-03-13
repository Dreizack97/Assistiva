using Entity;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        #region CRUD
        Task<User> CreateAsync(User user);

        Task<User> GetByIdAsync(int userId);

        Task<IEnumerable<User>> GetAllAsync();

        Task<bool> UpdateAsync(User user);

        Task<bool> DeleteAsync(int userId);
        #endregion

        Task<User> SignInAsync(string username, string password);

        Task<bool> IsUsernameOrEmailAvailable(string username, string email, int? userId = null);
    }
}
