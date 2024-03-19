using Microsoft.EntityFrameworkCore;
using Finance.Core.Entities;
using Finance.Core.Entities.Authorization;
using Finance.Core.Repositories;
using Finance.Infrastructure.Data;
using Finance.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Repositories
{
    public class PermissionRepository : Repository<Permission>, IPermissionRepository
    {
        private readonly DbSet<Permission> dbSet;

        public PermissionRepository(FinanceContext _econtext) : base(_econtext)
        {
            dbSet = context.Set<Permission>();
        }

        public async Task<List<Permission>> GetAllAsList()
        {
            return await dbSet.AsQueryable().ToListAsync();

        }
    }
}
