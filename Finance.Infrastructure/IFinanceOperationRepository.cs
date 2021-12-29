using Finance.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finance.Infrastructure
{
    public interface IFinanceOperationRepository
    {
        Task Create(FinanceOperation oper);
        Task<IEnumerable<FinanceOperation>> Get();
        FinanceOperation GetById(int id);
        IEnumerable<FinanceOperation> GetByData(DateTime data, bool type);
        IEnumerable<FinanceOperation> GetByPeriod(DateTime data1, DateTime data2, bool type);
        Task Edit(FinanceOperation oper);
        Task Delete(FinanceOperation oper);
    }
}
