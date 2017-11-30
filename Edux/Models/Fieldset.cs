using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.Models
{
    public class Fieldset:BaseEntity
    {
        public Fieldset()
        {
            // bu metot Fieldset class'ın her yeni instance üretildiğinde otomatik çalışır

        }

        [Display(Name="Görünen Adı")]
        [StringLength(200)]
        public string DisplayName { get; set; }

        [Display(Name="Css Sınıfı")]
        [StringLength(200)]
        public string CssClass { get; set; }

        [Display(Name="Pozisyon")]
        public int Position { get; set; }

        [Display(Name="Form")]
        public string FormId { get; set; }
        [Display(Name="Form")]
        [ForeignKey("FormId")]
        public Form Form { get; set; }

        [Display(Name = "Alanlar")]
        public ICollection<Field> Fields { get; set; }
    }
}
