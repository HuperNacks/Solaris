using Microsoft.AspNetCore.Identity;

namespace RealEstate.Core.Entities
{

    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

    }

    public class ApplicationRole : IdentityRole
    {

    }
}

