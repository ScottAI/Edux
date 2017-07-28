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

            var componentTypes = new ComponentType[]
            {
            new ComponentType{ Name="DivComponent", DisplayName="Div Bileşeni", CreateDate=DateTime.Parse("2017-07-26"), UpdateDate = DateTime.Parse("2017-07-26"), CreatedBy="username", UpdatedBy="username", AppTenantId="1"},
            new ComponentType{ Name="LinkComponent", DisplayName="Link Bileşeni", CreateDate=DateTime.Parse("2017-07-26"), UpdateDate = DateTime.Parse("2017-07-26"), CreatedBy="username", UpdatedBy="username", AppTenantId="1"},
            new ComponentType{ Name="DataTableComponent", DisplayName="Veri Tablosu Bileşeni", CreateDate=DateTime.Parse("2017-07-26"), UpdateDate = DateTime.Parse("2017-07-26"), CreatedBy="username", UpdatedBy="username", AppTenantId="1"}
            };
            foreach (ComponentType ct in componentTypes)
            {
                context.ComponentTypes.Add(ct);
            }
            context.SaveChanges();
        }
    }
}
