using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.Models
{
    public class Role : IdentityRole
    {
        [StringLength(200)]
        public string AppTenantId { get; set; }
        [Display(Name = "Açıklama")]
        public string Description { get; set; }
    } 
}
