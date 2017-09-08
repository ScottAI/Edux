using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Edux.Data;
using Edux.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Edux.Controllers
{
    [Authorize]
    public class PropertiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PropertiesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Properties
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Properties.Include(x => x.Entity);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Properties/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @property = await _context.Properties
                .Include(x => x.Entity)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (@property == null)
            {
                return NotFound();
            }

            return View(@property);
        }

        // GET: Properties/Create
        public IActionResult Create(string EntityId)
        {
            ViewData["EntityId"] = new SelectList(_context.Entities, "Id", "Name");
            ViewData["Entities"] = new SelectList(_context.Entities, "Id", "Name");
            ViewData["DataSourceEntityId"] = new SelectList(_context.Entities, "Id", "Name");
            ViewData["DataSourcePropertyId"] = new SelectList(_context.Properties, "Id", "Name");
            var property = new Property();
            property.EntityId = EntityId;
            ViewBag.EntityId = EntityId;
            return View(property);
        }

        // POST: Properties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> Create([Bind("Name,DisplayName,DefaultValue,DisplayFormat,DataSourceEntity,DataSourceProperty,IsRequired,PropertyType,StringLength,EntityId,Position,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Property @property , string entityId, IFormCollection form)
        {

            if (ModelState.IsValid)
            {
                property.CreateDate = DateTime.Now;
                property.CreatedBy = User.Identity.Name;
                property.UpdateDate = DateTime.Now;
                property.UpdatedBy = User.Identity.Name;
                _context.Add(@property);
                await _context.SaveChangesAsync();
                if (entityId != null)
                {
                    return RedirectToAction("Edit", "Entities", new { id = property.EntityId });
                }

               
            }
            ViewData["EntityId"] = new SelectList(_context.Entities, "Id", "Name", @property.EntityId);
            ViewData["DataSourceEntityId"] = new SelectList(_context.Entities, "Id", "Name", @property.DataSourceEntityId);
            ViewData["DataSourcePropertyId"] = new SelectList(_context.Properties, "Id", "Name", @property.DataSourcePropertyId);

            return RedirectToAction("Index", "Properties");




        }
        

        // GET: Properties/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @property = await _context.Properties.SingleOrDefaultAsync(m => m.Id == id);
            if (@property == null)
            {
                return NotFound();
            }
            ViewData["EntityId"] = new SelectList(_context.Entities, "Id", "Name", @property.EntityId);
            return View(@property);
        }

        // POST: Properties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,DisplayName,DefaultValue,DisplayFormat,DataSourceEntity,DataSourceProperty,IsRequired,PropertyType,StringLength,EntityId,Position,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Property @property)
        {
            if (id != @property.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    property.UpdateDate = DateTime.Now;
                    property.UpdatedBy = User.Identity.Name;
                    _context.Update(@property);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertyExists(@property.Id))
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
            ViewData["EntityId"] = new SelectList(_context.Entities, "Id", "Name", @property.EntityId);
            return View(@property);
        }

        // GET: Properties/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @property = await _context.Properties
                .Include(x => x.Entity)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (@property == null)
            {
                return NotFound();
            }

            return View(@property);
        }

        // POST: Properties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var @property = await _context.Properties.SingleOrDefaultAsync(m => m.Id == id);
            _context.Properties.Remove(@property);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PropertyExists(string id)
        {
            return _context.Properties.Any(e => e.Id == id);
        }
    }
}
