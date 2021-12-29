using Finance.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finance.Infrastructure
{
    public interface IFinanceOperationService
    {
        Task Create(FinanceOperation oper);
        Task<IEnumerable<FinanceOperation>> Get();
        IEnumerable<object> GetByData(string data);
        IEnumerable<object> GetByPeriod(string data1, string data2);
        Task Edit(FinanceOperation oper);
        Task Delete(int id);
    }
}
