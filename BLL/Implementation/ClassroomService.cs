using BLL.Interfaces;
using DAL.Interfaces;
using Entity;

namespace BLL.Implementation
{
    public class ClassroomService : IClassroomService
    {
        private readonly IGenericRepository<Classroom> _repository;

        public ClassroomService(IGenericRepository<Classroom> repository)
        {
            _repository = repository;
        }

        public async Task<Classroom> CreateAsync(Classroom classroom)
        {
            Classroom? oClassroom = await _repository.GetByFilterAsync(c => c.Name == classroom.Name);

            if (oClassroom != null)
                throw new TaskCanceledException("El nombre de grupo no se encuentra disponible.");

            Classroom _classroom = await _repository.AddAsync(classroom);

            if (_classroom.ClassroomId == 0)
                throw new TaskCanceledException("Ocurrió un error al intentar registrar el grupo.");

            return _classroom;
        }

        public async Task<Classroom> GetByIdAsync(int classroomId)
        {
            Classroom? classroom = await _repository.GetByIdAsync(classroomId)
                ?? throw new TaskCanceledException("No existe grupo con la información proporcionada.");

            return classroom;
        }

        public async Task<IEnumerable<Classroom>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<bool> UpdateAsync(Classroom classroom)
        {
            Classroom? oClassroom = await _repository.GetByFilterAsync(c => c.Name == classroom.Name && c.ClassroomId != classroom.ClassroomId);

            if (oClassroom != null)
                throw new TaskCanceledException("El nombre de grupo no se encuentra disponible.");

            Classroom _classroom = await GetByIdAsync(classroom.ClassroomId);

            _classroom.Name = classroom.Name;

            return await _repository.UpdateAsync(_classroom);
        }

        public async Task<bool> DeleteAsync(int classroomId)
        {
            return await _repository.DeleteAsync(classroomId);
        }
    }
}