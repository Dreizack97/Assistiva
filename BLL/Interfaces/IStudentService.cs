using Entity;

namespace BLL.Interfaces
{
    public interface IStudentService
    {
        Task<Student> CreateAsync(Student student);
        Task<Student> GetByIdAsync(int studentId);
        Task<IEnumerable<Student>> GetAllAsync();
        Task<bool> UpdateAsync(Student student);
        Task<bool> DisableAsync(int studentId);
    }
}
