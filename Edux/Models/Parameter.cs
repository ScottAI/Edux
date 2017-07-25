using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.Models
{
    public class Parameter : BaseEntity
    {
        public Parameter() : base()
        {
            UpdateDate = DateTime.Now;
        }
        [Required]
        [Display(Name = "Parametre Adı")]
        [StringLength(200)]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Parametre Görünen Adı")]
        [StringLength(200)]
        public string DisplayName { get; set; }
        [Display(Name = "Gerekli Mi?")]
        public bool IsRequired { get; set; }
        [Display(Name = "Bileşen Türü")]
        public string ComponentTypeId { get; set; }
        [Display(Name = "Bileşen Türü")]
        [ForeignKey("ComponentTypeId")]
        public ComponentType ComponentType { get; set; }
        [Display(Name = "Pozisyon")]
        public int Position { get; set; }
        [Display(Name = "Parametre Değerleri")]
        public virtual ICollection<ParameterValue> ParameterValues { get; set; }

    }
}
