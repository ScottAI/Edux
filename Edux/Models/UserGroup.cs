using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.Models
{
    public class UserGroup:BaseEntity
    {
        public UserGroup()
        {
            Users = new HashSet<ApplicationUser>();
            Roles = new HashSet<Role>();
        }
        [Required]
        [Display(Name = "Grup Adı")]
        public string Name { get; set; }
        [Display(Name = "Kullanıcılar")]
        public ICollection<ApplicationUser> Users { get; set; }
        [Display(Name = "Roller")]
        public ICollection<UserGroupRole> UserGroupRoles { get; set; }
    }
}
