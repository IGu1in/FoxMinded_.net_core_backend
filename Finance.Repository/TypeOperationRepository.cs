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

        public async Task Create(TypeOperation oper)
        {
            _db.TypeOperations.Add(oper);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(TypeOperation oper)
        {
            _db.TypeOperations.Remove(oper);
            await _db.SaveChangesAsync();
        }

        public async Task Edit(TypeOperation oper)
        {
            _db.Update(oper);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<TypeOperation>> Get()
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
