using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.Models
{
    public class Form:BaseEntity
    {
        [Required]
        [StringLength(200)]
        [Display(Name = "Form Adı")]
        public string Name { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "Form Görünen Adı")]
        public string DisplayName { get; set; }
        public virtual ICollection<Field> Fields { get; set; }
        
    }
}
