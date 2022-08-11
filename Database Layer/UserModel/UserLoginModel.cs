using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Database_Layer.UserModel
{
   public class UserLoginModel
   {
        public string Email { get; set; }
       public string Password { get; set; }
    }
}
