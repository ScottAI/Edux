using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.Models
{
    public enum Visibility
    {
        [Display(Name="Tüm Rollere Görünür")]
        VisibleToAll=1,
        [Display(Name = "Tüm Rollere Görünmez")]
        InvisibleToAll = 2,
        [Display(Name = "Rollere Göre Kontrol")]
        ControlledByRoles = 3,

    }
}
