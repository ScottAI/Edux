using Edux.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.ViewComponents
{
    public class LinkComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public LinkComponent(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(Models.Component component)
        {
            var viewName = component.View ?? "Default";
            ViewBag.CssClass = component.ParameterValues.FirstOrDefault(f => f.Parameter.Name == "CssClass").Value;
            ViewBag.Href = component.ParameterValues.FirstOrDefault(f => f.Parameter.Name == "Href").Value;
            ViewBag.Text = component.ParameterValues.FirstOrDefault(f => f.Parameter.Name == "Text").Value;
            return await Task.FromResult(View(viewName, component));
        }
    }
}
