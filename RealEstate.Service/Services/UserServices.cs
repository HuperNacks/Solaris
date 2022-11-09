﻿using RealEstate.Core.Entities;
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

        public ApplicationUser UpdateUser(ApplicationUser user)
        {
            return _userRepository.UpdateUser(user);

        }

        public ApplicationUser GetUser(string id)
        {
            return _userRepository.GetUser(id);
        }

        public ICollection<ApplicationUser> GetUsers()
        {
            return _userRepository.GetUsers();
        }
    }
}
