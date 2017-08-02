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
    public class FormsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FormsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Forms
        public async Task<IActionResult> Index()
        {
            return View(await _context.Forms.ToListAsync());
        }

        // GET: Forms/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var form = await _context.Forms
                .SingleOrDefaultAsync(m => m.Id == id);
            if (form == null)
            {
                return NotFound();
            }

            return View(form);
        }

        // GET: Forms/Create
        public IActionResult Create()
        {
            var form = new Form();
            return View(form);
        }

        // POST: Forms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,DisplayName,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Form form)
        {
            if (ModelState.IsValid)
            {
                form.CreateDate = DateTime.Now;
                form.CreatedBy = User.Identity.Name;
                form.UpdateDate = DateTime.Now;
                form.UpdatedBy = User.Identity.Name;
                _context.Add(form);
                await _context.SaveChangesAsync();
                return RedirectToAction("Edit",new { id=form.Id});
            }
            return View(form);
        }

        // GET: Forms/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var form = await _context.Forms.SingleOrDefaultAsync(m => m.Id == id);
            if (form == null)
            {
                return NotFound();
            }
            return View(form);
        }

        // POST: Forms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,DisplayName,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Form form)
        {
            if (id != form.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    form.UpdateDate = DateTime.Now;
                    form.UpdatedBy = User.Identity.Name;
                    _context.Update(form);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormExists(form.Id))
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
            return View(form);
        }

        // GET: Forms/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var form = await _context.Forms
                .SingleOrDefaultAsync(m => m.Id == id);
            if (form == null)
            {
                return NotFound();
            }

            return View(form);
        }

        // POST: Forms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var form = await _context.Forms.SingleOrDefaultAsync(m => m.Id == id);
            _context.Forms.Remove(form);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool FormExists(string id)
        {
            return _context.Forms.Any(e => e.Id == id);
        }
    }
}
