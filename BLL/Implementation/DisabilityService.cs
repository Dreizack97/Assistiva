using BLL.Interfaces;
using DAL.Interfaces;
using Entity;

namespace BLL.Implementation
{
    public class DisabilityService : IDisabilityService
    {
        private readonly IGenericRepository<Disability> _repository;

        public DisabilityService(IGenericRepository<Disability> repository)
        {
            _repository = repository;
        }

        public Task<Disability> CreateAsync(Disability disability)
        {
            throw new NotImplementedException();
        }

        public Task<Disability> GetByIdAsync(int disabilityId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Disability>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Disability disability)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DisableAsync(int disabilityId)
        {
            throw new NotImplementedException();
        }
    }
}
