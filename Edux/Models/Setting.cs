using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.Models
{
    public class Setting:BaseEntity
    {
        [Display(Name = "Sayfa Görünümü")]
        public string PageViews { get; set; }
        [Display(Name = "Bileşen Görünümü")]
        public string ComponentViews { get; set; }
        [Display(Name = "Site Görünümü")]
        public string LayoutViews { get; set; }
 


        //MAIL
        [StringLength(200)]
        [Display(Name = "Smtp Kullanıcı Adı")]
        public string SmtpUserName { get; set; }
        [StringLength(200)]
        [Display(Name = "Smtp Şifresi")]
        public string SmtpPassword { get; set; }
        [StringLength(200)]
        public string SmtpHost { get; set; }
        [StringLength(200)]
        public string SmtpPort { get; set; }
        public bool SmtpUseSSL { get; set; }


    }
}
