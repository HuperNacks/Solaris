using Microsoft.AspNetCore.Identity;

namespace Solaris.Service.Services.Interfaces
{
    public interface IRoleServices
    {
        Task<ICollection<IdentityRole>> GetRoles();
    }
}
