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
    public class ParametersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParametersController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Parameters
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Parameters.Include(p => p.ComponentType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Parameters/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parameter = await _context.Parameters
                .Include(p => p.ComponentType)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (parameter == null)
            {
                return NotFound();
            }

            return View(parameter);
        }

        // GET: Parameters/Create
        public IActionResult Create()
        {

            ViewData["ComponentTypeId"] = new SelectList(_context.ComponentTypes, "Id", "Id");
            var parameter = new Parameter();

            return View(parameter);
        }

        // POST: Parameters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,DisplayName,IsRequired,ComponentTypeId,Position,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Parameter parameter)
        {
            if (ModelState.IsValid)
            {
                parameter.CreateDate = DateTime.Now;
                parameter.CreatedBy = User.Identity.Name;
                parameter.UpdatedBy = User.Identity.Name;
                _context.Add(parameter);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ComponentTypeId"] = new SelectList(_context.ComponentTypes, "Id", "Id", parameter.ComponentTypeId);
            return View(parameter);
        }

        // GET: Parameters/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parameter = await _context.Parameters.SingleOrDefaultAsync(m => m.Id == id);
            if (parameter == null)
            {
                return NotFound();
            }
            ViewData["ComponentTypeId"] = new SelectList(_context.ComponentTypes, "Id", "Id", parameter.ComponentTypeId);
            return View(parameter);
        }

        // POST: Parameters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,DisplayName,IsRequired,ComponentTypeId,Position,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Parameter parameter)
        {
            if (id != parameter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    parameter.UpdateDate = DateTime.Now;
                    parameter.UpdatedBy = User.Identity.Name;
                    _context.Update(parameter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParameterExists(parameter.Id))
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
            ViewData["ComponentTypeId"] = new SelectList(_context.ComponentTypes, "Id", "Id", parameter.ComponentTypeId);
            return View(parameter);
        }

        // GET: Parameters/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parameter = await _context.Parameters
                .Include(p => p.ComponentType)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (parameter == null)
            {
                return NotFound();
            }

            return View(parameter);
        }

        // POST: Parameters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var parameter = await _context.Parameters.SingleOrDefaultAsync(m => m.Id == id);
            _context.Parameters.Remove(parameter);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ParameterExists(string id)
        {
            return _context.Parameters.Any(e => e.Id == id);
        }
    }
}
