using Edux.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.ViewComponents
{
    public class FormComponentsDataTable : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public FormComponentsDataTable(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string formId)
        {
            var components = _context.Components.Include(c => c.ComponentType).Include(c => c.ParentComponent).Include(c => c.Page).Where(c => c.FormId == formId).OrderBy(o => o.Position);
            ViewBag.FormId = formId;
            return View(await components.ToListAsync());
        }
    }
}
