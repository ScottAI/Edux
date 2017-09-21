using Edux.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.Data
{
    public static class HostDbContextInitializer
    {
        public static void Initialize(this HostDbContext context)
        {
            // migration'ları veritabanına uygula
            context.Database.Migrate();

            // Look for any tenants record.
            if (context.AppTenants.Any())
            {
                return;   // DB has been seeded
            }
            // Perform seed operations
            var theme = AddTheme(context);
            AddAppTenants(context, theme);
        }
        public static Theme AddTheme(HostDbContext context)
        {
            var defaultTheme = new Theme();
            defaultTheme.Name = "metronic";
            defaultTheme.Logo = "/metronic/layouts/layout/img/logo.png";
            defaultTheme.ImageUrl = "";
            defaultTheme.MetaDescription = "";
            defaultTheme.MetaTitle = "";
            defaultTheme.MetaKeywords = "";
            defaultTheme.CreateDate = DateTime.Now;
            defaultTheme.UpdateDate = DateTime.Now;
            defaultTheme.CreatedBy = "UserName";
            defaultTheme.UpdatedBy = "UserName";
            defaultTheme.CustomCSS = "";
            context.Themes.Add(defaultTheme);
            context.SaveChanges();
            return defaultTheme;
        }
            public static void AddAppTenants(HostDbContext context,Theme theme)
        {
            var appTenant = new AppTenant();
            appTenant.Name = "EduxBilgiKoleji";
            appTenant.Title = "Bilgi Koleji Edux";
            appTenant.Hostname = "localhost:60005";
            appTenant.Folder = "bilgikoleji";
            appTenant.CreateDate = DateTime.Now;
            appTenant.ThemeName = "metronic";
            appTenant.Theme = theme;
            appTenant.ConnectionString = $"Server=.;Database={appTenant.Name};Trusted_Connection=True;MultipleActiveResultSets=true";
            context.AppTenants.Add(appTenant);
            context.SaveChanges();
        }

    }
}

