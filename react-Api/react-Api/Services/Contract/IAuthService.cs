using api.Database.Models;
using api.Models;
using api.ViewModels;
using OneOf;
using System.Threading.Tasks;

namespace api.Services.Contract
{
    public interface IAuthService
    {
        Task<OneOf<LoginSuccess, NotValidToken>> Refresh(string token);

        Task<OneOf<LoginSuccess, NotCorrectData>> Login(LoginModel login);

        Task<OneOf<User, EmailAlreadyExists>> Register(RegisterModel register);

        Task Logout(string token);
    }
}