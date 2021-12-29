using Finance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Infrastructure
{
    public interface ITypeOperationRepository
    {
        Task Create(TypeOperation oper);
        Task<IEnumerable<TypeOperation>> Get();
        IEnumerable<TypeOperation> GetByType(bool type);
        TypeOperation GetById(int id);
        Task Edit(TypeOperation oper);
        Task Delete(TypeOperation oper);
    }
}
