using Finance.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finance.Infrastructure
{
    public interface IFinanceOperationService
    {
        Task CreateAsync(FinanceOperation operation);
        Task<IEnumerable<FinanceOperation>> GetAsync();
        IEnumerable<object> GetByData(string data);
        IEnumerable<object> GetByPeriod(string data1, string data2);
        Task EditAsync(FinanceOperation operation);
        Task DeleteAsync(int id);
    }
}
