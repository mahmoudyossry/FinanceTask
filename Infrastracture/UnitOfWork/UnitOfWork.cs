using Microsoft.AspNetCore.Identity;
using Finance.Core.Entities;
using Finance.Core.Entities.Authorization;
using Finance.Core.Repositories;
using Finance.Core.Repositories.Base;
using Finance.Core.UnitOfWork;
using Finance.Infrastructure.Data;
using Finance.Core.Global;
using Finance.Infrastructure.Identity;
using Finance.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Infrastructure.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FinanceContext context;

        public IPermissionRepository Permission { get; }
        public IRequestRepository Request { get; }

        public UnitOfWork(FinanceContext _context
            , IPermissionRepository Permission
            , IRequestRepository requestRepository

            )
        {
            context = _context;
            this.Permission = Permission;
            Request = requestRepository;
        }

        public async Task<int> CompleteAsync()
        {
            try
            {
                return await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                RollbackTran();
                throw ex;
            }

        }

        public object GetRepositoryByName(string name)
        {
            Type type = this.GetType();
            PropertyInfo info = type.GetProperty(name);
            if (info == null)
                throw new AppException(ExceptionEnum.PropertyNotAccess, name, type.FullName);
            //type.FullName, String.Format("A property called {0} can't be accessed for type {1}.", name));

            return info.GetValue(this, null);
        }

        public void BeginTran()
        {
            context.Database.BeginTransaction();
        }

        public void CommitTran()
        {
            context.Database.CommitTransaction();
        }

        public void RollbackTran()
        {
            var transaction = context.Database.CurrentTransaction;
            if (transaction != null)
                context.Database.RollbackTransaction();
        }
    }
}
