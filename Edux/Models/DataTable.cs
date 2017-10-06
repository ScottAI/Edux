using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.Models
{
    public class DataTable:BaseEntity
    {
        public DataTable()
        {
            Top = 5000;
            Columns = new HashSet<Column>();
        }
        [Required]
        [StringLength(200)]
        [Display(Name="Veri Tablosu Adı")]
        public string Name { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "Veri Tablosu Görünen Adı")]
        public string DisplayName { get; set; }
        [Display(Name="Sütunlar")]
        public virtual ICollection<Column> Columns { get; set; }
        public int Top { get; set; }
        [Display(Name = "Varlık")]
        public string EntityId { get; set; }
        [Display(Name = "Varlık")]
        [ForeignKey("EntityId")]
        public Entity Entity { get; set; }
        [Display(Name="Alanlar")]
        public virtual ICollection<Field> Fields { get; set; }
    }
}
