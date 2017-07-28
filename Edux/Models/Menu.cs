using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.Models
{
    public class Menu : BaseEntity
    {
        public Menu()
        {
            MenuItems = new HashSet<MenuItem>();
        }
        [Required]
        [Display(Name = "Menü Adı")]
        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(200)]
        [Display(Name = "Menu Konumu")]
        public string MenuLocation { get; set; }
        [Display(Name = "Menü Öğeleri")]
        public ICollection<MenuItem> MenuItems { get; set; }
    }
}
