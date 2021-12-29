using Finance.Exceptions;
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
        private readonly IFinanceOperationRepository _repository;

        public FinanceOperationService(IFinanceOperationRepository repository)
        {
            _repository = repository;
        }

        public async Task Create(FinanceOperation oper)
        {
            await _repository.Create(oper);
        }

        public async Task Delete(int id)
        {
            var financeOperation = _repository.GetById(id);

            if (financeOperation == null)
            {
                throw new NotFoundException();
            }

            await _repository.Delete(financeOperation);
        }

        public async Task Edit(FinanceOperation oper)
        {
            var financeOperation = _repository.GetById(oper.FinanceOperationId);

            if (financeOperation == null)
            {
                throw new NotFoundException();
            }

            await _repository.Edit(oper);
        }

        public async Task<IEnumerable<FinanceOperation>> Get()
        {
            return await _repository.Get();
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

            var listIncome = _repository.GetByData(data, true).ToList();
            var listExpence = _repository.GetByData(data, false).ToList();

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

            var listIncome = _repository.GetByPeriod(data1, data2, true).ToList();
            var listExpence = _repository.GetByPeriod(data1, data2, false).ToList();

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
