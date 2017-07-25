using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.Models
{
    public class ParameterValue : BaseEntity
    {
        public ParameterValue() : base()
        {
            CreateDate = DateTime.Now;
            CreatedBy = "username";
            UpdateDate = DateTime.Now;
            UpdatedBy = "username";
        }
        [Display(Name="Değer")]
        public string Value { get; set; }
        
        [Display(Name = "Bileşen")]
        public string ComponentId { get; set; }
        [ForeignKey("ComponentId")]
        [Display(Name = "Bileşen")]
        public Component Component { get; set; }
        
        [Display(Name = "Parametre")]
        public string ParameterId { get; set; }
        [ForeignKey("ParameterId")]
        [Display(Name = "Parametre")]
        public Parameter Parameter { get; set; }
    }
}
