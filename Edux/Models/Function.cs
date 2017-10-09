using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.Models
{
    public class Function:BaseEntity
    {
        [Display(Name="İşlev Adı")]
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [Display(Name = "Kod")]
        public string Code { get; set; }
        [Display(Name = "Anonim Mi?")]
        public bool IsAnonymous { get; set; }
        [Display(Name = "İzin Verilen Roller")]
        public string AllowedRoles { get; set; }
    }
}
