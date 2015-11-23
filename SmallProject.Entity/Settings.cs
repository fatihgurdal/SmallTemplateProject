using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallProject.Entity
{
    public class Settings:EntityBase
    {
        [StringLength(100)]
        public string Text { get; set; }
        [StringLength(100)]
        public string Value { get; set; }
        [StringLength(200)]
        public string Description { get; set; }  
    }
}
