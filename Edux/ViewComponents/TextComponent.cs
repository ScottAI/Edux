using Edux.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.ViewComponents
{
    public class TextComponent:ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public TextComponent(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(Models.Component component)
        {
            var viewName = component.View ?? "Default";
            ViewBag.Content = component.ParameterValues.FirstOrDefault(f => f.Parameter.Name == "Content")?.Value;
           

            return await Task.FromResult(View(viewName, component));
        }
        
    }
}
