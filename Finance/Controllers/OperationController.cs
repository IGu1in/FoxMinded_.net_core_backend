using Finance.Exceptions;
using Finance.Infrastructure;
using Finance.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finance.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OperationController : ControllerBase
    {
        private readonly IFinanceOperationService _service;

        public OperationController(IFinanceOperationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FinanceOperation>>> Get()
        {
            return  new ObjectResult(await _service.Get());
        }

        [HttpGet("{dataStr}")]
        public ActionResult<FinanceOperation> Get(string dataStr)
        {
            var result =  _service.GetByData(dataStr);

            return new ObjectResult(result);
        }

        [HttpGet("{dataStr1}/{dataStr2}")]
        public ActionResult<FinanceOperation> Get(string dataStr1, string dataStr2)
        {
            var result = _service.GetByPeriod(dataStr1, dataStr2);

            return new ObjectResult(result);
        }

        [HttpPost]
        public async Task<ActionResult<FinanceOperation>> Post(FinanceOperation operation)
        {
            if (operation == null)
            {
                return BadRequest();
            }

            await _service.Create(operation);

            return Ok(operation);
        }

        [HttpPut]
        public ActionResult<FinanceOperation> Put(FinanceOperation operation)
        {
            if (operation == null)
            {
                return BadRequest();
            }

            try
            {
                _service.Edit(operation);
            }
            catch(NotFoundException)
            {
                return NotFound();
            }

            return Ok(operation);
        }

        [HttpDelete("{id}")]
        public ActionResult<FinanceOperation> Delete(int id)
        {
            try
            {
                _service.Delete(id);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
