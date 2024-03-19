using Finance.Core.Entities;
using Finance.Core.Entities.Authorization;
using Finance.Core.Repositories;
using Finance.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Core.UnitOfWork
{
    public interface IUnitOfWork
    {
        IPermissionRepository Permission { get; }
        IRequestRepository Request { get; }

        Task<int> CompleteAsync();

        void BeginTran();

        void CommitTran();

        void RollbackTran();

        object GetRepositoryByName(string name);
    }
}
