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
            _db.TypeOperations.Add(operation);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(TypeOperation operation)
        {
            _db.TypeOperations.Remove(operation);
            await _db.SaveChangesAsync();
        }

        public async Task EditAsync(TypeOperation operation)
        {
            _db.Update(operation);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<TypeOperation>> GetAsync()
        {
            return await _db.TypeOperations.ToListAsync();
        }

        public TypeOperation GetById(int id)
        {
            return _db.TypeOperations.FirstOrDefault(x => x.TypeOperationId == id);
        }

        public IEnumerable<TypeOperation> GetByType(bool type)
        {
            return _db.TypeOperations.AsParallel().Where(x => x.IsIncome == type);
        }
    }
}
