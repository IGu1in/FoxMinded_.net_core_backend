using Finance.Infrastructure.CustomExceptions;
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
        public async Task<ActionResult<IEnumerable<FinanceOperation>>> GetAsync()
        {
            return  new ObjectResult(await _service.GetAsync());
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
        public async Task<ActionResult<FinanceOperation>> PostAsync(FinanceOperation operation)
        {
            if (operation == null)
            {
                return BadRequest();
            }

            await _service.CreateAsync(operation);

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
                _service.EditAsync(operation);
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
                _service.DeleteAsync(id);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
