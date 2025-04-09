using BLL.Interfaces;
using DAL.Interfaces;
using Entity;

namespace BLL.Implementation
{
    public class FormulaService : IFormulaService
    {
        private readonly IGenericRepository<Formula> _repository;

        public FormulaService(IGenericRepository<Formula> repository)
        {
            _repository = repository;
        }

        public Task<Formula> CreateAsync(Formula formula)
        {
            throw new NotImplementedException();
        }

        public Task<Formula> GetByIdAsync(int formulaId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Formula>> GetAllBySubjectIdAsync(int subjectId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Formula formula)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int formulaId)
        {
            throw new NotImplementedException();
        }
    }
}
