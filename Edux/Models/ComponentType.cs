using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.Models
{
    public class ComponentType : BaseEntity
    {
        public ComponentType() : base()
        {
            Parameters = new HashSet<Parameter>();
            UpdateDate = DateTime.Now;
            UpdatedBy = "username";
        }
        [Required]
        [StringLength(200)]
        [Display(Name="Bileşen Türü Adı")]
        public string Name { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name="Bileşen Türü Görünen Adı")]
        public string DisplayName { get; set; }
        [Display(Name="Parametreler")]
        public virtual ICollection<Parameter> Parameters { get; set; }
    }
}
