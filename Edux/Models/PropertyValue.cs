using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.Models
{
    public class PropertyValue : BaseEntity
    {
        public PropertyValue() : base()
        {
            CreateDate = DateTime.Now;
            CreatedBy = "username";
            UpdateDate = DateTime.Now;
            UpdatedBy = "username";
        }
        [Display(Name = "Satır No")]
        public long RowId { get; set; }
        [Display(Name="Değer")]
        public string Value { get; set; }
        [Display(Name = "Varlık")]
        public string EntityId { get; set; }
        [Display(Name = "Varlık")]
        [ForeignKey("EntityId")]
        public Entity Entity { get; set; }
        [Display(Name = "Özellik")]
        public string PropertyId { get; set; }
        [Display(Name = "Özellik")]
        [ForeignKey("PropertyId")]
        public Property Property { get; set; }        
    }
}
