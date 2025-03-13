using BLL.Interfaces;
using BLL.Utilities;
using DAL.Interfaces;
using Entity;

namespace BLL.Implementation
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _repository;

        public UserService(IGenericRepository<User> repository)
        {
            _repository = repository;
        }

        #region CRUD
        public Task<User> CreateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int userId)
        {
            throw new NotImplementedException();
        }
        #endregion

        public Task<User> SignInAsync(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsUsernameOrEmailAvailable(string username, string email, int? userId = null)
        {
            throw new NotImplementedException();
        }
    }
}
