using System;
using react_Api.Models;
using react_Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using react_Api.Database.Models;
using react_Api.Database.Interfaces;
using Microsoft.Extensions.Configuration;

namespace react_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenRepository tokenRepository;
        private readonly string passwordKey = string.Empty;
        private readonly string accessTokenKey = string.Empty;
        private readonly string refreshTokenKey = string.Empty;

        public AuthController(
            IUserRepository userRepository,
            ITokenRepository tokenRepository,
            IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.tokenRepository = tokenRepository;
            passwordKey = configuration.GetValue<string>("Secrets:PasswordSecret");
            accessTokenKey = configuration.GetValue<string>("Secrets:JwtAccessSecret");
            refreshTokenKey = configuration.GetValue<string>("Secrets:JwtRefreshSecret");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var user = await userRepository.GetByEmail(login.Email);
            var hashPassword = HashService.Hash(login.Password, passwordKey);

            if (user is not null && user.Password == hashPassword)
            {
                GenerateTokens(user, out string accessToken, out string refreshToken);

                await tokenRepository.SaveToken(user.Id, refreshToken);

                Response.Cookies.Append("token", refreshToken, new Microsoft.AspNetCore.Http.CookieOptions
                {
                    MaxAge = TimeSpan.FromDays(30)
                });

                return Ok(new { token = accessToken });
            }

            return BadRequest(new { error = "Неверный email или пароль." });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel register)
        {
            var user = new User
            {
                Email = register.Email,
                Login = register.Login,
                Password = register.Password,
                RoleId = 2
            };

            await userRepository.Create(user);
            await userRepository.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var refreshToken = Request.Cookies["token"];

            await tokenRepository.DeleteToken(refreshToken);

            Response.Cookies.Delete("token");

            return NoContent();
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            var cookieToken = Request.Cookies["token"];

            if (cookieToken is null)
            {
                return Unauthorized();
            }

            var dbToken = await tokenRepository.GetToken(cookieToken);
            var valid = JwtService.ValidateToken(cookieToken, refreshTokenKey);
            if (!valid || dbToken is null)
            {
                Response.Cookies.Delete("token");
                return Unauthorized();
            }

            var user = await userRepository.Get(dbToken.UserId);

            GenerateTokens(user, out string accessToken, out string refreshToken);

            await tokenRepository.SaveToken(user.Id, refreshToken);

            Response.Cookies.Append("token", refreshToken, new Microsoft.AspNetCore.Http.CookieOptions
            {
                MaxAge = TimeSpan.FromDays(30)
            });

            return Ok(new { token = accessToken });
        }

        private void GenerateTokens(User user, out string accessToken, out string refreshToken)
        {
            accessToken = JwtService.Generate(
                            user.Id,
                            user.Email,
                            user.Role.Name,
                            accessTokenKey,
                            DateTime.Now.AddMinutes(15));
            refreshToken = JwtService.Generate(
                            user.Id,
                            user.Email,
                            user.Role.Name,
                            refreshTokenKey,
                            DateTime.Now.AddDays(30));
        }
    }
}
