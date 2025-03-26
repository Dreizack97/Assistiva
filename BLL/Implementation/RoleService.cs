using BLL.Interfaces;
using DAL.Interfaces;
using Entity;

namespace BLL.Implementation
{
    public class RoleService : IRoleService
    {
        private readonly IGenericRepository<Role> _repository;

        public RoleService(IGenericRepository<Role> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
