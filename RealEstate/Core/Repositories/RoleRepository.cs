using Microsoft.AspNetCore.Identity;
using RealEstate.Areas.Identity.Data;
using RealEstate.Core.Repositories.IRepositories;

namespace RealEstate.Core.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;
        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public ICollection<IdentityRole>GetRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
