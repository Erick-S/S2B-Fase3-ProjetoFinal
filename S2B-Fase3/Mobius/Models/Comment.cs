using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mobius.Models
{
    public class Comment
    {
        [Key]
        public int ID { get; set; }

        public int ProductID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime CommentDate { get; set; }

        [StringLength(300)]
        public string Comentario { get; set; }

        [StringLength(300)]
        public string Resposta { get; set; }

        public string UserEmail { get; set; }
    }
}