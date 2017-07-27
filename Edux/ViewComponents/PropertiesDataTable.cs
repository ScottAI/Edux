using Edux.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.ViewComponents
{
    public class PropertiesDataTable : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public PropertiesDataTable(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string pageId)
        {
            var properties = _context.Properties.Include(c => c.PropertyValues).Include(c=>c.Entity).Where(c => c.Id == pageId);
            return View(await properties.ToListAsync());
        }
    }
}
