using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edux.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [StringLength(200)]
        public string AppTenantId { get; set; }
        public string UserGroupId { get; set; }
        [Display(Name = "Kullanıcı Grubu")]
        [ForeignKey("UserGroupId")]
        public UserGroup UserGroup { get; set; }
    }
}
