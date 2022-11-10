using Microsoft.AspNetCore.Identity;

namespace RealEstate.Service.Services.Interfaces
{
    public interface IRoleServices
    {
        Task<ICollection<IdentityRole>> GetRoles();
    }
}
