using Microsoft.AspNetCore.Identity;
using Solaris.Core.Interfaces;
using Solaris.Service.Services.Interfaces;

namespace Solaris.Service.Services
{
    public class RoleServices : IRoleServices
    {
        private readonly IRoleRepository _roleServices;

        public RoleServices(IRoleRepository roleServices)
        {
             _roleServices = roleServices;
        }

        public async Task<ICollection<IdentityRole>> GetRoles()
        {
            return await _roleServices.GetRoles();
        }
    }
}
