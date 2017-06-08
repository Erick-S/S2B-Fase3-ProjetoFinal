using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mobius.Models
{
    public class Product
    {
        [Display(Name = "ID de Produto")]
        [Key]
        public int ProductID { get; set; }

        [Display(Name = "Anúncio")]
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Display(Name = "Descrição")]
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(140)]
        public string Description { get; set; }

        [Display(Name = "Preço")]
        [Required]
        [DataType(DataType.Currency)]
        [Range(typeof(decimal), "0", "500")]
        public decimal Cost { get; set; }

        [Display(Name = "Endereço")]
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

        [Display(Name = "Status do Anúncio")]
        [DataType(DataType.Text)]
        public Status Status { get; set; }

        [Display(Name = "Avaliação")]
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

        [Display(Name = "Email do Publicador")]
        public string UserEmail { get; set; }

        //Sets an buyer...
        [Display(Name = "Email do Comprador")]
        public string BuyerEmail { get; set; }
    }

    public enum Status
    {
        Open,
        Expired,
        Negotiating,
        Donated,
        Cancelled,
        Sold
    }
}