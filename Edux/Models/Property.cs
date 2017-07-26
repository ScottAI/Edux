using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.Models
{
    public class Property : BaseEntity
    {
        public Property() : base()
        {
            PropertyValues = new HashSet<PropertyValue>();
            CreateDate = DateTime.Now;
            CreatedBy = "username";
            UpdateDate = DateTime.Now;
            UpdatedBy = "username";
        }
        [Required]
        [Display(Name="Özellik Adı")]
        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(200)]
        [Required]
        [Display(Name = "Özellik Görünen Adı")]
        public string DisplayName { get; set; }
        
        [Display(Name = "Veri Türü")]
        public DataType? DataType { get; set; }
        [Display(Name = "Gerekli Mi?")]
        public bool IsRequired { get; set; }
        [Display(Name = "Özellik Türü")]
        [EnumDataType(typeof(PropertyType))]
        public PropertyType PropertyType { get; set; }
        //[Display(Name = "Özellik Türü")]
        //public PropertyType PropertyType { get; set; }
        [Display(Name = "Metin Uzunluğu")]
        public int StringLength { get; set; }
        [Display(Name = "Varlık")]
        public string EntityId { get; set; }
        [ForeignKey("EntityId")]
        [Display(Name = "Varlık")]
        public Entity Entity { get; set; }
        [Display(Name = "Özellik Değerleri")]
        public virtual ICollection<PropertyValue> PropertyValues { get; set; }
        [Display(Name = "Pozisyon")]
        public int Position { get; set; }
    }
}
