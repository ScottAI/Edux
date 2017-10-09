using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.Models
{
    public class RoleGroup:BaseEntity
    {
        public RoleGroup()
        {
            Roles = new HashSet<Role>();
        }
        [Required]
        [Display(Name = "Grup Adı")]
        public string Name { get; set; }
        [Display(Name = "Roller")]
        public ICollection<Role> Roles { get; set; }
    }
}
