using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.Models
{
    public class Page : BaseEntity
    {
        public Page() : base()
        {
            IsPublished = true;
            ViewCount = 0;
            ChildPages = new HashSet<Page>();
            CreateDate = DateTime.Now;
            CreatedBy = "username";
            UpdateDate = DateTime.Now;
            UpdatedBy = "username";
            View = "Page";
        }
        [Required]
        [Display(Name = "Başlık")]
        [StringLength(200)]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Bağlantı")]
        [StringLength(200)]
        public string Slug { get; set; }
        [StringLength(200)]

        [Display(Name = "Şablon")]
        public string View { get; set; }
        [StringLength(200)]

        [Display(Name = "Tasarım Şablonu")]
        public string LayoutView { get; set; }

        
        [Display(Name = "Üst Sayfa")]
        public string ParentPageId { get; set; }
        [ForeignKey("ParentPageId")]
        [Display(Name = "Üst Sayfa")]
        public Page ParentPage { get; set; }
        [Display(Name = "Alt Sayfalar")]
        public virtual ICollection<Page> ChildPages { get; set; }
        [Display(Name = "Meta Başlık")]
        public string MetaTitle { get; set; }
        [Display(Name = "Meta Açıklama")]
        public string MetaDescription { get; set; }
        [Display(Name = "Meta Anahtar Kelimeler")]
        public string MetaKeywords { get; set; }
        [Display(Name ="Yayında Mı?")]
        public bool IsPublished { get; set; }
        [Display(Name = "Görüntülenme Sayısı")]
        public long ViewCount { get; set; }
        [Display(Name = "Pozisyon")]
        public long Position { get; set; }
        [Display(Name = "İzin Verilen Roller")]
        public string AllowedRoles { get; set; }
        [Display(Name = "Bileşenler")]
        public virtual ICollection<Component> Components { get; set; }
        [Display(Name="Uygulama")]
        public string AppId { get; set; }
        [ForeignKey("AppId")]
        [Display(Name="Uygulama")]
        public App App { get; set; }
        [Display(Name = "Dil")]
        public string LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        [Display(Name = "Dil")]
        public Language Language { get; set; }
        public string Scripts { get; set; }
    }
}
