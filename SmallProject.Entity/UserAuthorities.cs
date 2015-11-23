using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallProject.Entity
{
    public class UserAuthorities : EntityBase
    {
        public int UserId { get; set; }
        public int AuthorityId { get; set; }
        public virtual Authority Authority { get; set; }
        public virtual User User { get; set; }

    }
}
