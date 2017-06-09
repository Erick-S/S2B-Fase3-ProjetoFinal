using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mobius.Models
{
    //Extends from current user class?
    public class MyProfile : ApplicationUser
    {
        //FK from ApplicationUser?
        [Display(Name = "E-Mail")]
        public override string Email { get; set; }

        [Display(Name = "Avaliação")]
        [Range(-100, 100)]
        public override int Rating { get; set; }
    }
}