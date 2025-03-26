using Entity;

namespace BLL.Interfaces
{
    public interface IDisabilityService
    {
        Task<Disability> CreateAsync(Disability disability);

        Task<Disability> GetByIdAsync(int disabilityId);

        Task<IEnumerable<Disability>> GetAllAsync();

        Task<bool> UpdateAsync(Disability disability);

        Task<bool> DisableAsync(int disabilityId);
    }
}
