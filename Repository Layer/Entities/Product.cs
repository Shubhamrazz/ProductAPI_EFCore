using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository_Layer.Services.Entities
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }

        [Required]
        [ForeignKey("User")]
        public virtual int UserId { get; set; }

        public virtual User user { get; set; }
    }
}
