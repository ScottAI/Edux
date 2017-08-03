using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Edux.Data;
using Edux.Models;
using Microsoft.AspNetCore.Authorization;

namespace Edux.Controllers
{
    [Authorize]
    public class PropertyValuesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PropertyValuesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: PropertyValues
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PropertyValues.Include(p => p.Entity).Include(p => p.Property);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PropertyValues/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyValue = await _context.PropertyValues
                .Include(p => p.Entity)
                .Include(p => p.Property)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (propertyValue == null)
            {
                return NotFound();
            }

            return View(propertyValue);
        }

        // GET: PropertyValues/Create
        public IActionResult Create()
        {
            ViewData["EntityId"] = new SelectList(_context.Entities, "Id", "Name");
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Name");
            var PValues = new PropertyValue();
            return View(PValues);
        }

        // POST: PropertyValues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RowId,Value,EntityId,PropertyId,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] PropertyValue propertyValue)
        {
            if (ModelState.IsValid)
            {
                propertyValue.CreateDate = DateTime.Now;
                propertyValue.CreatedBy = User.Identity.Name;
                propertyValue.UpdateDate = DateTime.Now;
                propertyValue.UpdatedBy = User.Identity.Name;
                _context.Add(propertyValue);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["EntityId"] = new SelectList(_context.Entities, "Id", "Name", propertyValue.EntityId);
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Name", propertyValue.PropertyId);
           

            return View(propertyValue);
        }

        // GET: PropertyValues/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyValue = await _context.PropertyValues.SingleOrDefaultAsync(m => m.Id == id);
            if (propertyValue == null)
            {
                return NotFound();
            }
            ViewData["EntityId"] = new SelectList(_context.Entities, "Id", "Name", propertyValue.EntityId);
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Name", propertyValue.PropertyId);
            return View(propertyValue);
        }

        // POST: PropertyValues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("RowId,Value,EntityId,PropertyId,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] PropertyValue propertyValue)
        {
            if (id != propertyValue.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    propertyValue.UpdateDate = DateTime.Now;
                    propertyValue.UpdatedBy = User.Identity.Name;
                    _context.Update(propertyValue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertyValueExists(propertyValue.Id))
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
            ViewData["EntityId"] = new SelectList(_context.Entities, "Id", "Name", propertyValue.EntityId);
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Name", propertyValue.PropertyId);
            return View(propertyValue);
        }

        // GET: PropertyValues/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyValue = await _context.PropertyValues
                .Include(p => p.Entity)
                .Include(p => p.Property)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (propertyValue == null)
            {
                return NotFound();
            }

            return View(propertyValue);
        }

        // POST: PropertyValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var propertyValue = await _context.PropertyValues.SingleOrDefaultAsync(m => m.Id == id);
            _context.PropertyValues.Remove(propertyValue);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PropertyValueExists(string id)
        {
            return _context.PropertyValues.Any(e => e.Id == id);
        }
    }
}
