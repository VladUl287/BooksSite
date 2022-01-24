using System;
using react_Api.Models;
using react_Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using react_Api.Database.Models;
using Microsoft.Extensions.Configuration;
using react_Api.Database;
using Microsoft.EntityFrameworkCore;

namespace react_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DatabaseContext databaseContext;
        private readonly string passwordKey = string.Empty;
        private readonly string accessTokenKey = string.Empty;
        private readonly string refreshTokenKey = string.Empty;

        public AuthController(
            DatabaseContext databaseContext,
            IConfiguration configuration)
        {
            this.databaseContext = databaseContext;
            passwordKey = configuration.GetValue<string>("Secrets:PasswordSecret");
            accessTokenKey = configuration.GetValue<string>("Secrets:JwtAccessSecret");
            refreshTokenKey = configuration.GetValue<string>("Secrets:JwtRefreshSecret");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var user = await databaseContext.Users
                .AsNoTracking()
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Email == login.Email);

            var hashPassword = HashService.Hash(login.Password, passwordKey);

            if (user is not null && user.Password == hashPassword)
            {
                GenerateTokens(user, out string accessToken, out string refreshToken);

                await databaseContext.Tokens
                    .AddAsync(new Token
                    {
                        UserId = user.Id,
                        RefreshToken = refreshToken
                    });
                await databaseContext.SaveChangesAsync();

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
            var exists = await databaseContext.Users
                .AnyAsync(e => e.Email == register.Email);

            if (exists)
            {
                return BadRequest(new { error = "Пользователь с таким email уже существует." });
            }

            var user = new User
            {
                Email = register.Email,
                Login = register.Login,
                Password = register.Password,
                RoleId = 2
            };

            await databaseContext.AddAsync(user);
            await databaseContext.SaveChangesAsync();

            return CreatedAtAction($"user/{user.Id}", user);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var refreshToken = Request.Cookies["token"];

            if (refreshToken is not null)
            {
                await databaseContext.Database.ExecuteSqlInterpolatedAsync(
                    $"DELETE FROM [Tokens] WHERE [RefreshToken] LIKE {refreshToken}");

                Response.Cookies.Delete("token");
            }

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

            var dbToken = await databaseContext.Tokens
                .AsNoTracking()
                .Include(x => x.User)
                .ThenInclude(x => x.Role)
                .FirstOrDefaultAsync(x => x.RefreshToken == cookieToken);

            if (dbToken is null)
            {
                Response.Cookies.Delete("token");

                return Unauthorized();
            }

            var valid = JwtService.ValidateToken(dbToken.RefreshToken, refreshTokenKey);

            if (!valid)
            {
                databaseContext.Tokens.Remove(dbToken);
                await databaseContext.SaveChangesAsync();

                Response.Cookies.Delete("token");

                return Unauthorized();
            }

            var accessToken = JwtService.Generate(
                           dbToken.User.Id,
                           dbToken.User.Email,
                           dbToken.User.Role.Name,
                           accessTokenKey,
                           DateTime.Now.AddMinutes(15));

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
