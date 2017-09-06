using Edux.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.ViewComponents
{
    public class ColumnsDataTable:ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public ColumnsDataTable(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string dataTableId)
        {
            var columns = _context.Columns.Include(c => c.DataTable).Include(c => c.Property).Where(c => c.DataTableId == dataTableId).OrderBy(o => o.Position);
            ViewBag.DataTableId = dataTableId;
            return View(await columns.ToListAsync());
        }
    }
}