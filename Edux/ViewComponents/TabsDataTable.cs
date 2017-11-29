using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Edux.Data;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Edux.ViewComponents
{
    public class TabsDataTable : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public TabsDataTable(ApplicationDbContext context)
        {
            this._context = context;
        }


        public async Task<IViewComponentResult> InvokeAsync(string formId)
        {
            var tabs = _context.Tabs.Include(c => c.Form).Where(c => c.FormId == formId);
            ViewBag.formId= formId;
            return View(await tabs.ToListAsync());
        }
    }
}
