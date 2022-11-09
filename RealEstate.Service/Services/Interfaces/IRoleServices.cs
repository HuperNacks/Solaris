using Microsoft.AspNetCore.Identity;

namespace RealEstate.Service.Services.Interfaces
{
    public interface IRoleServices
    {
        ICollection<IdentityRole> GetRoles();
    }
}
