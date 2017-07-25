using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Edux.Models;

namespace Edux.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Entity> Entities { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyValue> PropertyValues { get; set; }
        public DbSet<ComponentType> ComponentTypes { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<ParameterValue> ParameterValues { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<PageComponent> PageComponents { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<PageComponent>().HasKey(pc => new { pc.PageId, pc.ComponentId });
            builder.Entity<PageComponent>().HasOne(bc => bc.Page)
                .WithMany(b => b.PageComponents)
                .HasForeignKey(bc => bc.PageId).OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Cascade);
            builder.Entity<PageComponent>().HasOne(bc => bc.Component)
                .WithMany(c => c.PageComponents)
                .HasForeignKey(bc => bc.ComponentId).OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Cascade);
        }

        public DbSet<Edux.Models.Media> Media { get; set; }
    }
}
