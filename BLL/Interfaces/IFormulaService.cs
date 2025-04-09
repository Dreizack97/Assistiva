using Entity;

namespace BLL.Interfaces
{
    public interface IFormulaService
    {
        Task<Formula> CreateAsync(Formula formula);

        Task<Formula> GetByIdAsync(int formulaId);

        Task<IEnumerable<Formula>> GetAllBySubjectIdAsync(int subjectId);

        Task<bool> UpdateAsync(Formula formula);

        Task<bool> DeleteAsync(int formulaId);
    }
}
