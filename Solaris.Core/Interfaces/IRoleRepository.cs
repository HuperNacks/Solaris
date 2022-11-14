
using Microsoft.AspNetCore.Identity;

namespace Solaris.Core.Interfaces
{
    public interface IRoleRepository
    {
        Task<ICollection<IdentityRole>> GetRoles();
    }
}
