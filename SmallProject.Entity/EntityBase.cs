using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallProject.Entity
{
    public class EntityBase
    {
        public EntityBase()
        {
            this.IsDeleted = false;
        }

        [Key]
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public DateTime CreatedDate { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? ModifiedDate { get; set; }

        [ScaffoldColumn(false)]
        [StringLength(100)]
        public string LastModifiedUserName { get; set; }

        [ScaffoldColumn(false)]
        public bool IsDeleted { get; set; }


    }
}