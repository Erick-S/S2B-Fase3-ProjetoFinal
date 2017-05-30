using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mobius.Models
{
    public class Category
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(140)]
        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}