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
        public async Task<ActionResult<FinanceOperation>> GetAsync(string dataStr)
        {
            try
            {
                var result = await _service.GetByDataAsync(dataStr);

                return new ObjectResult(result);
            }
            catch (BadRequestException)
            {
                return BadRequest();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("{dataStr1}/{dataStr2}")]
        public async Task<ActionResult<FinanceOperation>> GetAsync(string dataStr1, string dataStr2)
        {
            try
            {
                var result = await _service.GetByPeriodAsync(dataStr1, dataStr2);

                return new ObjectResult(result);
            }
            catch (BadRequestException)
            {
                return BadRequest();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
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
        public async Task<ActionResult<FinanceOperation>> PutAsync(FinanceOperation operation)
        {
            if (operation == null)
            {
                return BadRequest();
            }

            try
            {
                await _service.EditAsync(operation);
            }
            catch(NotFoundException)
            {
                return NotFound();
            }

            return Ok(operation);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<FinanceOperation>> DeleteAsync(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
