﻿using Microsoft.AspNetCore.Identity;
using RealEstate.Infrastructure.Data;
using RealEstate.Core.Interfaces;


namespace RealEstate.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;
        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public ICollection<IdentityRole>GetRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
