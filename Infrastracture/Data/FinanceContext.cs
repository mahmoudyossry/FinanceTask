using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Finance.Core.Entities;
using Finance.Core.Entities.Authorization;
using Finance.Core.Entities.Base;
using Finance.Infrastructure.DataConfiguration;
using Finance.Core.Global;
using Finance.Infrastructure.Identity;

namespace Finance.Infrastructure.Data
{
    public class FinanceContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        private readonly GlobalInfo globalInfo;

        private readonly UserRolesConfiguration userRolesConfiguration;
        private readonly PermissionConfiguration permissionConfiguration;
        private readonly RoleConfiguration roleConfiguration;
        private readonly RequestConfiguration requestConfiguration;
        private readonly StatusConfiguration statusConfiguration;
        private readonly RolePermissionConfiguration rolePermissionConfiguration;
        private readonly UserConfiguration userConfiguration;

        public FinanceContext(DbContextOptions<FinanceContext> options
                        , GlobalInfo globalInfo

                        , UserRolesConfiguration userRolesConfiguration
                        , RolePermissionConfiguration rolePermissionConfiguration
                        , RoleConfiguration roleConfiguration
                        , RequestConfiguration requestConfiguration
                        , StatusConfiguration statusConfiguration
                        , PermissionConfiguration permissionConfiguration
                        , UserConfiguration userConfiguration
            ) : base(options)
        {
            this.globalInfo = globalInfo;

            this.userRolesConfiguration = userRolesConfiguration;
            this.permissionConfiguration = permissionConfiguration;
            this.roleConfiguration = roleConfiguration;
            this.requestConfiguration = requestConfiguration;
            this.statusConfiguration = statusConfiguration;
            this.rolePermissionConfiguration = rolePermissionConfiguration;
            this.userConfiguration = userConfiguration;
        }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestStatus> RequestStatues { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SetGlobalFilters(builder);
            ApplyConfigurations(builder);
        }

        #region SaveChanges 
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            BeforeSaveProccess();

            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            BeforeSaveProccess();

            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            BeforeSaveProccess();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        #endregion
        private void ApplyConfigurations(ModelBuilder builder)
        {
            builder.ApplyConfiguration(permissionConfiguration);
            builder.ApplyConfiguration(userConfiguration);
            builder.ApplyConfiguration(userRolesConfiguration);
            builder.ApplyConfiguration(roleConfiguration);
            builder.ApplyConfiguration(statusConfiguration);
            builder.ApplyConfiguration(requestConfiguration);
            builder.ApplyConfiguration(rolePermissionConfiguration);
        }

        private void SetGlobalFilters(ModelBuilder builder)
        {
            builder.SetQueryFilterOnAllEntities<ISoftDelete>(p => !p.IsDeleted);
        }

        private void BeforeSaveProccess()
        {
            var changes = from e in this.ChangeTracker.Entries()
                          where e.State != EntityState.Unchanged
                          select e;

            foreach (var change in changes)
            {
                if (change.State == EntityState.Added)
                {
                    //if (change.Entity.GetType() == typeof(IAuditEntityx<>))
                    if (change.Entity is IAudit)
                    {
                        ((IAudit)change.Entity).CreateUser = globalInfo.UserId;
                        ((IAudit)change.Entity).CreateUserName = globalInfo.UserName;
                        ((IAudit)change.Entity).CreateDate = DateTime.Now;
                    }
                }
            }
        }
    }
}
