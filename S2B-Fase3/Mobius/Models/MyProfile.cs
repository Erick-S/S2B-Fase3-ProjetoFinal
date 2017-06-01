using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mobius.Models
{
    public class MyProfile
    {
        //FK from ApplicationUser
        public string Email { get; set; }

        [Range(0, 10)]
        public int Rating { get; set; }

        //+Imagem do User?

        //public virtual ICollection<Product> Products { get; set; }
    }
}