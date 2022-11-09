﻿using Microsoft.AspNetCore.Identity;
using RealEstate.Core.Interfaces;
using RealEstate.Service.Services.Interfaces;

namespace RealEstate.Service.Services
{
    public class RoleServices : IRoleServices
    {
        private readonly IRoleRepository _roleServices;

        public RoleServices(IRoleRepository roleServices)
        {
            _roleServices = roleServices;
        }

        public ICollection<IdentityRole> GetRoles()
        {
            return _roleServices.GetRoles();
        }
    }
}