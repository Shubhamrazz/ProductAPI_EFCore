using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Database_Layer.UserModel
{
    public class UserPostModel
    {
        
        [Required]
        [RegularExpression("^[A-Z][A-Za-z]{3,}$", ErrorMessage ="Please Enter The 3 character For Name and First Letter Is Capital ")]
        public string Name { get; set; }
        [Required]
        [RegularExpression("^([A-Za-z0-9]{3,20})([.][A-Za-z0-9]{1,10})*([@][A-Za-z]{2,5})+[.][A-Za-z]{2,3}([.][A-Za-z]{2,3})?$", ErrorMessage = "Please Enter Valid Email Eg.abc123@gamil.com")] //Email validation
        public string Email { get; set; }
        [Required]
        [RegularExpression("(?=.*[A-Z])(?=.*[0-9])(?=.*[@#$_])[a-zA-Z0-9@#$_]{8,}", ErrorMessage = "Please Enter For Password At least 1 numeric,1 Capital char,1 Special char")] //Password Validation
        public string Password { get; set; }
    }
}
