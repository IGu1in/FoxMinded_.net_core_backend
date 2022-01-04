﻿using Finance.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finance.Infrastructure
{
    public interface IFinanceOperationService
    {
        Task CreateAsync(FinanceOperation operation);
        Task<IEnumerable<FinanceOperation>> GetAsync();
        Task<IEnumerable<object>> GetByDataAsync(string data);
        Task<IEnumerable<object>> GetByPeriodAsync(string data1, string data2);
        Task EditAsync(FinanceOperation operation);
        Task DeleteAsync(int id);
    }
}
