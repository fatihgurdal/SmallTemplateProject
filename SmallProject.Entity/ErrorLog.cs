using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallProject.Entity
{
    public class ErrorLog
    {
        [Key]
        public int Id { get; set; }
        [StringLength(20)]
        public string ContollerName { get; set; }
        [StringLength(20)]
        public string ActionName { get; set; }
        [StringLength(100)]
        public string ErrorDetial { get; set; }
        public DateTime EnterDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
