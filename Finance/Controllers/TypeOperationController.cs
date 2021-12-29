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
    public class TypeOperationController : ControllerBase
    {
        private readonly ITypeOperationService _service;

        public TypeOperationController(ITypeOperationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeOperation>>> Get()
        {
            return new ObjectResult(await _service.Get());
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
        public async Task<ActionResult<TypeOperation>> Post(TypeOperation operation)
        {
            if (operation == null)
            {
                return BadRequest();
            }

            await _service.Create(operation);

            return Ok(operation);
        }

        [HttpPut]
        public ActionResult<TypeOperation> Put(TypeOperation operation)
        {
            if (operation == null)
            {
                return BadRequest();
            }

            try
            {
                _service.Edit(operation);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return Ok(operation);
        }

        [HttpDelete("{id}")]
        public ActionResult<TypeOperation> Delete(int id)
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
