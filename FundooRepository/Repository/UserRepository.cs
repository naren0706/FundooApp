using FundooModel.User;
using FundooRepository.Context;
using FundooRepository.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NlogImplementation;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepository.Repository
{
    public class UserRepository : IUserRepository
    {
        public readonly UserDbContext context;
        public readonly IConfiguration configuration;
        NlogOperation nlog = new NlogOperation();
        public UserRepository(UserDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }
        public Task<int> RegisterUser(Register register)
        {
            var password = EncryptPassword(register.Password);
            register.Password = password;
            this.context.Register.Add(register);
            var result = this.context.SaveChangesAsync();
            nlog.LogInfo("Registered successfully");
            return result;
        }
        public string LoginUser(Login login)
        {
            try
            {
                var result = this.context.Register.Where(x => x.Email.Equals(login.Email)).FirstOrDefault();

                if (result != null)
                {
                    var decryptPassword = DecryptPassword(result.Password);

                    if (decryptPassword.Equals(login.Password))
                    {
                        nlog.LogInfo("Login successfully");
                        var token = GenerateSecurityToken(result.Email, result.Id);
                        return token;
                    }
                    else
                    {
                        nlog.LogWarn("Password Does not match!");
                        return null;
                    }
                }
                else
                {
                    nlog.LogWarn("User with the specified email not found");
                    return null;
                }
            }
            catch (Exception ex)
            {
                nlog.LogError( ex.Message);
                return null;
            }
        }

        public Register ResetPassword(string email,string newPassword,string confirmPassword)
        {
            try
            {
                if (newPassword.Equals(confirmPassword))
                {
                    var input = this.context.Register.Where(x => x.Email.Equals(email)).FirstOrDefault();
                    if (input != null)
                    {
                        // Update the password and save changes
                        var password = EncryptPassword(newPassword);
                        input.Password = password; // Assuming you store the encrypted password
                        this.context.Register.Update(input);
                        this.context.SaveChanges();

                        nlog.LogInfo("Password reset successfully");
                        return input;
                    }
                    else
                    {
                        nlog.LogWarn("User with the specified email not found");
                        return null;
                    }
                }
                else
                {
                    nlog.LogWarn("NewPassword and ConfirmPassword do not match");
                    return null;
                }
            }
            catch (Exception ex)
            {
                nlog.LogError("Something went wrong during password reset");
                return null;
            }
        }
        public string GenerateSecurityToken(string email, int userId)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.configuration[("JWT:Key")]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("Id",userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenhandler.CreateToken(tokenDescriptor);
            return tokenhandler.WriteToken(token);
        }
        public string ForgetPassword(string Email)
        {
            try
            {
                var emailcheck = this.context.Register.FirstOrDefault(x => x.Email == Email);
                if (emailcheck != null)
                {
                    var token = GenerateSecurityToken(emailcheck.Email, emailcheck.Id);
                    MSMQ msmq = new MSMQ();
                    msmq.sendData2Queue(token,Email);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Password Encrypt and Decrypt
        public string EncryptPassword(string password)
        {
            string strmsg = string.Empty;
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }
        public string DecryptPassword(string encryptpwd)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new string(decoded_char);
            return decryptpwd;
        }
    }
}
