using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Edux.Data;
using Edux.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;

namespace Edux.Controllers
{
    public class ComponentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComponentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Components
        public async Task<IActionResult> Index()
        {
            var components = _context.Components.Include(c => c.ComponentType).ThenInclude(ct => ct.Parameters).Include(c => c.ParentComponent).Include(c => c.ParameterValues);
            return View(await components.ToListAsync());
        }

        // GET: Components/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var component = await _context.Components
                .Include(c => c.ComponentType)
                .Include(c => c.ParentComponent)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (component == null)
            {
                return NotFound();
            }

            return View(component);
        }

        // GET: Components/Create
        public IActionResult Create(string PageId)
        {
            ViewData["ComponentTypeId"] = new SelectList(_context.ComponentTypes, "Id", "Name");
            ViewData["ParentComponentId"] = new SelectList(_context.Components.Where(c => c.PageId == PageId), "Id", "Name");
            ViewData["Pages"] = new SelectList(_context.Pages, "Id", "Title");
            var component = new Component();
            component.PageId = PageId;
            ViewBag.PageId = PageId;
            return View(component);
        }

        // POST: Components/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,DisplayName,ComponentTypeId,View,ParentComponentId,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId,PageId,Position")] Component component, string PageIdRef, IFormCollection form)
        {
            if (ModelState.IsValid)
            {
                
                _context.Add(component);
                await _context.SaveChangesAsync();
                foreach (var key in form.Keys)
                {
                    Guid result;
                    if (Guid.TryParse(key, out result)) {
                        var value = new ParameterValue();
                        value.ParameterId = key;
                        value.ComponentId = component.Id;
                        value.Value = form[key].ToString();
                        value.CreateDate = DateTime.Now;
                        value.UpdateDate = DateTime.Now;
                        value.UpdatedBy = User.Identity.Name;
                        value.CreatedBy = User.Identity.Name;
                        
                        _context.ParameterValues.Add(value);
                    }

                }
                await _context.SaveChangesAsync();
                if (PageIdRef != null)
                {
                    string url = "/Pages/Edit/" + PageIdRef + "#tab_1_2";
                    return Redirect(url);
                }
                
                return RedirectToAction("Index");
            }
            ViewData["ComponentTypeId"] = new SelectList(_context.ComponentTypes, "Id", "Name", component.ComponentTypeId);
            ViewData["ParentComponentId"] = new SelectList(_context.Components.Where(c => c.PageId == component.PageId), "Id", "Name", component.ParentComponentId);
            ViewData["Pages"] = new SelectList(_context.Pages, "Id", "Title");
            
            return View(component);
        }

        // GET: Components/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var component = await _context.Components.SingleOrDefaultAsync(m => m.Id == id);
            if (component == null)
            {
                return NotFound();
            }
            ViewData["ComponentTypeId"] = new SelectList(_context.ComponentTypes, "Id", "Name", component.ComponentTypeId);
            ViewData["ParentComponentId"] = new SelectList(_context.Components.Where(c=>c.PageId==component.PageId),  "Id", "Name", component.ParentComponentId);
            ViewData["Pages"] = new SelectList(_context.Pages, "Id", "Title");
           
            return View(component);
        }

        // POST: Components/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,DisplayName,ComponentTypeId,View,ParentComponentId,PageId,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Component component, IFormCollection form )
        {
            if (id != component.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    _context.Update(component);
                    await _context.SaveChangesAsync();
                    _context.ParameterValues.RemoveRange(_context.ParameterValues.Where(v => v.ComponentId == component.Id).ToArray());
                    await _context.SaveChangesAsync();
                    foreach (var key in form.Keys)
                    {
                        Guid result;
                        if (Guid.TryParse(key, out result))
                        {
                            var value = new ParameterValue();
                            value.ParameterId = key;
                            value.ComponentId = component.Id;
                            value.Value = form[key].ToString();
                            value.CreateDate = DateTime.Now;
                            value.UpdateDate = DateTime.Now;
                            value.UpdatedBy = User.Identity.Name;
                            value.CreatedBy = User.Identity.Name;

                            _context.ParameterValues.Add(value);
                        }

                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComponentExists(component.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["ComponentTypeId"] = new SelectList(_context.ComponentTypes, "Id", "Name", component.ComponentTypeId);
            ViewData["ParentComponentId"] = new SelectList(_context.Components.Where(c => c.PageId == component.PageId), "Id", "Name", component.ParentComponentId);
            ViewData["Pages"] = new SelectList(_context.Pages, "Id", "Title");
           

            return View(component);
        }

        // GET: Components/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var component = await _context.Components
                .Include(c => c.ComponentType)
                .Include(c => c.ParentComponent)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (component == null)
            {
                return NotFound();
            }

            return View(component);
        }

        // POST: Components/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var component = await _context.Components.SingleOrDefaultAsync(m => m.Id == id);
            _context.Components.Remove(component);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ComponentExists(string id)
        {
            return _context.Components.Any(e => e.Id == id);
        }
    }
}
