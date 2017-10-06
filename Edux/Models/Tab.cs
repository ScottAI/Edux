using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.Models
{
    public class Tab:BaseEntity
    {
        public Tab()
        {
            Fields = new HashSet<Field>();
        }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        public bool IsVisible { get; set; }
        public string AllowedRoles { get; set; }
        public virtual ICollection<Field> Fields { get; set; }
    }
}
