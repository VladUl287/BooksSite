using react_Api.Controllers;
using System.Threading.Tasks;
using react_Api.Database.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace react_Api.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext dbContext;

        public UserRepository(DatabaseContext context)
        {
            this.dbContext = context;
        }

        public async Task Create(User user)
        {
            await dbContext.AddAsync(user);
        }

        public async Task<IEnumerable<User>> Get()
        {
            return await dbContext.Users.ToListAsync();
        }

        public async Task<User> Get(int id)
        {
            return await dbContext.Users
                .Include(e => e.Role)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<User> GetByEmail(string email)
        {
            return await dbContext.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public Task<int> SaveChangesAsync()
        {
            return dbContext.SaveChangesAsync();
        }
    }
}
