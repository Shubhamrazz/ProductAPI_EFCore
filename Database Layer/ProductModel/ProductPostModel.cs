using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Database_Layer.ProductModel
{
    public class ProductPostModel
    {
        [Required]
        [RegularExpression("^[A-Z][A-Za-z]{3,}$", ErrorMessage = "Please Enter The 3 character For ProductName and First Letter Is Capital ")]
        public string Name { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }

    }
}
