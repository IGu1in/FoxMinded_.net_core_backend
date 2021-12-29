using Finance.Exceptions;
using Finance.Infrastructure;
using Finance.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finance.Application
{
    public class TypeOperationService : ITypeOperationService
    {
        private readonly ITypeOperationRepository _repository;

        public TypeOperationService(ITypeOperationRepository repository)
        {
            _repository = repository;
        }

        public async Task Create(TypeOperation oper)
        {
            await _repository.Create(oper);
        }

        public async Task Delete(int id)
        {
            var typeOperation = _repository.GetById(id);

            if (typeOperation == null)
            {
                throw new NotFoundException();
            }

            await _repository.Delete(typeOperation);
        }

        public async Task Edit(TypeOperation oper)
        {
            var typeOperation = _repository.GetById(oper.TypeOperationId);

            if (typeOperation == null)
            {
                throw new NotFoundException();
            }

            await _repository.Edit(oper);
        }

        public async Task<IEnumerable<TypeOperation>> Get()
        {
            return await _repository.Get();
        }

        public IEnumerable<TypeOperation> GetByType(bool type)
        {
            return _repository.GetByType(type);
        }
    }
}
