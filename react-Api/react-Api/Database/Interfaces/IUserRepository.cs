using System.Threading.Tasks;
using react_Api.Database.Models;
using System.Collections.Generic;

namespace react_Api.Controllers
{
    public interface IUserRepository
    {
        Task Create(User user);
        Task<User> Get(int id);
        Task<IEnumerable<User>> Get();
        Task<User> GetByEmail(string email);
        Task<int> SaveChangesAsync();
    }
}