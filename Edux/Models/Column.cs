using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.Models
{
    public class Column:BaseEntity
    {
        [Display(Name="Veri Tablosu")]
        public string DataTableId { get; set; }
        [Display(Name = "Veri Tablosu")]
        [ForeignKey("DataTableId")]
        public DataTable DataTable { get; set; }
        [Display(Name = "Özellik")]
        public string PropertyId { get; set; }
        [Display(Name = "Özellik")]
        [ForeignKey("PropertyId")]
        public Property Property { get; set; }
        [Display(Name = "Varlık")]
        public string EntityId { get; set; }
        [Display(Name = "Varlık")]
        [ForeignKey("EntityId")]
        public Entity Entity { get; set; }
        public int Position { get; set; }
        public bool? OrderBy { get; set; }
        [Display(Name="Filtreleme Operatörü")]
        public FilterOperator FilterOperator { get; set; }
        [Display(Name = "Filtreleme Değeri")]
        [StringLength(200)]
        public string FilterValue { get; set; }
        
    }
}
