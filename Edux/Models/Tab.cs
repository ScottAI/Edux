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
        [Display(Name="Sekme Adı")]
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [Display(Name = "Pasif mi?")]
        public bool IsDisabled { get; set; }
        [Display(Name = "Gizli mi?")]
        public bool IsInvisible { get; set; }
        [Display(Name = "Şu Rollere Görünür")]
        public string VisibleToRoles { get; set; }
        [Display(Name = "Alanlar")]
        public virtual ICollection<Field> Fields { get; set; }
    }
}
