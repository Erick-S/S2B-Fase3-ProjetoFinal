using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mobius.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(140)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(typeof(decimal), "0", "500")]
        public decimal Cost { get; set; }

        [Required]
        public string Address { get; set; }

        [Display(Name = "Publish Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime PublishDate { get; set; }

        [Display(Name = "Expiration Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime ExpirationDate { get; set; }

        [DataType(DataType.Text)]
        public Status Status { get; set; }

        [Range(0,5)]
        public int Rating { get; set; }

        [Display(Name = "Upload Image")]
        public byte[] ImageFile { get; set; }
        public string ImageMimeType { get; set; }

        [Display(Name = "Image Link")]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }

        public int UserID { get; set; }
        //public virtual ApplicationUser User { get; set; }
    }

    public enum Status
    {
        Open,
        Negotiating,
        Donated,
        Cancelled,
        Sold
    }
}