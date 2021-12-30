using Finance.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finance.Infrastructure
{
    public interface ITypeOperationService
    {
        Task CreateAsync(TypeOperation operation);
        Task<IEnumerable<TypeOperation>> GetAsync();
        IEnumerable<TypeOperation> GetByType(bool type);
        Task EditAsync(TypeOperation operation);
        Task DeleteAsync(int id);
    }
}
