using Finance.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finance.Infrastructure
{
    public interface IFinanceOperationRepository
    {
        Task CreateAsync(FinanceOperation operation);
        Task<IEnumerable<FinanceOperation>> GetAsync();
        FinanceOperation GetById(int id);
        IEnumerable<FinanceOperation> GetByData(DateTime data, bool type);
        IEnumerable<FinanceOperation> GetByPeriod(DateTime data1, DateTime data2, bool type);
        void Edit(FinanceOperation operation);
        void Delete(FinanceOperation operation);
    }
}
