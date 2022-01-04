using Finance.Infrastructure;
using Finance.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finance.Repository
{
    public class TypeOperationRepository : ITypeOperationRepository
    {
        private readonly FinanceContext _db;

        public TypeOperationRepository(FinanceContext context)
        {
            _db = context;
        }

        public async Task CreateAsync(TypeOperation operation)
        {
            await _db.TypeOperations.AddAsync(operation);
        }

        public void Delete(TypeOperation operation)
        {
            _db.TypeOperations.Remove(operation);
        }

        public void Edit(TypeOperation operation)
        {
            _db.TypeOperations.Update(operation);
        }

        public async Task<IEnumerable<TypeOperation>> GetAsync()
        {
            return await _db.TypeOperations.ToListAsync();
        }

        public async Task<TypeOperation> GetByIdAsync(int id)
        {
            return await _db.TypeOperations.FirstOrDefaultAsync(x => x.TypeOperationId == id);
        }

        public async Task<IEnumerable<TypeOperation>> GetByTypeAsync(bool type)
        {
            return await _db.TypeOperations.Where(x => x.IsIncome == type).ToListAsync();
        }
    }
}
