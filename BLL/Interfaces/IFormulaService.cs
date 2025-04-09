using Entity;

namespace BLL.Interfaces
{
    /// <summary>
    /// Define las operaciones del servicio para la gestión de fórmulas matemáticas.
    /// </summary>
    public interface IFormulaService
    {
        /// <summary>
        /// Crea una nueva fórmula de manera asincrónica.
        /// </summary>
        /// <param name="formula">Objeto Formula con los datos a registrar.</param>
        /// <returns>La fórmula creada con su ID generado.</returns>
        Task<Formula> CreateAsync(Formula formula);

        /// <summary>
        /// Obtiene una fórmula por su ID de manera asincrónica.
        /// </summary>
        /// <param name="formulaId">ID de la fórmula a buscar.</param>
        /// <returns>La fórmula encontrada.</returns>
        Task<Formula> GetByIdAsync(int formulaId);

        /// <summary>
        /// Obtiene todas las fórmulas asociadas a una asignatura específica de manera asincrónica.
        /// </summary>
        /// <param name="subjectId">ID de la asignatura para filtrar las fórmulas.</param>
        /// <returns>Colección enumerable de fórmulas.</returns>
        Task<IEnumerable<Formula>> GetAllBySubjectIdAsync(int subjectId);

        /// <summary>
        /// Actualiza los datos de una fórmula existente de manera asincrónica.
        /// </summary>
        /// <param name="formula">Objeto Formula con los datos actualizados.</param>
        /// <returns><c>true</c> si la actualización fue exitosa, <c>false</c> en caso contrario.</returns>
        Task<bool> UpdateAsync(Formula formula);

        /// <summary>
        /// Elimina una fórmula por su ID de manera asincrónica.
        /// </summary>
        /// <param name="formulaId">ID de la fórmula a eliminar.</param>
        /// <returns><c>true</c> si la eliminación fue exitosa, <c>false</c> en caso contrario.</returns>
        Task<bool> DeleteAsync(int formulaId);
    }
}
