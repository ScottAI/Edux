using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Display(Name = "Sekme Görünen Adı")]
        [Required]
        [StringLength(200)]
        public string DisplayName { get; set; }
        [Display(Name = "Pozisyon")]
        public int Position { get; set; }
        [Display(Name = "Salt Okunur")]
        public bool IsReadOnly { get; set; }
        [Display(Name = "Görünür mü?")]
        public bool IsVisible { get; set; }

        [Display(Name = "Görünür mü?")]
        public Visibility Visibility { get; set; }

        [Display(Name = "Şu Rollere Görünmez")]
        public string InvisibleToRoles { get; set; }
        [Display(Name = "Şu Rollere Görünür")]
        public string VisibleToRoles { get; set; }
        [Display(Name = "Şu Rollere Salt Okunur")]
        public string ReadOnlyToRoles { get; set; }
        [Display(Name = "Şu Roller Düzenleyebilir")]
        public string EditableToRoles { get; set; }
        [Display(Name = "Alanlar")]
        public virtual ICollection<Field> Fields { get; set; }
        [Display(Name="Form")]
        public string FormId { get; set; }
        [ForeignKey("FormId")]
        [Display(Name = "Form")]
        public Form Form { get; set; }
        
    }
}
