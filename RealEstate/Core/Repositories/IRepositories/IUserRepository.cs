using RealEstate.Areas.Identity.Data;

namespace RealEstate.Core.Repositories.IRepositories
{
    public interface IUserRepository
    {
        ICollection<ApplicationUser> GetUsers();

        ApplicationUser GetUser(string id);

        ApplicationUser UpdateUser(ApplicationUser user);
    }
}
