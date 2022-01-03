using Finance.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finance.Infrastructure
{
    public interface ITypeOperationRepository
    {
        Task CreateAsync(TypeOperation operation);
        Task<IEnumerable<TypeOperation>> GetAsync();
        IEnumerable<TypeOperation> GetByType(bool type);
        TypeOperation GetById(int id);
        void Edit(TypeOperation operation);
        void Delete(TypeOperation operation);
    }
}
