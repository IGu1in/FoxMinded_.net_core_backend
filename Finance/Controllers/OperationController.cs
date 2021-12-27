using Finance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finance.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OperationController : ControllerBase
    {
        readonly FinanceContext _db;

        public OperationController(FinanceContext context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FinanceOperation>>> Get()
        {
            return await _db.Operations.Include(p => p.TypeOperation).ToListAsync();
        }

        [HttpGet("{dataStr}")]
        public ActionResult<FinanceOperation> Get(string dataStr)
        {
            var isData = DateTime.TryParse(dataStr, out var data);

            if (!isData)
            {
                return BadRequest();
            }

            var listIncome = new List<FinanceOperation>();
            var listExpence = new List<FinanceOperation>();
            decimal sumIncome = 0;
            decimal sumExpence = 0;
            var result = new List<object>();

            listIncome = _db.Operations.Include(p => p.TypeOperation).AsParallel().Where(x => x.TypeOperation.IsIncome == true).Where(x=>DateTime.Parse(x.Data) == data).ToList();
            listExpence = _db.Operations.Include(p => p.TypeOperation).AsParallel().Where(x => x.TypeOperation.IsIncome == false).Where(x => DateTime.Parse(x.Data) == data).ToList();

            if (listExpence.Count == 0 && listIncome.Count==0)
            {
                return NotFound();
            }

            foreach(var oper in listIncome)
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

            return new ObjectResult(result);
        }

        [HttpGet("{dataStr1}/{dataStr2}")]
        public ActionResult<FinanceOperation> Get(string dataStr1, string dataStr2)
        {
            var isData1 = DateTime.TryParse(dataStr1, out var data1);
            var isData2 = DateTime.TryParse(dataStr2, out var data2);

            if(!isData1 || !isData2)
            {
                return BadRequest();
            }

            var listIncome = new List<FinanceOperation>();
            var listExpence = new List<FinanceOperation>();
            decimal sumIncome = 0;
            decimal sumExpence = 0;
            var result = new List<object>();

            listIncome = _db.Operations.Include(p => p.TypeOperation).AsParallel().Where(x => x.TypeOperation.IsIncome == true).Where(x => DateTime.Parse(x.Data) > data1).Where(x => DateTime.Parse(x.Data) < data2).ToList();
            listExpence = _db.Operations.Include(p => p.TypeOperation).AsParallel().Where(x => x.TypeOperation.IsIncome == false).Where(x => DateTime.Parse(x.Data) > data1).Where(x => DateTime.Parse(x.Data) < data2).ToList();

            if (listExpence.Count == 0 && listIncome.Count == 0)
            {
                return NotFound();
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

            return new ObjectResult(result);
        }

        [HttpPost]
        public async Task<ActionResult<FinanceOperation>> Post(FinanceOperation operation)
        {
            if (operation == null)
            {
                return BadRequest();
            }

            _db.Operations.Add(operation);
            await _db.SaveChangesAsync();

            return Ok(operation);
        }

        [HttpPut]
        public async Task<ActionResult<FinanceOperation>> Put(FinanceOperation operation)
        {
            if (operation == null)
            {
                return BadRequest();
            }

            if (!_db.Operations.Any(x => x.FinanceOperationId == operation.FinanceOperationId))
            {
                return NotFound();
            }

            _db.Update(operation);
            await _db.SaveChangesAsync();

            return Ok(operation);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<FinanceOperation>> Delete(int id)
        {
            FinanceOperation operation = _db.Operations.FirstOrDefault(x => x.FinanceOperationId == id);

            if (operation == null)
            {
                return NotFound();
            }

            _db.Operations.Remove(operation);
            await _db.SaveChangesAsync();

            return Ok(operation);
        }
    }
}
