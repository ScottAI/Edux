using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Edux.Data;
using Microsoft.AspNetCore.Mvc;
using Edux.Models.PageViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Edux.Models;

namespace Edux.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string slug)
        {
            if (slug == null)
            {
                return Redirect("/tr/Giris");
            }
            else
            {
                var model = new DisplayViewModel();

                // Getting the page with the slug that user entered
                var page = await _context.Pages
                    .Include(p => p.ParentPage).Include("Components.ComponentType").Include("Components.ChildComponents")
                    .Include(p => p.Components)
                            .ThenInclude(x => x.ParameterValues)
                                .ThenInclude(x => x.Parameter)
                    .FirstOrDefaultAsync(m => m.Slug.Equals(slug.ToLower()) && m.IsPublished == true);
                

                if (page == null)
                {
                    return Content($"'{slug}' adresli bulunamadı!");
                }
                else
                {
                    // Incrementing the ViewCount
                    page.ViewCount++;
                    _context.SaveChanges();
                    model.Page = page;
                    //ViewData["ComponentTypeId"] = new SelectList(_context.ComponentTypes, "Id", "DisplayName");
                    //ViewData["ParentComponentId"] = new SelectList(_context.Components, "Id", "DisplayName");
                    return View(page.View, model);
                }
            }
        }
        [HttpPost]
        public IActionResult SaveForm(IFormCollection form)
        {
            if (ModelState.IsValid) {
                long rowId = 0;
                if (_context.PropertyValues.Any())
                {
                    rowId = _context.PropertyValues.Max(m => m.RowId) + 1;
                }
                foreach (var key in form.Keys)
            {
                if (_context.Fields.Any(f => f.FormId == form["FormId"].ToString() && f.PropertyId == key)) { 
                    var value = new PropertyValue();

                    value.Value = form[key];
                    value.EntityId = form[key + ".EntityId"];
                    value.PropertyId = key;
                        value.RowId = rowId;
                    value.CreateDate = DateTime.Now;
                    value.CreatedBy = User.Identity.Name;
                    value.UpdateDate = DateTime.Now;
                    value.UpdatedBy = User.Identity.Name;
                    value.AppTenantId = "1";
                    _context.Add(value);
                    _context.SaveChanges();
                    }

                }
                return Redirect(Request.Headers["Referer"].ToString() + "?status=ok");
            }
            return Redirect(Request.Headers["Referer"].ToString() + "?status=validationerror");
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
