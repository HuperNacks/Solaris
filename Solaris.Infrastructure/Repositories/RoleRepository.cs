using Microsoft.AspNetCore.Identity;
using Solaris.Infrastructure.Data;
using Solaris.Core.Interfaces;


namespace Solaris.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ICollection<IdentityRole>>GetRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
