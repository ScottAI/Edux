using Edux.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.ViewComponents
{
    public class ParametersDataTable:ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public ParametersDataTable(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string componenttypeId)
        {
            var parameters = _context.Parameters.Include(c => c.ComponentType).Include(c => c.ParameterValues).Where(c => c.ComponentTypeId == componenttypeId).OrderBy(o => o.Position);
            ViewBag.ComponenttypeId = componenttypeId;
            return View(await parameters.ToListAsync());
        }
    }
}
