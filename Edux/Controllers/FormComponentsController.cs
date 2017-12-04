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
using System.Collections;
using Microsoft.AspNetCore.Authorization;

namespace Edux.Controllers
{
    [Authorize]
    public class FormComponentsController : ControllerBase
    {


        public FormComponentsController(ApplicationDbContext context) : base(context)
        {

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
        public IActionResult Create(string FormId)
        {
            ViewData["ComponentTypeId"] = new SelectList(_context.ComponentTypes, "Id", "Name");
            ViewData["ParentComponentId"] = new SelectList(_context.Components.Where(c => c.FormId == FormId), "Id", "Name");
            ViewBag.Forms = new SelectList(_context.Forms.ToList(), "Id", "Name");
            var component = new Component();
            component.FormId = FormId;
            ViewBag.FormId = FormId;
            return View(component);
        }

        // POST: Components/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,DisplayName,ComponentTypeId,View,ParentComponentId,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId,FormId,Position")] Component component, string FormIdRef, IFormCollection form)
        {
            if (ModelState.IsValid)
            {

                _context.Add(component);
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
                if (FormIdRef != null)
                {
                    string url = "/Forms/Edit/" + FormIdRef + "#tab_1_5";
                    return Redirect(url);
                }

                return RedirectToAction("Index");
            }
            ViewData["ComponentTypeId"] = new SelectList(_context.ComponentTypes, "Id", "Name", component.ComponentTypeId);
            ViewData["ParentComponentId"] = new SelectList(_context.Components.Where(c => c.FormId == component.FormId), "Id", "Name", component.ParentComponentId);
            ViewBag.Forms = new SelectList(_context.Forms.ToList(), "Id", "Name", component.FormId);

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
            ViewData["ParentComponentId"] = new SelectList(_context.Components.Where(c => c.FormId == component.FormId), "Id", "Name", component.ParentComponentId);
            ViewBag.Forms = new SelectList(_context.Forms.ToList(), "Id", "Name", component.FormId);

            return View(component);
        }

        // POST: Components/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,DisplayName,ComponentTypeId,View,ParentComponentId,FormId,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Component component, IFormCollection form)
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
                string url = "/Forms/Edit/" + component.FormId + "#tab_1_5";
                return Redirect(url);
            }
            ViewData["ComponentTypeId"] = new SelectList(_context.ComponentTypes, "Id", "Name", component.ComponentTypeId);
            ViewData["ParentComponentId"] = new SelectList(_context.Components.Where(c => c.FormId == component.FormId), "Id", "Name", component.ParentComponentId);
            ViewBag.Forms = new SelectList(_context.Forms.ToList(), "Id", "Name", component.FormId);


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
            List<ParameterValue> list = await _context.ParameterValues.Where(m => m.ComponentId == id).ToListAsync();
            foreach (ParameterValue item in list)
            {
                _context.ParameterValues.Remove(item);
            }
            var component = await _context.Components.SingleOrDefaultAsync(m => m.Id == id);
            _context.Components.Remove(component);
            await _context.SaveChangesAsync();
            string url = "/Forms/Edit/" + component.FormId + "#tab_1_5";
            return Redirect(url);
        }

        private bool ComponentExists(string id)
        {
            return _context.Components.Any(e => e.Id == id);
        }
    }
}
