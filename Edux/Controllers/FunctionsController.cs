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
    public class FunctionsController : ControllerBase
    {
        public FunctionsController(ApplicationDbContext context):base(context)
        {
        }






      

        // GET: Functions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Functions.ToListAsync());
        }

        // GET: Functions/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var function = await _context.Functions
                .SingleOrDefaultAsync(m => m.Id == id);
            if (function == null)
            {
                return NotFound();
            }

            return View(function);
        }

        // GET: Functions/Create
        public IActionResult Create()
        {
            var function = new Function();
            return View(function);
        }

        // POST: Functions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Code,IsAnonymous,AllowedRoles,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Function function)
        {
            if (ModelState.IsValid)
            {
                function.CreatedBy = User.Identity.Name;
                function.UpdateDate = DateTime.Now;
                function.UpdatedBy = User.Identity.Name;
                function.CreateDate = DateTime.Now;
                _context.Add(function);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(function);
        }

        // GET: Functions/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var function = await _context.Functions.SingleOrDefaultAsync(m => m.Id == id);
            if (function == null)
            {
                return NotFound();
            }
            return View(function);
        }

        // POST: Functions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Code,IsAnonymous,AllowedRoles,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Function function)
        {
            if (id != function.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    function.UpdateDate = DateTime.Now;
                    function.UpdatedBy = User.Identity.Name;
                    _context.Update(function);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FunctionExists(function.Id))
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
            return View(function);
        }

        // GET: Functions/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var function = await _context.Functions
                .SingleOrDefaultAsync(m => m.Id == id);
            if (function == null)
            {
                return NotFound();
            }

            return View(function);
        }

        // POST: Functions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var function = await _context.Functions.SingleOrDefaultAsync(m => m.Id == id);
            _context.Functions.Remove(function);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool FunctionExists(string id)
        {
            return _context.Functions.Any(e => e.Id == id);
        }
    }
}
