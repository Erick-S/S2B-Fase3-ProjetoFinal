using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mobius.Models
{
    //Extends from current user class?
    public class MyProfile /*: ApplicationUser*/
    {
        //FK from ApplicationUser?
        [Display(Name = "E-Mail")]
        public string Email { get; set; }

        [Display(Name = "Avaliação")]
        [Range(0, 10)]
        public int Rating { get; set; }

        //Profile image (Upload or Link)
        [Display(Name = "Upload Image")]
        public byte[] ImageFile { get; set; }
        public string ImageMimeType { get; set; }

        [Display(Name = "Image Link")]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }
    }
}