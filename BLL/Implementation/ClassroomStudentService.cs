using BLL.Interfaces;
using DAL.Interfaces;
using Entity;

namespace BLL.Implementation
{
    /// <summary>
    /// Implementación concreta de <see cref="IClassroomStudentService"/> para gestionar la relación estudiantes-aulas.
    /// </summary>
    /// <remarks>
    /// Esta clase utiliza un repositorio genérico para interactuar con la capa de acceso a datos.
    /// <para>
    /// Validaciones clave incluidas:
    /// <list type="bullet">
    /// <item>Evita asignaciones duplicadas de estudiantes en un mismo aula</item>
    /// <item>Verifica la existencia de registros antes de operaciones críticas</item>
    /// </list>
    /// </para>
    /// </remarks>
    public class ClassroomStudentService : IClassroomStudentService
    {
        private readonly IGenericRepository<ClassroomStudent> _repository;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ClassroomStudentService"/>.
        /// </summary>
        /// <param name="repository">Repositorio genérico para operaciones CRUD. Debe ser inyectado mediante DI.</param>
        public ClassroomStudentService(IGenericRepository<ClassroomStudent> repository)
        {
            _repository = repository;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Lógica adicional:
        /// <list type="number">
        /// <item>Verifica que el estudiante no esté previamente registrado en el mismo aula</item>
        /// <item>Valida que el ID generado después de la inserción sea mayor a 0</item>
        /// </list>
        /// </remarks>
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

        /// <inheritdoc/>
        /// <remarks>
        /// Comportamiento específico:
        /// <para>Incluye una verificación de nullabilidad para garantizar la integridad de los datos.</para>
        /// </remarks>
        public async Task<ClassroomStudent> GetByIdAsync(int id)
        {
            ClassroomStudent? student = await _repository.GetByIdAsync(id)
                ?? throw new TaskCanceledException("No se encontró estudiante con la información proporcionada.");

            return student;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Detalles de implementación:
        /// <para>Utiliza una expresión lambda para filtrar por <c>classroomId</c> en la capa de repositorio.</para>
        /// </remarks>
        public Task<IEnumerable<ClassroomStudent>> GetAllByClassroomIdAsync(int classroomId)
        {
            return _repository.GetAllAsync(e => e.ClassroomId == classroomId, [s => s.Student]);
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Flujo de actualización:
        /// <list type="number">
        /// <item>Verifica conflictos de asignación duplicada</item>
        /// <item>Obtiene la entidad existente para mantener la trazabilidad</item>
        /// <item>Actualiza solo campos permitidos (ClassroomId y StudentId)</item>
        /// </list>
        /// </remarks>
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

        /// <inheritdoc/>
        /// <remarks>
        /// Consideraciones:
        /// <para>La eliminación es física y depende de la implementación del repositorio subyacente.</para>
        /// </remarks>
        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}