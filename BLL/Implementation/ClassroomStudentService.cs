using BLL.Interfaces;
using DAL.Interfaces;
using Entity;

namespace BLL.Implementation
{
    public class ClassroomStudentService : IClassroomStudentService
    {
        private readonly IGenericRepository<ClassroomStudent> _repository;

        public ClassroomStudentService(IGenericRepository<ClassroomStudent> repository)
        {
            _repository = repository;
        }

        public Task<ClassroomStudent> CreateAsync(ClassroomStudent student)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ClassroomStudent>> GetAllByClassroomIdAsync(int classroomId)
        {
            throw new NotImplementedException();
        }

        public Task<ClassroomStudent> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(ClassroomStudent student)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int classroomId)
        {
            throw new NotImplementedException();
        }
    }
}