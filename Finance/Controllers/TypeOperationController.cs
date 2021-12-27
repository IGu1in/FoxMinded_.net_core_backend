using Finance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finance.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TypeOperationController : ControllerBase
    {
        readonly FinanceContext _db;

        public TypeOperationController(FinanceContext context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeOperation>>> Get()
        {
            return await _db.TypeOperations.ToListAsync();
        }

        [HttpGet("{type}")]
        public ActionResult<TypeOperation> Get(bool type)
        {
            var operation =  _db.TypeOperations.AsParallel().Where(x => x.IsIncome == type);

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

            _db.TypeOperations.Add(operation);
            await _db.SaveChangesAsync();

            return Ok(operation);
        }

        [HttpPut]
        public async Task<ActionResult<TypeOperation>> Put(TypeOperation operation)
        {
            if (operation == null)
            {
                return BadRequest();
            }

            if (!_db.TypeOperations.Any(x => x.TypeOperationId == operation.TypeOperationId))
            {
                return NotFound();
            }

            _db.Update(operation);
            await _db.SaveChangesAsync();

            return Ok(operation);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TypeOperation>> Delete(int id)
        {
            TypeOperation operation = _db.TypeOperations.FirstOrDefault(x => x.TypeOperationId == id);

            if (operation == null)
            {
                return NotFound();
            }

            _db.TypeOperations.Remove(operation);
            await _db.SaveChangesAsync();

            return Ok(operation);
        }
    }
}
