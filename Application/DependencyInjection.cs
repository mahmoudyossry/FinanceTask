using Microsoft.Extensions.DependencyInjection;
using Finance.Application;
using Finance.Application.Services;
using Finance.Application.Services.Interfaces;
using Finance.Core;

namespace Finance.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAuthApplicationDependencyInjection(this IServiceCollection services, AppSettingsConfiguration config)
        {
            services.AddSingleton(config);

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<IRequestService, RequestService>();

            services.AddScoped(typeof(IService<,,>), typeof(BaseService<,,>));
            //services.AddSingleton(typeof(AppSettingsConfiguration));

            return services;
        }
    }
}
