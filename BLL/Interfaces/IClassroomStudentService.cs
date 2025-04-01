using Entity;

namespace BLL.Interfaces
{
    public interface IClassroomStudentService
    {
        Task<ClassroomStudent> CreateAsync(ClassroomStudent student);
        
        Task<ClassroomStudent> GetByIdAsync(int id);

        Task<IEnumerable<ClassroomStudent>> GetAllByClassroomIdAsync(int classroomId);

        Task<bool> UpdateAsync(ClassroomStudent student);

        Task<bool> DeleteAsync(int classroomId);
    }
}