using Solaris.Core.Entities;
using Solaris.Core.Interfaces;
using Solaris.Infrastructure.Repositories;
using Solaris.Service.Services;
using Solaris.Service.Services.Interfaces;

namespace Solaris.Extensions
{
    public static class Helpers 
    {
        public static void AddAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Constants.Policies.RequireMaster, policy => policy.RequireRole(Constants.Roles.Master));
                options.AddPolicy(Constants.Policies.RequireAdmin, policy => policy.RequireRole(Constants.Roles.Admin));
            });
        }

        public static void AddScoped(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IRoleServices, RoleServices>();
        }
    }
}
