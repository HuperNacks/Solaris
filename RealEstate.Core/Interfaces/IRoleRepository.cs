
using Microsoft.AspNetCore.Identity;

namespace RealEstate.Core.Interfaces
{
    public interface IRoleRepository
    {
        Task<ICollection<IdentityRole>> GetRoles();
    }
}
