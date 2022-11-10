using RealEstate.Infrastructure.Data;
using RealEstate.Core.Interfaces;
using RealEstate.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace RealEstate.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationUser> GetUser(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<ICollection<ApplicationUser>>GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<ApplicationUser> UpdateUser(ApplicationUser user)
        { 
            _context.Update(user);
            await _context.SaveChangesAsync();

            return user;
        }


     
    }
}
