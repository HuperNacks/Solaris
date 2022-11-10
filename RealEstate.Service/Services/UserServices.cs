using RealEstate.Core.Entities;
using RealEstate.Core.Interfaces;
using RealEstate.Service.Services.Interfaces;

namespace RealEstate.Service.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;

        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ApplicationUser> UpdateUser(ApplicationUser user)
        {
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
            user.LockoutEnabled = true;
            user.LockoutEnd = DateTime.MaxValue;
            return await _userRepository.UpdateUser(user);
        }

        public async Task<ApplicationUser> RecoverUser(ApplicationUser user)
        {
            user.LockoutEnabled=false;
            return await _userRepository.UpdateUser(user);
        }
    }
}
