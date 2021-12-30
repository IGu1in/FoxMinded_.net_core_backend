using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finance.Infrastructure;
using Finance.Models;
using Microsoft.EntityFrameworkCore;

namespace Finance.Repository
{
    public class FinanceOperationRepository : IFinanceOperationRepository
    {
        private readonly FinanceContext _db;

        public FinanceOperationRepository(FinanceContext context)
        {
            _db = context;
        }

        public async Task CreateAsync(FinanceOperation operation)
        {
            _db.Operations.Add(operation);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(FinanceOperation operation)
        {
            _db.Operations.Remove(operation);
            await _db.SaveChangesAsync();
        }

        public async Task EditAsync(FinanceOperation operation)
        {
            _db.Update(operation);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<FinanceOperation>> GetAsync()
        {
            return await _db.Operations.Include(p => p.TypeOperation).ToListAsync();
        }

        public IEnumerable<FinanceOperation> GetByData(DateTime data, bool type)
        {
            return _db.Operations.Include(p => p.TypeOperation).AsParallel().Where(x => x.TypeOperation.IsIncome == type).Where(x => DateTime.Parse(x.Data) == data).ToList();
        }

        public FinanceOperation GetById(int id)
        {
            return _db.Operations.FirstOrDefault(x => x.FinanceOperationId == id);
        }

        public IEnumerable<FinanceOperation> GetByPeriod(DateTime data1, DateTime data2, bool type)
        {
            return _db.Operations.Include(p => p.TypeOperation).AsParallel().Where(x => x.TypeOperation.IsIncome == type).Where(x => DateTime.Parse(x.Data) > data1).Where(x => DateTime.Parse(x.Data) < data2).ToList();
        }
    }
}
