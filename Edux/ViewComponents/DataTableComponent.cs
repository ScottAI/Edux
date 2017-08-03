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
            var DataTableName = component.ParameterValues.FirstOrDefault(f => f.Parameter.Name == "DataTableName").Value;
            ViewBag.dataTableName = DataTableName;
            var datatable = await _context.DataTables.Include(e => e.Columns).ThenInclude(e => e.Property).ThenInclude(pv => pv.PropertyValues).SingleOrDefaultAsync(e => e.Name == DataTableName);
            ViewBag.DataTable = datatable;
            var entityName = datatable.EntityName;
            ViewBag.Values = await _context.PropertyValues.Include(i=>i.Entity).Include(i=>i.Property).Where(p => p.Entity.Name == entityName).OrderBy(r => r.RowId).Take(datatable.Top).ToListAsync();
            return await Task.FromResult(View(viewName, component));
        }
    }
}
