﻿
using Microsoft.AspNetCore.Identity;

namespace RealEstate.Core.Interfaces
{
    public interface IRoleRepository
    {
        ICollection<IdentityRole> GetRoles();
    }
}
