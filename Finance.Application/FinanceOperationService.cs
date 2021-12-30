using Finance.Infrastructure.CustomExceptions;
using Finance.Infrastructure;
using Finance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finance.Application
{
    public class FinanceOperationService : IFinanceOperationService
    {
        private readonly IRepositoryManager _repository;

        public FinanceOperationService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(FinanceOperation operation)
        {
            await _repository.FinanceOperation.CreateAsync(operation);
        }

        public async Task DeleteAsync(int id)
        {
            var financeOperation = _repository.FinanceOperation.GetById(id);

            if (financeOperation == null)
            {
                throw new NotFoundException();
            }

            await _repository.FinanceOperation.DeleteAsync(financeOperation);
        }

        public async Task EditAsync(FinanceOperation operation)
        {
            var financeOperation = _repository.FinanceOperation.GetById(operation.FinanceOperationId);

            if (financeOperation == null)
            {
                throw new NotFoundException();
            }

            await _repository.FinanceOperation.EditAsync(operation);
        }

        public async Task<IEnumerable<FinanceOperation>> GetAsync()
        {
            return await _repository.FinanceOperation.GetAsync();
        }

        public IEnumerable<object> GetByData(string dataStr)
        {
            var isData = DateTime.TryParse(dataStr, out var data);

            if (!isData)
            {
                throw new BadRequestException();
            }

            decimal sumIncome = 0;
            decimal sumExpence = 0;
            var result = new List<object>();

            var listIncome = _repository.FinanceOperation.GetByData(data, true).ToList();
            var listExpence = _repository.FinanceOperation.GetByData(data, false).ToList();

            if (listExpence.Count == 0 && listIncome.Count == 0)
            {
                throw new NotFoundException();
            }

            foreach (var oper in listIncome)
            {
                sumIncome += oper.Value;
            }

            foreach (var oper in listExpence)
            {
                sumExpence += oper.Value;
            }

            result.Add(sumIncome);
            result.Add(sumExpence);
            result.Add(listIncome);
            result.Add(listExpence);

            return result;
        }

        public IEnumerable<object> GetByPeriod(string dataStr1, string dataStr2)
        {
            var isData1 = DateTime.TryParse(dataStr1, out var data1);
            var isData2 = DateTime.TryParse(dataStr2, out var data2);

            if (!isData1 || !isData2)
            {
                new BadRequestException();
            }

            decimal sumIncome = 0;
            decimal sumExpence = 0;
            var result = new List<object>();

            var listIncome = _repository.FinanceOperation.GetByPeriod(data1, data2, true).ToList();
            var listExpence = _repository.FinanceOperation.GetByPeriod(data1, data2, false).ToList();

            if (listExpence.Count == 0 && listIncome.Count == 0)
            {
                new NotFoundException();
            }

            foreach (var oper in listIncome)
            {
                sumIncome += oper.Value;
            }

            foreach (var oper in listExpence)
            {
                sumExpence += oper.Value;
            }

            result.Add(sumIncome);
            result.Add(sumExpence);
            result.Add(listIncome);
            result.Add(listExpence);

            return result;
        }
    }
}
