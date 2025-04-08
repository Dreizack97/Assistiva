using BLL.Interfaces;
using DAL.Interfaces;
using Entity;

namespace BLL.Implementation
{
    public class SubjectService : ISubjectService
    {
        private readonly IGenericRepository<Subject> _repository;

        public SubjectService(IGenericRepository<Subject> repository)
        {
            _repository = repository;
        }

        public Task<Subject> CreateAsync(Subject subject)
        {
            throw new NotImplementedException();
        }

        public Task<Subject> GetByIdAsync(int subjectId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Subject>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Subject subject)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int subjectId)
        {
            throw new NotImplementedException();
        }
    }
}
