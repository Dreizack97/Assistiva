using Entity;

namespace BLL.Interfaces
{
    public interface IClassroomService
    {
        Task<Classroom> CreateAsync(Classroom classroom);
        Task<Classroom> GetByIdAsync(int classroomId);
        Task<IEnumerable<Classroom>> GetAllAsync();
        Task<bool> UpdateAsync(Classroom classroom);
        Task<bool> DeleteAsync(int classroomId);
    }
}