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
    public class FieldsetsDataTable : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public FieldsetsDataTable(ApplicationDbContext context)
        {
            this._context = context;
        }


        public async Task<IViewComponentResult> InvokeAsync(string formId)
        {
            var fieldsets = _context.Fieldsets.Include(c => c.Form).Where(c => c.FormId == formId);
            ViewBag.formId = formId;
            return View(await fieldsets.ToListAsync());
        }
    }
}
