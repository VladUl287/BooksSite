using react_Api.Database.Models;
using System.Threading.Tasks;

namespace react_Api.Database.Interfaces
{
    public interface ITokenRepository
    {
        Task<Token> GetToken(string token);
        Task SaveToken(int userId, string token);
        Task DeleteToken(string token);
    }
}
