using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallProject.Entity
{
    public class Authority : EntityBase
    {
        [StringLength(40)]
        public string Name { get; set; }
        public bool IsMenu { get; set; }
        public int? SubAuthorityId { get; set; }
        [StringLength(20)]
        public string ActionName { get; set; }
        [StringLength(20)]
        public string ControllerName { get; set; }
        [StringLength(20)]
        public string FaIconCode { get; set; }
        public bool IsLink { get; set; }
        public virtual ICollection<Authority> SubAuthoritys { get; set; }
        public virtual Authority SubAuthority { get; set; }
        public virtual ICollection<UserAuthorities> UserAuthorities { get; set; }
    }
}
