using Finance.Core.Entities.Authorization;
using Finance.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Core.Repositories
{
    public interface IPermissionRepository : IRepository<Permission>
    {
        Task<List<Permission>> GetAllAsList();

    }
}
