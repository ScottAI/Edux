using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Edux.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Http;

namespace Edux.Data
{
        public class ApplicationDbContext : IdentityDbContext<ApplicationUser, Role, string>
        {
            public readonly AppTenant tenant;
            private readonly IHttpContextAccessor _accessor;
            public ApplicationDbContext() { }
            public ApplicationDbContext(AppTenant tenant, IHttpContextAccessor accessor)
                {
                 _accessor = accessor;

                    if (tenant != null)
                        {
                         this.tenant = tenant;
                         var tenantId = this.tenant.AppTenantId;
                         this.Initialize(accessor);
                         ApplicationDbContextInitializer.Initialize(this, accessor);

                //QueryFilterManager.Filter<Page>(q => q.Where(x => x.AppTenantId == tenantId));

                //QueryFilterManager.InitilizeGlobalFilter(this);
            }
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer((tenant != null ? tenant.ConnectionString : "Server=.;Database=TenantDb;Trusted_Connection=True;MultipleActiveResultSets=true"));
                base.OnConfiguring(optionsBuilder);
            }
            public DbSet<Language> Languages { get; set; }
            public DbSet<Entity> Entities { get; set; }
            public DbSet<Property> Properties { get; set; }
            public DbSet<PropertyValue> PropertyValues { get; set; }
            public DbSet<ComponentType> ComponentTypes { get; set; }
            public DbSet<Page> Pages { get; set; }
            public DbSet<ParameterValue> ParameterValues { get; set; }
            public DbSet<Parameter> Parameters { get; set; }
            public DbSet<Component> Components { get; set; }
            public DbSet<DataTable> DataTables { get; set; }
            public DbSet<Column> Columns { get; set; }
            public DbSet<Form> Forms { get; set; }
            public DbSet<Field> Fields { get; set; }
            public DbSet<Menu> Menus { get; set; }
            public DbSet<MenuItem> MenuItems { get; set; }
            public DbSet<Chart> Charts { get; set; }
            public DbSet<Setting> Settings { get; set; }
            public DbSet<App> Apps{ get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<Property>().HasOne(c => c.Entity)
                .WithMany(p => p.Properties)
                .HasForeignKey(c => c.EntityId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Property>().HasOne(c => c.DataSourceEntity)
                .WithMany(p => p.DataSourceProperties)
                .HasForeignKey(c => c.DataSourceEntityId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Component>().HasOne(c => c.Page)
                .WithMany(p=>p.Components)
                .HasForeignKey(c => c.PageId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ParameterValue>().HasOne(pv => pv.Component)
                .WithMany(c => c.ParameterValues)
                .HasForeignKey(pv => pv.ComponentId).OnDelete(DeleteBehavior.Cascade);



        }

        public DbSet<Edux.Models.Media> Media { get; set; }

        public DbSet<Edux.Models.Role> Role { get; set; }
    }
}
