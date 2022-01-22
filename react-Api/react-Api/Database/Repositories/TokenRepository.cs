using Microsoft.EntityFrameworkCore;
using react_Api.Database.Interfaces;
using react_Api.Database.Models;
using System.Threading.Tasks;

namespace react_Api.Database.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly DatabaseContext dbContext;

        public TokenRepository(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task DeleteToken(string token)
        {
            var dbToken = await dbContext.Tokens.FirstOrDefaultAsync(x => x.RefreshToken == token);

            dbContext.Tokens.Remove(dbToken);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Token> GetToken(string token)
        {
            return await dbContext.Tokens.FirstOrDefaultAsync(x => x.RefreshToken == token);
        }

        public async Task SaveToken(int userId, string token)
        {
            var dbToken = await dbContext.Tokens.FindAsync(userId);

            if (dbToken is null)
            {
                await dbContext.Tokens.AddAsync(new Models.Token { UserId = userId, RefreshToken = token });
            }
            else
            {
                dbToken.RefreshToken = token;
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
