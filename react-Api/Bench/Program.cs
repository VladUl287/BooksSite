using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Buffers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bench
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<Benchmark>();
        }
    }

    [MemoryDiagnoser]
    public class Benchmark
    {
        [Benchmark]
        public void Generate()
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1234567890123456"));
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var claims = new Claim[]
            {
                new Claim("id", 1.ToString()),
                new Claim("role", "User")
            };

            var securityToken = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: credentials);
            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        [Benchmark]
        public void Generate_Array_Pool()
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1234567890123456"));
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var shared = ArrayPool<Claim>.Shared;
            var claims = shared.Rent(2);
            claims[0] = new Claim("id", 1.ToString());
            claims[1] = new Claim("role", "User");

            var securityToken = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: credentials);
            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            shared.Return(claims);
        }
    }
}