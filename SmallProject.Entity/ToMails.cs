using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallProject.Entity
{
    public class ToMails
    {
        [Key]
        public int Id { get; set; }

        public int EmailId { get; set; }
        public Email EMail { get; set; }
        [StringLength(100)]
        public string Tomail { get; set; }
    }
}
