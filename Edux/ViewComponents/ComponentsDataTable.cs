using Edux.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.ViewComponents
{
    public class ComponentsDataTable:ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public ComponentsDataTable(ApplicationDbContext context)
        {
            this._context = context;
        }
        
        public async Task<IViewComponentResult> InvokeAsync(string pageId)
        {
            var components = _context.Components.Include(c => c.ComponentType).Include(c => c.ParentComponent).Include(c=>c.Page).Where(c => c.PageId == pageId);
            ViewBag.pageId = pageId;
            return View(await components.ToListAsync());
        }
    }
}
