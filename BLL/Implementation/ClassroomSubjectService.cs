using BLL.Interfaces;
using DAL.Interfaces;
using Entity;

namespace BLL.Implementation
{
    public class ClassroomSubjectService : IClassroomSubjectService
    {
        private readonly IGenericRepository<ClassroomSubject> _repository;

        public ClassroomSubjectService(IGenericRepository<ClassroomSubject> repository)
        {
            _repository = repository;
        }

        public Task<ClassroomSubject> CreateAsync(ClassroomSubject classroomSubject)
        {
            throw new NotImplementedException();
        }

        public Task<ClassroomSubject> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ClassroomSubject>> GetAllByClassroomIdAsync(int classroomId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(ClassroomSubject classroomSubject)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
