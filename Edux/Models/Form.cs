using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [StringLength(200)]
        [Display(Name = "Varlık Adı")]
        public string EntityName { get; set; }
        public string EntityId { get; set; }
        [ForeignKey("EntityId")]
        public Entity Entity { get; set; }
        [Display(Name = "Alanlar")]
        public virtual ICollection<Field> Fields { get; set; }
        [Display(Name = "Betikler")]
        public string Scripts { get; set; }   
        [Display(Name = "Sekmeler")]
        public ICollection<Tab> Tabs { get; set; }
    }
}
