using Microsoft.EntityFrameworkCore.ChangeTracking;
using RealEstate.Areas.Identity.Data;
using RealEstate.Core.Repositories.IRepositories;

namespace RealEstate.Core.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ApplicationUser GetUser(string id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public ICollection<ApplicationUser> GetUsers()
        {
            return _context.Users.ToList();
        }

        public ApplicationUser UpdateUser(ApplicationUser user)
        { 
            _context.Update(user);
            _context.SaveChanges();

            return user;
        }
    }
}
