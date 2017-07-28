using Edux.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.ViewComponents
{
    public class DataTableComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public DataTableComponent(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(Models.Component component)
        {
            var viewName = component.View ?? "Default";
            var entityName = component.ParameterValues.FirstOrDefault(f => f.Parameter.Name == "EntityName").Value;
            ViewBag.EntityName = entityName;
            ViewBag.Entity = await _context.Entities.Include(e => e.Properties).Include(e => e.PropertyValues).ThenInclude(pv => pv.Property).SingleOrDefaultAsync(e => e.Name == entityName);
            return await Task.FromResult(View(viewName, component));
        }
    }
}
