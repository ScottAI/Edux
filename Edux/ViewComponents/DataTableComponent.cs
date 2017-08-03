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
            ViewBag.DataTable = await _context.DataTables.Include(e => e.Columns).ThenInclude(e => e.Property).ThenInclude(pv => pv.PropertyValues).SingleOrDefaultAsync(e => e.Name == DataTableName);
            return await Task.FromResult(View(viewName, component));
        }
    }
}
