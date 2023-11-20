using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using FundooModel.User;
using FundooManager.IManager;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class UserController : ControllerBase
    {
        public readonly IUserManager userManager;
        public UserController(IUserManager userManager)
        {
            this.userManager = userManager;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> UserRegister(Register register)
        {
            try
            {
                var result = await this.userManager.RegisterUser(register);
                if (result == 1)
                {
                    return this.Ok(new { Status = true, Message = "User Registration Successful", data = register });
                }
                return this.BadRequest(new { Status = false, Message = "User Registration UnSuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult UserLogin(Login login)
        {
            try
            {
                var result = this.userManager.LoginUser(login);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "User Login Successful", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "User Login UnSuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut]
        [Route("ResetPassword")]
        public ActionResult UserResetPassword(string newPassword,string confirmPassword)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var result = this.userManager.ResetPassword(email,newPassword,confirmPassword);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "User password reset Successful", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "User  password reset UnSuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("ForgetPassword")]
        public ActionResult ForgetPassword(string email)
        {
            try
            {
                var resultLog = this.userManager.ForgetPassword(email);
                if (resultLog != null)
                {
                    return Ok(new { success = true, message = "Reset Email Send" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Reset UnSuccessful" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
