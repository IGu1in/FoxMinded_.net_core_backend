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
    public class TypeOperationController : ControllerBase
    {
        private readonly ITypeOperationService _service;

        public TypeOperationController(ITypeOperationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeOperation>>> GetAsync()
        {
            return new ObjectResult(await _service.GetAsync());
        }

        [HttpGet("{type}")]
        public ActionResult<TypeOperation> Get(bool type)
        {
            var operation = _service.GetByType(type);

            if (operation == null)
            {
                return NotFound();
            }

            return new ObjectResult(operation);
        }

        [HttpPost]
        public async Task<ActionResult<TypeOperation>> PostAsync(TypeOperation operation)
        {
            if (operation == null)
            {
                return BadRequest();
            }

            await _service.CreateAsync(operation);

            return Ok(operation);
        }

        [HttpPut]
        public ActionResult<TypeOperation> PutAsync(TypeOperation operation)
        {
            if (operation == null)
            {
                return BadRequest();
            }

            try
            {
                _service.EditAsync(operation);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return Ok(operation);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TypeOperation>> DeleteAsync(int id)
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
