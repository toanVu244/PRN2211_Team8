using JPOS.Model;
using JPOS.Model.Entities;
using JPOS.Model.Models;
using JPOS.Model.Models.AppConfig;
using JPOS.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Service.Implementations
{
    public class UserServices : IUserServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppConfig _appConfig;

        public UserServices(IUnitOfWork unitOfWork, AppConfig appConfig)
        {
            _unitOfWork = unitOfWork;
            _appConfig = appConfig;
        }
        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appConfig.jwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim("userId", user.UserId),
                new Claim("userName", user.Username),
                new Claim("role", user.RoleId.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7), 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public async Task<string> AuthenticateAsync(string username, string password)
        {
            var hashedInputPasswordString = HashAndTruncatePassword(password);
            var user = await _unitOfWork.Users.GetUserByUsernameAndPasswordAsync(username, hashedInputPasswordString);
            
            if (hashedInputPasswordString == user.Password)
            {
                if (user == null)
                    return "";
            }

                var token = GenerateJwtToken(user);

                return token;
            
        }
        public async Task<string> GenerateNextUserIDAsync()
        {
            var lastUser = await _unitOfWork.Users.GetLastUserAsync();

            if (lastUser == null || lastUser.UserId.Length < 3)
            {
                // In case there are no users yet or UserId length is less than 3
                return "US00001";
            }

            int numericPart = int.Parse(lastUser.UserId.Substring(2)); 

            int nextNumericPart = numericPart + 1;
            string nextUserID = "US" + nextNumericPart.ToString("D5"); 

            return nextUserID;
        }
        public async Task<bool> UserRegister(RegisterModel model)
        {
            string nextUserID = await GenerateNextUserIDAsync();
            var hashedInputPasswordString = HashAndTruncatePassword(model.Password);
            model.Password = hashedInputPasswordString;
            var newUser = new User
            {
                UserId = nextUserID,
                Username = model.Username,
                Password = model.Password,
                FullName = model.FullName,
                Address = model.Address,
                PhoneNum = model.PhoneNum,
                Status = true,
                RoleId = 1,
                CreateDate = DateTime.Now,
                Email = model.Email
            };
            var result = await _unitOfWork.Users.InsertAsync(newUser);
            await _unitOfWork.CompleteAsync();

            return result;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _unitOfWork.Users.GetAllAsync();
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            return await _unitOfWork.Users.GetByIdAsync(id);
        }
        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _unitOfWork.Users.GetByUsernameAsync(username);
        }
        public async Task<bool> CreateUserAsync(User user)
        {
            string nextUserID = await GenerateNextUserIDAsync();
            user.UserId = nextUserID;
            var hashedInputPasswordString = HashAndTruncatePassword(user.Password);
            user.Password = hashedInputPasswordString;
            var result = await _unitOfWork.Users.InsertAsync(user);
            await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            var result = await _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            var result = await _unitOfWork.Users.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
           if (email == null)
            {
                return null;
            }
           return await _unitOfWork.Users.GetUserByEmail(email);
        }

        public string HashAndTruncatePassword(string password)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
                password = BitConverter.ToString(result).Replace("-", "").ToLowerInvariant();
            }

            // Truncate hash to 16 characters
            password = password.Substring(0, 16);

            return password;
        }

        public async Task<List<Model.Entities.Role>> GetAllRolesAsync()
        {
            return await _unitOfWork.Users.GetAllRolesAsync();
        }
    }
}
