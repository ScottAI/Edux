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
    public class FieldsetsController : ControllerBase
    {


        public FieldsetsController(ApplicationDbContext context) : base(context)
        {

        }

        // GET: Fields
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Fieldsets.Include(x => x.Form);
            return View(await applicationDbContext.ToListAsync());
        }


        // GET: Fields/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @fieldset = await _context.Fieldsets
                .Include(x => x.Form)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (@fieldset == null)
            {
                return NotFound();
            }

            return View(@fieldset);
        }

        // GET: Fields/Create
        public IActionResult Create(string formId)
        {
            var fieldset = new Fieldset();
            ViewData["FormId"] = new SelectList(_context.Forms.OrderBy(f => f.Name).ToList(), "Id", "Name");
            fieldset.FormId = formId;
            ViewBag.FormIdRef = formId;
            return View(fieldset);

        }

        // POST: Fields/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Fieldset fieldset, string FormIdRef)
        {
            if (ModelState.IsValid)
            {
                fieldset.CreateDate = DateTime.Now;
                fieldset.CreatedBy = User.Identity.Name;
                fieldset.UpdateDate = DateTime.Now;
                fieldset.UpdatedBy = User.Identity.Name;
                _context.Add(fieldset);
                await _context.SaveChangesAsync();
                if (FormIdRef != null)
                {
                    string url = "/Forms/Edit/" + FormIdRef + "#tab_1_2";
                    return Redirect(url);
                }
                return RedirectToAction("Index");
            }
            ViewData["FormId"] = new SelectList(_context.Forms.OrderBy(f => f.Name).ToList(), "Name", fieldset.FormId);

            return View(fieldset);

        }

        // GET: Fields/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fieldset = await _context.Fieldsets.SingleOrDefaultAsync(m => m.Id == id);
            if (fieldset == null)
            {
                return NotFound();
            }
            ViewData["FormId"] = new SelectList(_context.Forms.OrderBy(f => f.Name).ToList(), "Id", "Name", fieldset.FormId);

            return View(fieldset);
        }

        // POST: Fields/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Fieldset fieldset)
        {
            if (id != fieldset.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    fieldset.UpdateDate = DateTime.Now;
                    fieldset.UpdatedBy = User.Identity.Name;
                    _context.Update(fieldset);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TabExists(fieldset.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Edit", "Forms", new { id = fieldset.FormId });
            }
            ViewData["FormId"] = new SelectList(_context.Forms.OrderBy(f => f.Name).ToList(), "Id", "Name", fieldset.FormId);

            return View(fieldset);
        }

        // GET: Fields/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @fieldset = await _context.Fieldsets
                .Include(x => x.Form)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (@fieldset == null)
            {
                return NotFound();
            }

            return View(@fieldset);
        }

        // POST: Fields/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var @fieldset = await _context.Fieldsets.SingleOrDefaultAsync(m => m.Id == id);
            _context.Fieldsets.Remove(@fieldset);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TabExists(string id)
        {
            return _context.Fieldsets.Any(e => e.Id == id);
        }
    }
}
