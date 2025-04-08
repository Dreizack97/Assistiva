using Entity;

namespace BLL.Interfaces
{
    public interface ISubjectService
    {
        Task<Subject> CreateAsync(Subject subject);

        Task<Subject> GetByIdAsync(int subjectId);

        Task<IEnumerable<Subject>> GetAllAsync();

        Task<bool> UpdateAsync(Subject subject);

        Task<bool> DeleteAsync(int subjectId);
    }
}
