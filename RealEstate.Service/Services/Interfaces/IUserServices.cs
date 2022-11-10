using RealEstate.Core.Entities;

namespace RealEstate.Service.Services.Interfaces
{
    public interface IUserServices
    {
        ApplicationUser GetUser(string id);

        ICollection<ApplicationUser> GetUsers();
        ApplicationUser UpdateUser(ApplicationUser user);

        ApplicationUser DeleteUser(ApplicationUser user);

        ApplicationUser RecoverUser(ApplicationUser user);


    }
}