using Entity;

namespace BLL.Interfaces
{
    public interface IClassroomSubjectService
    {
        Task<ClassroomSubject> CreateAsync(ClassroomSubject classroomSubject);

        Task<ClassroomSubject> GetByIdAsync(int id);

        Task<IEnumerable<ClassroomSubject>> GetAllByClassroomIdAsync(int classroomId);

        Task<bool> Update(ClassroomSubject classroomSubject);

        Task<bool> DeleteAsync(int id);
    }
}
