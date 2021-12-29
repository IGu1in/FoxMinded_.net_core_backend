using Finance.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finance.Infrastructure
{
    public interface ITypeOperationService
    {
        Task Create(TypeOperation oper);
        Task<IEnumerable<TypeOperation>> Get();
        IEnumerable<TypeOperation> GetByType(bool type);
        Task Edit(TypeOperation oper);
        Task Delete(int id);
    }
}
