using Finance.Infrastructure.CustomExceptions;
using Finance.Infrastructure;
using Finance.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finance.Application
{
    public class TypeOperationService : ITypeOperationService
    {
        private readonly IRepositoryManager _repository;

        public TypeOperationService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(TypeOperation operation)
        {
            await _repository.TypeOperation.CreateAsync(operation);
        }

        public async Task DeleteAsync(int id)
        {
            var typeOperation = _repository.TypeOperation.GetById(id);

            if (typeOperation == null)
            {
                throw new NotFoundException();
            }

            await _repository.TypeOperation.DeleteAsync(typeOperation);
        }

        public async Task EditAsync(TypeOperation operation)
        {
            var typeOperation = _repository.TypeOperation.GetById(operation.TypeOperationId);

            if (typeOperation == null)
            {
                throw new NotFoundException();
            }

            await _repository.TypeOperation.EditAsync(operation);
        }

        public async Task<IEnumerable<TypeOperation>> GetAsync()
        {
            return await _repository.TypeOperation.GetAsync();
        }

        public IEnumerable<TypeOperation> GetByType(bool type)
        {
            return _repository.TypeOperation.GetByType(type);
        }
    }
}
