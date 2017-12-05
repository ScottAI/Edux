using Microsoft.AspNetCore.Http;
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
        [Display(Name = "Parametre Türü")]
        public ParameterType ParameterType { get; set; }
        [Display(Name = "Varsayılan Değer")]
        public string DefaultValue { get; set; }
        [Display(Name = "Ön Tanımlı Değerler")]
        public string PresetValues { get; set; }
        [Display(Name = "Seçenek Etiketi")]
        [StringLength(200)]
        public string OptionLabel { get; set; }
        [ForeignKey("DataSourceEntityId")]
        public Entity DataSourceEntity { get; set; }
        [Display(Name = "Veri Kaynağı Özelliği")]
        public string DataSourcePropertyId { get; set; }
        [Display(Name = "Veri Kaynağı Özelliği")]
        [ForeignKey("DataSourcePropertyId")]
        public Property DataSourceProperty { get; set; }
        [Display(Name = "Veri Kaynağı Özelliği 2")]
        public string DataSourcePropertyId2 { get; set; }
        [Display(Name = "Veri Kaynağı Özelliği 2")]
        [ForeignKey("DataSourcePropertyId2")]
        public Property DataSourceProperty2 { get; set; }
        [Display(Name = "Veri Kaynağı Özelliği 3")]
        public string DataSourcePropertyId3 { get; set; }
        [Display(Name = "Veri Kaynağı Özelliği 3")]
        [ForeignKey("DataSourcePropertyId3")]
        public Property DataSourceProperty3 { get; set; }
        [Display(Name = "Parametre Değerleri")]
        public virtual ICollection<ParameterValue> ParameterValues { get; set; }
        

    }
}
