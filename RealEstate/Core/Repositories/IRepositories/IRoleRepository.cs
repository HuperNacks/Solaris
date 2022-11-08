using Microsoft.AspNetCore.Identity;
using RealEstate.Areas.Identity.Data;

namespace RealEstate.Core.Repositories.IRepositories
{
    public interface IRoleRepository
    {
        ICollection<IdentityRole> GetRoles();
    }
}
