using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.Services.Interfaces
{
    public interface ILogService<TEntity>
    {
        Task Create(TEntity input);
    }
}
