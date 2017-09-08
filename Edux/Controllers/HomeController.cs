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
                long rowId = 1;
                string mode = form["Mode"].ToString().ToLowerInvariant();
                if (String.IsNullOrEmpty(mode))
                {
                    mode = "create";
                }

                if (mode == "edit" || mode == "delete")
                {
                    rowId = Convert.ToInt64(form["RowId"].ToString());
                }
                else
                {
                    if (_context.PropertyValues.Any())
                    {
                        rowId = _context.PropertyValues.Max(m => m.RowId) + 1;
                    }
                }
                
                
                foreach (var key in form.Keys)
                {
                    if (_context.Fields.Any(f => f.FormId == form["FormId"].ToString() && f.PropertyId == key)) {

                        PropertyValue value;
                        if (mode == "create")
                        {
                            value = new PropertyValue();
                        } else
                        {
                            value = _context.PropertyValues.Where(pv => pv.PropertyId == key && pv.RowId == rowId).FirstOrDefault();
                        }
                        if (mode == "create" || mode == "edit") { 
                            value.Value = form[key];
                            value.EntityId = form[key + ".EntityId"];
                            if (mode == "create")
                            {
                                value.PropertyId = key;
                                value.RowId = rowId;
                                value.CreateDate = DateTime.Now;
                                value.CreatedBy = User.Identity.Name;
                            }
                            value.UpdateDate = DateTime.Now;
                            value.UpdatedBy = User.Identity.Name;
                            value.AppTenantId = "1";
                        }
                        if (mode == "create")
                        {
                            _context.Add(value);
                        } else if (mode == "edit")
                        {
                            _context.Update(value);
                        } else if (mode == "delete")
                        {
                            _context.Remove(value);
                        }
                        _context.SaveChanges();
                    }

                }
                return Redirect(form["ReturnUrl"].ToString() + "?status=ok");
            }
            return Redirect(form["ReturnUrl"].ToString() + "?status=validationerror");
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
