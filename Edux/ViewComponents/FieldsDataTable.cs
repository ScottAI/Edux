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
    public class FieldsDataTable : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public FieldsDataTable(ApplicationDbContext context)
        {
            this._context = context;
        }


        public async Task<IViewComponentResult> InvokeAsync(string formId)
        {
            var fields = _context.Fields.Include(c => c.Form).Include(c=>c.Entity).Include(c=>c.Property).Where(c => c.FormId == formId);
            ViewBag.formId= formId;
            return View(await fields.ToListAsync());
        }
    }
}
