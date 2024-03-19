using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Finance.Core.Repositories.Base;
using Finance.Infrastructure.Data;
using Finance.Infrastructure.Repositories;
using Finance.Infrastructure.Repositories.Base;
using System.Configuration;
using Finance.Core.UnitOfWork;
using Finance.Infrastructure.UnitOfWorks;
using Finance.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Finance.Core.Global;
using Finance.Core.Repositories;
using Finance.Infrastructure.DataConfiguration;
using Finance.Core;
using Finance.Core.Entities;

namespace Finance.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAuthInfrastructureDependencyInjection(this IServiceCollection services, AppSettingsConfiguration settings)
        {

            // For ERP Identity
            services.AddDbContext<FinanceContext>(
                m => m.UseSqlServer(settings.ConnectionStrings.FinanceDB));

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<FinanceContext>()
                //.AddUserManager<ApplicationUserManager>()
                .AddDefaultTokenProviders();

            // Overwrite the default password complixity
            services.Configure<IdentityOptions>(option => {
                option.Password.RequiredLength = 6;
                option.Password.RequireUppercase = false;
                option.Password.RequireLowercase = false;
                option.Password.RequireDigit = false;
                option.Password.RequireNonAlphanumeric = false;
                option.User.RequireUniqueEmail = false;
            }
            );

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped(typeof(IPermissionRepository), typeof(PermissionRepository));
            services.AddScoped(typeof(IRequestRepository), typeof(RequestRepository));

            services.AddScoped(typeof(IRolePermissionRepository<RolePermission>), typeof(RolePermissionRepository));
            services.AddScoped(typeof(IUserRoleRepository<IdentityUserRole<string>>), typeof(UserRoleRepository));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ApplicationUserManager>();
            services.AddScoped<GlobalInfo>();
            services.AddScoped(typeof(IPasswordHasher<>), typeof(PasswordHasher<>));


            services.AddScoped<PermissionConfiguration>();
            services.AddScoped<StatusConfiguration>();
            services.AddScoped<RequestConfiguration>();
            services.AddScoped<RoleConfiguration>();
            services.AddScoped<RolePermissionConfiguration>();
            services.AddScoped<UserConfiguration>();
            services.AddScoped<UserRolesConfiguration>();

            
            return services;
        }
    }
}
