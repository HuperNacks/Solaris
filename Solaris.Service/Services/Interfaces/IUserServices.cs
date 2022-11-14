using Solaris.Core.Entities;

namespace Solaris.Service.Services.Interfaces
{
    public interface IUserServices
    {
        Task<ApplicationUser> GetUser(string id);

        Task<ICollection<ApplicationUser>> GetUsers();

        Task<ApplicationUser> UpdateUser(ApplicationUser user);

        Task<ApplicationUser> DeleteUser(string id);

        Task<ApplicationUser> RecoverUser(ApplicationUser user);

        Task AddUser(ApplicationUser user);


    }
}