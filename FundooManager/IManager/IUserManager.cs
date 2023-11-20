﻿using FundooModel.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.IManager
{
    public interface IUserManager
    {
        public Task<int> RegisterUser(Register register);
        public string LoginUser(Login login);
        public Register ResetPassword(string email, string newPassword, string confirmPassword);
        public string ForgetPassword(string email);
    }
}
