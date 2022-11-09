

using RealEstate.Core.Entities;

namespace RealEstate.Core.Interfaces
{
    public interface IUserRepository
    {
        ICollection<ApplicationUser> GetUsers();

        ApplicationUser GetUser(string id);

        ApplicationUser UpdateUser(ApplicationUser user);


    }
}
