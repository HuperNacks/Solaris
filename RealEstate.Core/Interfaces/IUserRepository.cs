

using RealEstate.Core.Entities;

namespace RealEstate.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<ICollection<ApplicationUser>>GetUsers();

        Task<ApplicationUser> GetUser(string id);

        Task<ApplicationUser> UpdateUser(ApplicationUser user);

        Task AddUser(ApplicationUser user);
    }
}
