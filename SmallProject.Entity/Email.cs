using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallProject.Entity
{
    public class Email:EntityBase
    {
        public Email()
        {
            ToMails=new HashSet<ToMails>();
            CcMails=new HashSet<CcMails>();
            Attachments=new HashSet<Attachments>();
        }
        public string Subject { get; set; }
        public string Body { get; set; }
        public ICollection<ToMails> ToMails { get; set; }
        public ICollection<CcMails> CcMails { get; set; }
        public ICollection<Attachments> Attachments { get; set; }
    }
}
