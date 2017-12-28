using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.Models
{
    public class Role : IdentityRole
    {
        public Role()
        {
            UserGroupRoles = new HashSet<UserGroupRole>();
        }
        [StringLength(200)]
        public string AppTenantId { get; set; }
        [Display(Name = "Açıklama")]
        public string Description { get; set; }
        [Display(Name = "Oluştur")]
        public bool AllowCreate { get; set; }
        [Display(Name = "Oku")]
        public bool AllowRead { get; set; }
        [Display(Name = "Güncelle")]
        public bool AllowUpdate { get; set; }
        [Display(Name = "Sil")]
        public bool AllowDelete { get; set; }
        [Display(Name = "Özel")]
        public bool AllowSpecial { get; set; }
        [Display(Name = "Tümü")]
        public bool AllowAll { get; set; }
        [Display(Name = "Rol Grubu")]
        public string RoleGroupId { get; set; }
        [Display(Name = "Rol Grubu")]
        [ForeignKey("RoleGroupId")]
        public RoleGroup RoleGroup { get; set; }
        public ICollection<UserGroupRole> UserGroupRoles { get; set; }
    } 
}
