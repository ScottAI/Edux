using Edux.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.ViewComponents
{
    public class ImageComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public ImageComponent(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(Models.Component component)
        {
            var viewName = component.View ?? "Default";
            ViewBag.Src = component.ParameterValues.FirstOrDefault(f => f.Parameter.Name == "Src")?.Value;
            ViewBag.Width= component.ParameterValues.FirstOrDefault(f => f.Parameter.Name == "Width")?.Value;
            ViewBag.Height = component.ParameterValues.FirstOrDefault(f => f.Parameter.Name == "Height")?.Value;

            return await Task.FromResult(View(viewName, component));
        }
    }
}
