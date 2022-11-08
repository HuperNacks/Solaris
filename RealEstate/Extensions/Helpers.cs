
using RealEstate.Core;
using RealEstate.Core.Repositories;
using RealEstate.Core.Repositories.IRepositories;

namespace RealEstate.Extensions
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
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRoleRepository, RoleRepository>();
        }
    }
}
