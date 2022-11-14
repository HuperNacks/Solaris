using Solaris.Infrastructure.Data;
using Solaris.Core.Interfaces;
using Solaris.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Solaris.Infrastructure.Repositories
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

        public async Task AddUser(ApplicationUser user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

    }
}
