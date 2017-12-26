using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.Models
{
    public class Entity : BaseEntity
    {
        public Entity() : base()
        {
            Properties = new HashSet<Property>();
            PropertyValues = new HashSet<PropertyValue>();
            Columns = new HashSet<Column>();
            DataTables = new HashSet<DataTable>();
            Forms = new HashSet<Form>();
            CreateDate = DateTime.Now;
            CreatedBy = "username";
            UpdateDate = DateTime.Now;
            UpdatedBy = "username";
        }
        [Required]
        [Display(Name="Varlık Adı")]
        [StringLength(200)]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Varlık Çoğul Adı")]
        [StringLength(200)]
        public string PluralName { get; set; }
        [Display(Name="Özellikler")]
        public virtual ICollection<Property> Properties { get; set; }
        [Display(Name = "Özellikler Değerleri")]
        public virtual ICollection<PropertyValue> PropertyValues { get; set; }
        [Display(Name = "Veri Kaynağı Özellikleri")]
        public virtual ICollection<Property> DataSourceProperties { get; set; }
        
        [Display(Name="Sütunlar")]
        public virtual ICollection<Column> Columns { get; set; }
        [Display(Name = "Veri Tabloları")]
        public virtual ICollection<DataTable> DataTables { get; set; }
        [Display(Name = "Formlar")]
        public virtual ICollection<Form> Forms { get; set; }
        public virtual ICollection<EntityRow> EntityRows { get; set; }
    }
}
