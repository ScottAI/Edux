using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.Models
{
    public class Chart:BaseEntity
    {
        [StringLength(200)]
        [Display(Name = "Grafik Adı")]
        public string Name { get; set; }
        [StringLength(200)]
        [Display(Name = "Grafik Görünen Adı")]
        public string DisplayName { get; set; }
        [StringLength(200)]
        [Display(Name = "X Ekseni Varlık Adı")]
        public string AxisXEntity { get; set; } //x Ekseni varlıkAdı
        [StringLength(200)]
        [Display(Name = "X Ekseni Özellik Adı")]
        public string AxisXProperty { get; set; } //x Ekseni ozellıkadi 
        [StringLength(200)]
        [Display(Name = "X Ekseni Görüntüleme Adı")]
        public string AxisXDisplayFormat { get; set; } //x Ekseni goruntuleme formatı 
        [StringLength(200)]
        [Display(Name = "Y Ekseni Varlık Adı")]
        public string AxisYEntity { get; set; }
        [StringLength(200)]
        [Display(Name = "Y Ekseni Özellik Adı")]
        public string AxisYProperty { get; set; }
        [StringLength(200)]
        [Display(Name = "Y Ekseni Görüntüleme Adı")]
        public string AxisYDisplayFormat { get; set; }
        [StringLength(200)]
        [Display(Name = "X Ekseni Varlık Adı")]
        public string AxisZEntity { get; set; }
        [StringLength(200)]
        [Display(Name = "X Ekseni Özellik Adı")]
        public string AxisZPropert { get; set; }
        [StringLength(200)]
        [Display(Name = "X Ekseni Görüntüleme Adı")]
        public string AxisZDisplayFormat { get; set; }
        [Display(Name = "Grafik Türü")]
        [EnumDataType(typeof(CharType))]
        public CharType CharType { get; set; }
    }
}
