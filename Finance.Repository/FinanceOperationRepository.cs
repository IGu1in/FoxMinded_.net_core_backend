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
            await _db.Operations.AddAsync(operation);
        }

        public void Delete(FinanceOperation operation)
        {
            _db.Operations.Remove(operation);
        }

        public void Edit(FinanceOperation operation)
        {
            _db.Operations.Update(operation);
        }

        public async Task<IEnumerable<FinanceOperation>> GetAsync()
        {
            return await _db.Operations.Include(p => p.TypeOperation).ToListAsync();
        }

        public async Task<IEnumerable<FinanceOperation>> GetByDataAsync(DateTime data, bool type)
        {
            return await _db.Operations.Include(p => p.TypeOperation).Where(x => x.TypeOperation.IsIncome == type).Where(x => DateTime.Parse(x.Data) == data).ToListAsync();
        }

        public async Task<FinanceOperation> GetByIdAsync(int id)
        {
            return await _db.Operations.FirstOrDefaultAsync(x => x.FinanceOperationId == id);
        }

        public async Task<IEnumerable<FinanceOperation>> GetByPeriodAsync(DateTime data1, DateTime data2, bool type)
        {
            return await _db.Operations.Include(p => p.TypeOperation).Where(x => x.TypeOperation.IsIncome == type).Where(x => DateTime.Parse(x.Data) > data1).Where(x => DateTime.Parse(x.Data) < data2).ToListAsync();
        }
    }
}
