using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Edux.Data;
using Edux.Models;

namespace Edux.Controllers
{
    public class ComponentTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComponentTypesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: ComponentTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ComponentTypes.ToListAsync());
        }

        // GET: ComponentTypes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentType = await _context.ComponentTypes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (componentType == null)
            {
                return NotFound();
            }

            return View(componentType);
        }

        // GET: ComponentTypes/Create
        public IActionResult Create()
        {
            var componentType = new ComponentType();
            return View(componentType);
        }

        // POST: ComponentTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,DisplayName,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] ComponentType componentType)
        {
            if (ModelState.IsValid)
            {
                componentType.CreatedBy= User.Identity.Name;
                componentType.UpdateDate = DateTime.Now;
                componentType.UpdatedBy = User.Identity.Name;
                componentType.CreateDate = DateTime.Now;
                _context.Add(componentType);
                await _context.SaveChangesAsync();
                return RedirectToAction("Edit",new {id=componentType.Id });
            }
            return View(componentType);
        }

        // GET: ComponentTypes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentType = await _context.ComponentTypes.SingleOrDefaultAsync(m => m.Id == id);
            if (componentType == null)
            {
                return NotFound();
            }
            return View(componentType);
        }

        // POST: ComponentTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,DisplayName,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] ComponentType componentType)
        {
            if (id != componentType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    componentType.UpdateDate = DateTime.Now;
                    componentType.UpdatedBy = User.Identity.Name;
                    _context.Update(componentType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComponentTypeExists(componentType.Id))
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
            return View(componentType);
        }

        // GET: ComponentTypes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentType = await _context.ComponentTypes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (componentType == null)
            {
                return NotFound();
            }

            return View(componentType);
        }

        // POST: ComponentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var componentType = await _context.ComponentTypes.SingleOrDefaultAsync(m => m.Id == id);
            _context.ComponentTypes.Remove(componentType);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ComponentTypeExists(string id)
        {
            return _context.ComponentTypes.Any(e => e.Id == id);
        }
    }
}
