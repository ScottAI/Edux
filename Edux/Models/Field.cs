using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.Models
{
    public class Field:BaseEntity
    {
        [Required]
        [Display(Name = "Alan Adı")]
        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(200)]
        [Required]
        [Display(Name = "Alan Görünen Adı")]
        public string DisplayName { get; set; }
        [Display(Name = "Form")]
        public string FormId { get; set; }
        [Display(Name = "Form")]
        [ForeignKey("FormId")]
        public Form Form { get; set; }
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
        [Display(Name = "Özellik Değeri")]
        public string PropertyValueId { get; set; }
        [Display(Name = "Özellik Değeri")]
        [ForeignKey("PropertyValueId")]
        public PropertyValue PropertyValue { get; set; }
        [StringLength(200)]
        [Display(Name="Sekme")]
        public string Tab { get; set; }
        
        [Display(Name = "Satır")]
        public int Row { get; set; }
       
        [Display(Name = "Sütun")]
        public int Col { get; set; }
        [Display(Name = "Pozisyon")]
        public int Position { get; set; }
        [Display(Name = "Editör Türü")]
        public EditorType EditorType { get; set; }
        [Display(Name = "Varsayılan Değer")]
        public string DefaultValue { get; set; }
    }
}
