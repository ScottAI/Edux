using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.Models
{
    public class Component : BaseEntity
    {
        public Component() : base()
        {
            ParameterValues = new HashSet<ParameterValue>();
            ChildComponents = new HashSet<Component>();
            View = "Default";
            UpdateDate = DateTime.Now;
            Position = 0;
        }
        [Required]
        [Display(Name="Bileşen Adı")]
        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(200)]
        [Required]
        [Display(Name = "Bileşen Görünen Adı")]
        public string DisplayName { get; set; }
        [Display(Name = "Bileşen Türü")]
        public string ComponentTypeId { get; set; }
        [ForeignKey("ComponentTypeId")]
        [Display(Name = "Bileşen Türü")]
        public ComponentType ComponentType { get; set; }
        [Display(Name = "Bileşen Parametre Değerleri")]
        public virtual ICollection<ParameterValue> ParameterValues { get; set; }
        [StringLength(200)]
        [Display(Name="Şablon")]
        public string View { get; set; }
        [Display(Name="Üst Bileşen")]
        public string ParentComponentId { get; set; }
        [Display(Name = "Üst Bileşen")]
        [ForeignKey("ParentComponentId")]
        public Component ParentComponent { get; set; }
        [Display(Name = "Alt Bileşenler")]
        public virtual ICollection<Component> ChildComponents { get; set; }
        
   
        [Display(Name="Sayfa")]
        public string PageId { get; set; }
        [Display(Name="Sayfa")]
        [ForeignKey("PageId")]
        public Page Page { get; set; }

        [Display(Name = "Form")]
        public string FormId { get; set; }
        [Display(Name = "Form")]
        [ForeignKey("FormId")]
        public Form Form { get; set; }

        [Display(Name = "Pozisyon")]
        public int Position { get; set; }
    }
}
