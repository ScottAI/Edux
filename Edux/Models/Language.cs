using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.Models
{
    public class Language:BaseEntity
    {
        public Language()
        {
            IsActive = true;
            Pages = new HashSet<Page>();
        }
        [Required]
        [StringLength(200)]
        [Display(Name = "Ad")]
        public string Name { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "Yerel Ad")]
        public string NativeName { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "Kültür")]
        public string Culture { get; set; }
        [Display(Name = "Aktif mi?")]
        public bool IsActive { get; set; }
        [Display(Name = "Sayfalar")]
        public virtual ICollection<Page> Pages { get; set; }
    }
}
