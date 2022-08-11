using Bussiness_Layer.Interface;
using Database_Layer.UserModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI_EFCore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ProductContext productContext;
        private readonly IUserBL userBL;

        public UserController(ProductContext productContext, IUserBL userBL)
        {
            this.productContext = productContext;
            this.userBL = userBL;
        }

        [HttpPost("Register")]
        public IActionResult Register(UserPostModel userModel)
        {
            try
            {
                this.userBL.Register(userModel);
                return this.Ok(new { success = true, message = "UserCreated Successfully" });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("LoginUser")]
        public IActionResult LoginUser(UserLoginModel userLoginModel)
        {
            try
            {
   
                string token = this.userBL.Login(userLoginModel);
                if (token == null)
                {
                    return this.BadRequest(new { success = false, message = "Enter Valid Email and Password" });
                }

                return this.Ok(new { success = true, message = "User Login Successfully", data = token });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
