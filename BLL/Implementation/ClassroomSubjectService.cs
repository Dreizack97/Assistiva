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

        public async Task<ClassroomSubject> CreateAsync(ClassroomSubject classroomSubject)
        {
            ClassroomSubject? oClassroomSubject = await _repository.GetByFilterAsync(c => c.ClassroomId == classroomSubject.ClassroomId && c.SubjectId == classroomSubject.SubjectId);

            if (oClassroomSubject != null)
                throw new TaskCanceledException("La materia ya se encuentra registrada al grupo.");

            ClassroomSubject _classroomSubject = await _repository.AddAsync(classroomSubject);

            if (_classroomSubject.Id == 0)
                throw new TaskCanceledException("Ocurrió un problema al intentar registrar la materia al grupo.");

            return _classroomSubject;
        }

        public async Task<ClassroomSubject> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id)
                ?? throw new TaskCanceledException("No se ha encontrado una relación con la información proporcionada.");
        }

        public async Task<IEnumerable<ClassroomSubject>> GetAllByClassroomIdAsync(int classroomId)
        {
            return await _repository.GetAllAsync(c => c.ClassroomId == classroomId);
        }

        public async Task<bool> Update(ClassroomSubject classroomSubject)
        {
            ClassroomSubject? oClassroomSubject = await _repository.GetByFilterAsync(c => c.ClassroomId == classroomSubject.ClassroomId && c.SubjectId == classroomSubject.SubjectId && c.Id != classroomSubject.Id);

            if (oClassroomSubject != null)
                throw new TaskCanceledException("La materia ya se encuentra registrada al grupo.");

            ClassroomSubject _classroomSubject = await GetByIdAsync(classroomSubject.Id);

            _classroomSubject.SubjectId = classroomSubject.SubjectId;

            return await _repository.UpdateAsync(_classroomSubject);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
