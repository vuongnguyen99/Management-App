using CloudinaryDotNet;
using Management.Common.Common;
using Management.Common.Exception;
using Management.Core.Models.Authenticate;
using Management.Core.Services;
using Management.Data;
using Management.Data.Entities;
using Management_Core.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Management.Core.Services
{
    public interface IAuthenticateService 
    {
        Task<ResponseAuthenticate> LoginSystem(Authenticate request, CancellationToken cancellationToken);
    }
    public class AuthenticateServices : IAuthenticateService
    {
        private readonly ManagementDbContext _context;
        private readonly IConfiguration _configuration;
        private int CountLoginFail = 3;
        public AuthenticateServices(ManagementDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<ResponseAuthenticate> LoginSystem(Authenticate request, CancellationToken cancellationToken)
        {
            var getUser = await _context.Users.FirstOrDefaultAsync(x => x.Username == request.UserName, cancellationToken);
            if(getUser == null)
            {
                throw new NotFoundException($"Cann't find user {request.UserName} in DB.");
            }
            if(!getUser.Active)
            {
                throw new ValidationException($"User with username {request.UserName} is locked. Please contact with admin to open account.");
            }
            var verifiedPassword = PasswordHelper.VerifiedPassword(request.Password, getUser.PasswordHash);
            if(!verifiedPassword)
            {
                getUser.LoginFailedCount++;
                if(getUser.LoginFailedCount == CountLoginFail)
                    getUser.Active = false;
                _context.Update(getUser);
                await _context.SaveChangesAsync();
                throw new ValidationException("Username or password is incorrect.");
            }
            else
            {
                getUser.LoginFailedCount = 0;
                _context.Update(getUser);
                await _context.SaveChangesAsync();
            }

            var accessToken = GenerateToken(getUser);
            return new ResponseAuthenticate { AccessToken = accessToken };
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authenticate:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            var token = new JwtSecurityToken(_configuration["Authenticate:Issuer"],
                _configuration["Authenticate:Audience"],
                claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
