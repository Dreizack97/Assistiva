using BLL.Interfaces;
using DAL.Interfaces;
using Entity;

namespace BLL.Implementation
{
    public class StudentService : IStudentService
    {
        private readonly IGenericRepository<Student> _repository;
        private readonly IUserService _userService;

        public StudentService(IGenericRepository<Student> repository, IUserService userService)
        {
            _repository = repository;
            _userService = userService;
        }

        public Task<Student> CreateAsync(Student student)
        {
            throw new NotImplementedException();
        }

        public Task<Student> GetByIdAsync(int studentId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Student>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Student student)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DisableAsync(int studentId)
        {
            throw new NotImplementedException();
        }
    }
}
