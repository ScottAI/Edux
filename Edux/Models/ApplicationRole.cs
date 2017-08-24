using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.Models
{
    public class Role:IdentityRole<Guid>
    {
        [StringLength(200)]
        public string AppTenantId { get; set; }
    }
}
