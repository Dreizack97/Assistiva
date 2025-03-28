using BLL.Interfaces;
using DAL.Interfaces;
using Entity;

namespace BLL.Implementation
{
    public class StudentDisabilityService : IStudentDisabilityService
    {
        private readonly IGenericRepository<StudentDisability> _repository;

        public StudentDisabilityService(IGenericRepository<StudentDisability> repository)
        {
            _repository = repository;
        }

        public Task<StudentDisability> CreateAsync(StudentDisability studentDisability)
        {
            throw new NotImplementedException();
        }

        public Task<StudentDisability> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StudentDisability>> GetAllByStudentIdAsync(int studentId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(StudentDisability studentDisability)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}