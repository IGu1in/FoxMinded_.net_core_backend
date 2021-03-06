using Finance.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finance.Infrastructure
{
    public interface ITypeOperationService
    {
        Task CreateAsync(TypeOperation operation);
        Task<IEnumerable<TypeOperation>> GetAsync();
        Task<IEnumerable<TypeOperation>> GetByTypeAsync(bool type);
        Task EditAsync(TypeOperation operation);
        Task DeleteAsync(int id);
    }
}
