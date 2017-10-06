using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.Models
{
    public class Function:BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsAuthorized { get; set; }
        public string AllowedRoles { get; set; }
    }
}
