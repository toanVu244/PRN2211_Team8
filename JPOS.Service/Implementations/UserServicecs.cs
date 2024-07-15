using CloudinaryDotNet.Actions;
using JPOS.Model;
using JPOS.Model.Entities;
using JPOS.Model.Models;
using JPOS.Model.Models.AppConfig;
using JPOS.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Service.Implementations
{
    public class UserServices : IUserServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache cache;

        public UserServices(IUnitOfWork unitOfWork, IMemoryCache memoryCache)
        {
            _unitOfWork = unitOfWork;
            cache = memoryCache;
        }
        private string GenerateJwtToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("UserId", user.UserId.ToString()),
                new Claim("UserName", user.FullName),
                new Claim("Email", user.Email),
                new Claim("Role", user.RoleId.ToString()),
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("c2VydmVwZXJmZWN0bHljaGVlc2VxdWlja2NvYWNoY29sbGVjdHNsb3Bld2lzZWNhbWU="));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "YourIssuer",
                audience: "YourAudience",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
                );
            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            return accessToken;
        }


        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var hashedInputPasswordString = HashAndTruncatePassword(password);
            var user = await _unitOfWork.Users.GetByUsernameAsync(username);
            if (user != null)
            {
                if (hashedInputPasswordString == user.Password)
                {
                    return user;
                }
            }
            return null;
        }
        public async Task<string> GenerateNextUserIDAsync()
        {
            var lastUser = await _unitOfWork.Users.GetLastUserAsync();
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


        public void sendmail(string mail, string body)
        {
            try
            {
                if (string.IsNullOrEmpty(mail) || string.IsNullOrEmpty(body))
                {
                    return;
                }
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("jpos.application@gmail.com", "ehvu emtf npjy qjay"),
                    EnableSsl = true,
                };
                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("jpos.application@gmail.com"),
                    Subject = "OTP for reset Password",
                    Body = "Do not share your OTP - Your OTP : " + body,
                    IsBodyHtml = false, // Set to true if you're using HTML in the email body
                };

                // Add recipient email address
                mailMessage.To.Add(mail);

                // Send the email
                smtpClient.Send(mailMessage);
                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

        public string GenerateRandomOTP()
        {
            Random random = new Random();
            int otpValue = 0;

            // Loop để đảm bảo rằng mã OTP không bắt đầu bằng số 0
            while (otpValue < 100000)
            {
                otpValue = random.Next(100000, 999999);
            }

            return otpValue.ToString();
        }


        public async Task<bool> ConfirmEmail(string email)
        {
            try
            {
                var token = await GetUserByEmail(email);
                if (token == null)
                {
                    return false;
                }
                var OTPsave = GenerateRandomOTP();
                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromMinutes(2),
                    Size = 1024,
                };
                cache.Set(email, OTPsave, cacheExpiryOptions);
                sendmail(email, OTPsave);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<bool> ResetPassword(string email, string password, string otp)
        {
            try
            {
                if (otp == null || password == null || otp == null)
                {
                    return false;
                }
                var token = await GetUserByEmail(email);
                string getOTPsave = string.Empty;
                cache.TryGetValue(email, out getOTPsave);
                if (token != null && getOTPsave == otp)
                {
                    var hashedInputPasswordString = HashAndTruncatePassword(password);
                    token.Password = hashedInputPasswordString;
                    await UpdateUserAsync(token);

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
