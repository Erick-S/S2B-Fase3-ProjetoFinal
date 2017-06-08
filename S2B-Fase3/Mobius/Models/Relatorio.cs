using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mobius.Models
{
    public class Relatorio
    {
        [Key]
        public int ID { get; set; }

        public string Log { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:u}")]
        public DateTime LogDate { get; set; }
    }
}