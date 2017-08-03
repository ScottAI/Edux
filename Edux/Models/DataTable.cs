using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.Models
{
    public class DataTable:BaseEntity
    {
        public DataTable()
        {
            Top = 5000;
        }
        [Required]
        [StringLength(200)]
        [Display(Name="Veri Tablosu Adı")]
        public string Name { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "Veri Tablosu Görünen Adı")]
        public string DisplayName { get; set; }
        public virtual ICollection<Column> Columns { get; set; }
        public int Top { get; set; }
        [StringLength(200)]
        [Display(Name="Varlık Adı")]
        public string EntityName { get; set; }
    }
}
