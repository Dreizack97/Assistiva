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

        public async Task<ClassroomStudent> CreateAsync(ClassroomStudent student)
        {
            ClassroomStudent? oStudent = await _repository.GetByFilterAsync(s => s.ClassroomId == student.ClassroomId && s.StudentId == student.Id);

            if (oStudent != null)
                throw new TaskCanceledException("El estudiante ya se encuentra asignado al grupo.");

            ClassroomStudent _student = await _repository.AddAsync(student);

            if (_student.Id == 0)
                throw new TaskCanceledException("Ocurrió un error al intentar registrar al estudiante.");

            return _student;
        }

        public async Task<ClassroomStudent> GetByIdAsync(int id)
        {
            ClassroomStudent? student = await _repository.GetByIdAsync(id)
                ?? throw new TaskCanceledException("No se encontró estudiante con la información proporcionada.");

            return student;
        }

        public Task<IEnumerable<ClassroomStudent>> GetAllByClassroomIdAsync(int classroomId)
        {
            return _repository.GetAllAsync(e => e.ClassroomId == classroomId);
        }

        public async Task<bool> UpdateAsync(ClassroomStudent student)
        {
            ClassroomStudent? oStudent = await _repository.GetByFilterAsync(s => s.ClassroomId == student.ClassroomId && s.StudentId == student.Id);

            if (oStudent != null)
                throw new TaskCanceledException("El estudiante ya se encuentra asignado al grupo.");

            ClassroomStudent _student = await GetByIdAsync(student.Id);

            _student.ClassroomId = student.ClassroomId;
            _student.StudentId = student.StudentId;

            return await _repository.UpdateAsync(_student);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}