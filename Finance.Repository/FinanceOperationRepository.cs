using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Finance.Infrastructure;
using Finance.Models;
using Microsoft.EntityFrameworkCore;

namespace Finance.Repository
{
    public class FinanceOperationRepository : IFinanceOperationRepository
    {
        private readonly FinanceContext _db;
        private readonly IMapper _mapper;

        public FinanceOperationRepository(FinanceContext context, IMapper mapper)
        {
            _db = context;
            _mapper = mapper;
        }

        public async Task CreateAsync(ViewModel.FinanceOperation operation)
        {
            await _db.Operations.AddAsync(_mapper.Map<FinanceOperation>(operation));
        }

        public void Delete(ViewModel.FinanceOperation operation)
        {
            _db.Entry(_mapper.Map<FinanceOperation>(operation)).State = EntityState.Deleted;
        }

        public void Edit(ViewModel.FinanceOperation operationWithOldData, ViewModel.FinanceOperation operationWithNewData)
        {
            _db.Entry(_mapper.Map<FinanceOperation>(operationWithOldData)).CurrentValues.SetValues(_mapper.Map<FinanceOperation>(operationWithNewData));
        }

        public async Task<IEnumerable<ViewModel.FinanceOperation>> GetAsync()
        {
            var listOperation = await _db.Operations.Include(p => p.TypeOperation).ToListAsync();
                
            return _mapper.Map<IEnumerable<ViewModel.FinanceOperation>>(listOperation);
        }

        public async Task<IEnumerable<ViewModel.FinanceOperation>> GetByDataAsync(DateTime data, bool type)
        {
            var listOperation = await _db.Operations.Include(p => p.TypeOperation).Where(x => x.TypeOperation.IsIncome == type).Where(x => DateTime.Parse(x.Data) == data).ToListAsync();
            
            return _mapper.Map<IEnumerable<ViewModel.FinanceOperation>>(listOperation);
        }

        public async Task<ViewModel.FinanceOperation> GetByIdAsync(int id)
        {
            var operation = await _db.Operations.FirstOrDefaultAsync(x => x.FinanceOperationId == id);

            return _mapper.Map<ViewModel.FinanceOperation>(operation);
            //return operation;
        }

        public async Task<IEnumerable<ViewModel.FinanceOperation>> GetByPeriodAsync(DateTime data1, DateTime data2, bool type)
        {
            var listOperation = await _db.Operations.Include(p => p.TypeOperation).Where(x => x.TypeOperation.IsIncome == type).Where(x => DateTime.Parse(x.Data) > data1).Where(x => DateTime.Parse(x.Data) < data2).ToListAsync();
            
            return _mapper.Map<IEnumerable<ViewModel.FinanceOperation>>(listOperation);
        }
    }
}
