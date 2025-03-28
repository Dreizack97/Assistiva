using Entity;

namespace BLL.Interfaces
{
    public interface IStudentDisabilityService
    {
        Task<StudentDisability> CreateAsync(StudentDisability studentDisability);
        Task<StudentDisability> GetByIdAsync(int id);
        Task<IEnumerable<StudentDisability>> GetAllByStudentIdAsync(int studentId);
        Task<bool> UpdateAsync(StudentDisability studentDisability);
        Task<bool> DeleteAsync(int id);
    }
}