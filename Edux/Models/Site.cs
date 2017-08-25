using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.Models
{
    public class Site:BaseEntity
    {
        public Site() : base()
        {

            CreateDate = DateTime.Now;
            CreatedBy = "username";
            UpdateDate = DateTime.Now;
            UpdatedBy = "username";
        }

        [Required]
        [Display(Name = "Site Adı")]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Bağlantı")]
        [StringLength(200)]
        public string Slug { get; set; }

        [Display(Name = "Varsayılan Tasarım")]
        [StringLength(200)]
        public string DefaultLayout { get; set; }

        [Display(Name = "İzin Verilen Roller")]
        public string AllowedRoles { get; set; }

        [Display(Name = "Varsayılan Sayfa")]
        [StringLength(200)]
        public string DefaultPage { get; set; }
    }
}
