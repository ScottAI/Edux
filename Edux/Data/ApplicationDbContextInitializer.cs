using Edux.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.Data
{
    public static class ApplicationDbContextInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.Migrate();

            // Look for any students.
            if (context.ComponentTypes.Any())
            {
                return;   // DB has been seeded
            }

            // Div bileşenini ekle
            var ct1 = new ComponentType() { Name = "DivComponent", DisplayName = "Div Bileşeni", CreateDate = DateTime.Parse("2017-07-26"), UpdateDate = DateTime.Parse("2017-07-26"), CreatedBy = "username", UpdatedBy = "username", AppTenantId = "1" };
            context.ComponentTypes.Add(ct1);
            context.SaveChanges();

            // Div bileşeninin parametrelerini ekle
            var p1 = new Parameter() { Name = "CssClass", DisplayName = "CssClass", CreateDate = DateTime.Parse("2017-07-26"), UpdateDate = DateTime.Parse("2017-07-26"), CreatedBy = "username", UpdatedBy = "username", ComponentTypeId = ct1.Id };
            context.Parameters.Add(p1);
            context.SaveChanges();


            // FormComponent bileşenini ekle
            var Fc = new ComponentType() { Name = "FormComponent", DisplayName = "Form Bileşeni", CreateDate = DateTime.Parse("2017-07-26"), UpdateDate = DateTime.Parse("2017-07-26"), CreatedBy = "username", UpdatedBy = "username", AppTenantId = "1" };
            context.ComponentTypes.Add(Fc);
            context.SaveChanges();

            // FormComponent bileşeninin parametrelerini ekle
            var Fcv = new Parameter() { Name = "FormName", DisplayName = "Form Adı", CreateDate = DateTime.Parse("2017-07-26"), UpdateDate = DateTime.Parse("2017-07-26"), CreatedBy = "username", UpdatedBy = "username", ComponentTypeId = Fc.Id };
            context.Parameters.Add(Fcv);
            context.SaveChanges();

            // Link bileşenini ekle
            var ct2 = new ComponentType() { Name = "LinkComponent", DisplayName = "Link Bileşeni", CreateDate = DateTime.Parse("2017-07-26"), UpdateDate = DateTime.Parse("2017-07-26"), CreatedBy = "username", UpdatedBy = "username", AppTenantId = "1" };
            context.ComponentTypes.Add(ct2);
            context.SaveChanges();

            // link bileşeninin parametrelerini ekle
            var p2 = new Parameter() { Name = "CssClass", DisplayName = "CssClass", CreateDate = DateTime.Parse("2017-07-26"), UpdateDate = DateTime.Parse("2017-07-26"), CreatedBy = "username", UpdatedBy = "username", ComponentTypeId = ct2.Id };
            var p3 = new Parameter() { Name = "Href", DisplayName = "Href", CreateDate = DateTime.Parse("2017-07-26"), UpdateDate = DateTime.Parse("2017-07-26"), CreatedBy = "username", UpdatedBy = "username", ComponentTypeId = ct2.Id };
            var p4 = new Parameter() { Name = "Text", DisplayName = "Text", CreateDate = DateTime.Parse("2017-07-26"), UpdateDate = DateTime.Parse("2017-07-26"), CreatedBy = "username", UpdatedBy = "username", ComponentTypeId = ct2.Id };
            context.Parameters.Add(p2);
            context.Parameters.Add(p3);
            context.Parameters.Add(p4);
            context.SaveChanges();

            // Image bileşenini ekle
            var ct3 = new ComponentType() { Name = "ImageComponent", DisplayName = "Resim Bileşeni", CreateDate = DateTime.Parse("2017-07-26"), UpdateDate = DateTime.Parse("2017-07-26"), CreatedBy = "username", UpdatedBy = "username", AppTenantId = "1" };
            context.ComponentTypes.Add(ct3);
            context.SaveChanges();

            // link bileşeninin parametrelerini ekle
            var p5 = new Parameter() { Name = "Src", DisplayName = "Src", CreateDate = DateTime.Parse("2017-07-26"), UpdateDate = DateTime.Parse("2017-07-26"), CreatedBy = "username", UpdatedBy = "username", ComponentTypeId = ct3.Id };
            var p6 = new Parameter() { Name = "Width", DisplayName = "Width", CreateDate = DateTime.Parse("2017-07-26"), UpdateDate = DateTime.Parse("2017-07-26"), CreatedBy = "username", UpdatedBy = "username", ComponentTypeId = ct3.Id };
            var p7 = new Parameter() { Name = "Height", DisplayName = "Height", CreateDate = DateTime.Parse("2017-07-26"), UpdateDate = DateTime.Parse("2017-07-26"), CreatedBy = "username", UpdatedBy = "username", ComponentTypeId = ct3.Id };
            context.Parameters.Add(p5);
            context.Parameters.Add(p6);
            context.Parameters.Add(p7);
            context.SaveChanges();

            // veri tablosu bileşenini ekle
            var ct4 = new ComponentType { Name = "DataTableComponent", DisplayName = "Veri Tablosu Bileşeni", CreateDate = DateTime.Parse("2017-07-26"), UpdateDate = DateTime.Parse("2017-07-26"), CreatedBy = "username", UpdatedBy = "username", AppTenantId = "1" };
            context.ComponentTypes.Add(ct4);
            context.SaveChanges();

            // veri tablosu bileşeninin parametrelerini ekle
            var p8 = new Parameter() { Name = "DataTableName", DisplayName = "DataTableName", CreateDate = DateTime.Parse("2017-07-26"), UpdateDate = DateTime.Parse("2017-07-26"), CreatedBy = "username", UpdatedBy = "username", Position=1, ComponentTypeId = ct4.Id };
            var p11 = new Parameter() { Name = "CreateButtonText", DisplayName = "Oluştur Butonu Metni", CreateDate = DateTime.Parse("2017-07-26"), UpdateDate = DateTime.Parse("2017-07-26"), CreatedBy = "username", UpdatedBy = "username", Position = 2, ComponentTypeId = ct4.Id };
            var p12 = new Parameter() { Name = "CreateButtonHref", DisplayName = "Oluştur Butonu Linki", CreateDate = DateTime.Parse("2017-07-26"), UpdateDate = DateTime.Parse("2017-07-26"), CreatedBy = "username", UpdatedBy = "username", Position = 3, ComponentTypeId = ct4.Id };
            var p13 = new Parameter() { Name = "EditButtonText", DisplayName = "Düzenle Butonu Metni", CreateDate = DateTime.Parse("2017-07-26"), UpdateDate = DateTime.Parse("2017-07-26"), CreatedBy = "username", UpdatedBy = "username", Position = 4, ComponentTypeId = ct4.Id };
            var p14 = new Parameter() { Name = "EditButtonHref", DisplayName = "Düzenle Butonu Linki", CreateDate = DateTime.Parse("2017-07-26"), UpdateDate = DateTime.Parse("2017-07-26"), CreatedBy = "username", UpdatedBy = "username", Position = 5, ComponentTypeId = ct4.Id };
            var p15 = new Parameter() { Name = "DeleteButtonText", DisplayName = "Sil Butonu Metni", CreateDate = DateTime.Parse("2017-07-26"), UpdateDate = DateTime.Parse("2017-07-26"), CreatedBy = "username", UpdatedBy = "username", Position = 6, ComponentTypeId = ct4.Id };
            var p16 = new Parameter() { Name = "DeleteButtonHref", DisplayName = "Sil Butonu Linki", CreateDate = DateTime.Parse("2017-07-26"), UpdateDate = DateTime.Parse("2017-07-26"), CreatedBy = "username", UpdatedBy = "username", Position = 7, ComponentTypeId = ct4.Id };
            context.Parameters.Add(p8);
            context.Parameters.Add(p11);
            context.Parameters.Add(p12);
            context.Parameters.Add(p13);
            context.Parameters.Add(p14);
            context.Parameters.Add(p15);
            context.Parameters.Add(p16);
            context.SaveChanges();
            // Text tablosu bileşenini ekle
            var ct5 = new ComponentType() { Name = "TextComponent", DisplayName = "Yazı Bileşeni", CreateDate = DateTime.Parse("2017-07-26"), UpdateDate = DateTime.Parse("2017-07-26"), CreatedBy = "username", UpdatedBy = "username", AppTenantId = "1" };
            context.ComponentTypes.Add(ct5);
            context.SaveChanges();

            // text bileşeninin parametrelerini ekle
            var p9 = new Parameter() { Name = "Content", DisplayName = "Content", CreateDate = DateTime.Parse("2017-07-26"), UpdateDate = DateTime.Parse("2017-07-26"), CreatedBy = "username", UpdatedBy = "username", ComponentTypeId = ct5.Id };
            context.Parameters.Add(p9);
            context.SaveChanges();

            //Giriş Menüsü Ekle
            var m1 = new Menu() { Name = "MenuComponent",MenuLocation="Primary2",  CreateDate = DateTime.Parse("2017-07-26"), UpdateDate = DateTime.Parse("2017-07-26"), CreatedBy = "username", UpdatedBy = "username", AppTenantId = "1" };
            context.Menus.Add(m1);
            context.SaveChanges();

            // text bileşeninin parametrelerini ekle
            var mI1 = new MenuItem() { Icon = "icon-bar-chart", Name = "Giriş", Url="/tr/Giris" , IsPublished=true, CreateDate = DateTime.Parse("2017-07-26"), UpdateDate = DateTime.Parse("2017-07-26"), CreatedBy = "username", UpdatedBy = "username", MenuId = m1.Id };
            context.MenuItems.Add(mI1);
            context.SaveChanges();

             var p10= new Page() { Title = "Giriş", Slug="giris" , CreateDate = DateTime.Parse("2017-07-26"), UpdateDate = DateTime.Parse("2017-07-26"), CreatedBy = "username", UpdatedBy = "username", AppTenantId = "1" };
            context.Pages.Add(p10);
            context.SaveChanges();


            var c1= new Component() { Name = "Container", DisplayName = "Container", ComponentTypeId=ct1.Id, CreateDate = DateTime.Parse("2017-07-26"), UpdateDate = DateTime.Parse("2017-07-26"), CreatedBy = "username", UpdatedBy = "username",PageId=p10.Id, AppTenantId = "1" };
            context.Components.Add(c1);
            context.SaveChanges();

            var p20 = new Page() { Title = "Yardım", Slug = "yardim", CreateDate = DateTime.Parse("2017-07-26"), UpdateDate = DateTime.Parse("2017-07-26"), CreatedBy = "username", UpdatedBy = "username", AppTenantId = "1" };
            context.Pages.Add(p20);
            context.SaveChanges();


           var c2 = new Component() { Name = "Container", DisplayName = "Container", ComponentTypeId = ct1.Id, CreateDate = DateTime.Parse("2017-07-26"), UpdateDate = DateTime.Parse("2017-07-26"), CreatedBy = "username", UpdatedBy = "username", PageId = p20.Id, AppTenantId = "1" };
            context.Components.Add(c2);
            context.SaveChanges();

            var t1 = new Component() { Name = "TextComponent", DisplayName = "Yazı Bileşeni", ComponentTypeId = ct5.Id, CreateDate = DateTime.Parse("2017-07-26"), UpdateDate = DateTime.Parse("2017-07-26"), CreatedBy = "username", UpdatedBy = "username", ParentComponentId =c2.Id, PageId = p20.Id, AppTenantId = "1"};
            context.Components.Add(t1);
            context.SaveChanges();
            var p22 = new ParameterValue() { ComponentId=t1.Id, ParameterId=p9.Id, Value= "<br/><br/><b>Edux Özelleştirmesi Nasıl Yapılır?</b><br/><br/> 1. Verileri saklamak için Varlık nasıl oluşturulur? <br/>Varlıklar modülünden verileri saklayabileceğimiz varlıklar oluşturup,bu varlıkların özelliklerini oluşturmamız gerekiyor.<br/><br/> 2. Verileri görüntülemek için Veri Tablosu nasıl oluşturulur?<br/> Veri Tabloları modülünden veri tablosu oluşturulup,sütunları tanımlanmalıdır.Sütunlar hangi varlığın hangi özelliğinin değerinin görüntüleneceğini belirlemeye yarar.<br/><br/> 3. Verileri girmek ve düzenlemek için Form nasıl oluşturulur?<br/> Formlar modülünden veri girmek ve düzenlemek için form oluşturup, alanlarını tanımlamamız gerekmektedir.Form alanları ile girilen değerlerin hangi varlığın özelliği için olduğu tanımlanır.<br/><br/> 4. Yönetim paneli sayfaları nasıl hazırlanır?<br/>Sayfalar modülünden veri tabloları ve formlar içeren sayfalar tasarlanabilir.Sayfayı oluşturup sayfada görüntülenecek Veri Tablosu ve Form bileşeni gibi bileşenleri parametre değerlerini girerek o sayfaya eklemek gerekmektedir." };
            context.ParameterValues.Add(p22);
            context.SaveChanges();




        }
    }
}
