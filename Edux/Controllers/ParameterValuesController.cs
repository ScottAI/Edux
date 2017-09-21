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
    public class ParameterValuesController : ControllerBase
    {

        public ParameterValuesController(ApplicationDbContext context):base(context)
        {
        }

        // GET: ParameterValues
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ParameterValues.Include(p => p.Component).Include(p => p.Parameter);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ParameterValues/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parameterValue = await _context.ParameterValues
                .Include(p => p.Component)
                .Include(p => p.Parameter)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (parameterValue == null)
            {
                return NotFound();
            }

            return View(parameterValue);
        }

        // GET: ParameterValues/Create
        public IActionResult Create()
        {
            ViewData["ComponentId"] = new SelectList(_context.Components, "Id", "Name");
            ViewData["ParameterId"] = new SelectList(_context.Parameters, "Id", "Name");
            
            var parameterValue = new ParameterValue();
            return View(parameterValue);
        }

        // POST: ParameterValues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Value,ComponentId,ParameterId,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] ParameterValue parameterValue)
        {
            if (ModelState.IsValid)
            {
                parameterValue.CreatedBy = User.Identity.Name;
                parameterValue.UpdateDate = DateTime.Now;
                parameterValue.UpdatedBy = User.Identity.Name;
                parameterValue.CreateDate = DateTime.Now;
                _context.Add(parameterValue);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ComponentId"] = new SelectList(_context.Components, "Id", "Name", parameterValue.ComponentId);
            ViewData["ParameterId"] = new SelectList(_context.Parameters, "Id", "Name", parameterValue.ParameterId);
            return View(parameterValue);
        }

        // GET: ParameterValues/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parameterValue = await _context.ParameterValues.SingleOrDefaultAsync(m => m.Id == id);
            if (parameterValue == null)
            {
                return NotFound();
            }
            ViewData["ComponentId"] = new SelectList(_context.Components, "Id", "Name", parameterValue.ComponentId);
            ViewData["ParameterId"] = new SelectList(_context.Parameters, "Id", "Name", parameterValue.ParameterId);
            return View(parameterValue);
        }

        // POST: ParameterValues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Value,ComponentId,ParameterId,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] ParameterValue parameterValue)
        {
            if (id != parameterValue.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    parameterValue.UpdateDate = DateTime.Now;
                    parameterValue.UpdatedBy = User.Identity.Name;
                    _context.Update(parameterValue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParameterValueExists(parameterValue.Id))
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
            ViewData["ComponentId"] = new SelectList(_context.Components, "Id", "Name", parameterValue.ComponentId);
            ViewData["ParameterId"] = new SelectList(_context.Parameters, "Id", "Name", parameterValue.ParameterId);
            return View(parameterValue);
        }

        // GET: ParameterValues/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parameterValue = await _context.ParameterValues
                .Include(p => p.Component)
                .Include(p => p.Parameter)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (parameterValue == null)
            {
                return NotFound();
            }

            return View(parameterValue);
        }

        // POST: ParameterValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var parameterValue = await _context.ParameterValues.SingleOrDefaultAsync(m => m.Id == id);
            _context.ParameterValues.Remove(parameterValue);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ParameterValueExists(string id)
        {
            return _context.ParameterValues.Any(e => e.Id == id);
        }
    }
}
