using Edux.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.ViewComponents
{
    public class DivComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public DivComponent(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(Models.Component component)
        {
            var viewName = component.View ?? "Default";
            ViewBag.CssClass = component.ParameterValues.FirstOrDefault(f => f.Parameter.Name == "CssClass").Value;
            return await Task.FromResult(View(viewName, component));
        }
    }
}
