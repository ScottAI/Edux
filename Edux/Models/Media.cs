using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.Models
{
    public class Media : BaseEntity
    {
        public Media() : base()
        {
           
            CreateDate = DateTime.Now;
            CreatedBy = "username";
            UpdateDate = DateTime.Now;
            UpdatedBy = "username";
        }

        [Required]
        [StringLength(200)]
        [DisplayName("Ortam Adı")]
        public string Name { get; set; }
        [StringLength(200)]
        [DisplayName("Açıklama")]
        public string Description { get; set; }
        [StringLength(200)]
        [DisplayName("Uzantı")]
        public string Extension { get; set; }
        [StringLength(200)]
        [DisplayName("Dosya Yolu")]
        public string FilePath { get; set; }
        [DisplayName("Dosya Boyutu")]
        public double? FileSize { get; set; }
        [DisplayName("Yıl")]
        public int Year { get; set; }
        [DisplayName("Ay")]
        public int Month { get; set; }
        [StringLength(200)]
        [DisplayName("İçerik Tipi")]
        public string ContentType { get; set; }
    }
}
