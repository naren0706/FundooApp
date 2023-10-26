using FundooManager.IManager;
using FundooModel.User;
using FundooRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public class UserManager : IUserManager
    {
        public readonly IUserRepository userRepository;
        public UserManager(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public Task<int> RegisterUser(Register register)
        {
            var result = this.userRepository.RegisterUser(register);
            return result;
        }
        public string LoginUser(Login login)
        {
            var result = this.userRepository.LoginUser(login);
            return result;
        }
        public Register ResetPassword(ResetPassword reset)
        {
            var result = this.userRepository.ResetPassword(reset);
            return result;
        }
        public string ForgetPassword(string Email)
        {
            try
            {
                return this.userRepository.ForgetPassword(Email);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
