using Microsoft.AspNetCore.Identity;
using Solaris.Core.Entities;
using Solaris.Core.Interfaces;
using Solaris.Service.Services.Interfaces;

namespace Solaris.Service.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserServices(IUserRepository userRepository, UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<ApplicationUser> UpdateUser(ApplicationUser user)
        {
            await _userManager.UpdateSecurityStampAsync(user);
            return await _userRepository.UpdateUser(user);
        }

        public async Task<ApplicationUser> GetUser(string id)
        {
            return await _userRepository.GetUser(id);
        }

        public async Task<ICollection<ApplicationUser>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        public async Task<ApplicationUser> DeleteUser(string id)
        {
            var user = await _userRepository.GetUser(id);
            await _userManager.UpdateSecurityStampAsync(user);
            user.LockoutEnabled = true;
            user.LockoutEnd = DateTime.MaxValue;
            return await _userRepository.UpdateUser(user);
        }

        public async Task<ApplicationUser> RecoverUser(ApplicationUser user)
        {
            user.LockoutEnabled=false;
            user.LockoutEnd = DateTime.Now;
            return await _userRepository.UpdateUser(user);
        }

        public async Task AddUser(ApplicationUser user)
        {
             await _userRepository.AddUser(user);
        } 
    }
}
