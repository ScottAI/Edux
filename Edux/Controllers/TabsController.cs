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
    public class TabsController : ControllerBase
    {
        

        public TabsController(ApplicationDbContext context):base(context)
        {
            
        }

        // GET: Fields
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Tabs.Include(x=> x.Form);
            return View(await applicationDbContext.ToListAsync());
        }


        // GET: Fields/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @tab = await _context.Tabs
                .Include(x => x.Form)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (@tab == null)
            {
                return NotFound();
            }

            return View(@tab);
        }

        // GET: Fields/Create
        public IActionResult Create(string formId)
        {
            var tab = new Tab();
            ViewData["FormId"] = new SelectList(_context.Forms.OrderBy(f=>f.Name).ToList(), "Id", "Name");
            tab.FormId = formId;
            ViewBag.FormIdRef = formId;
            return View(tab);
            
        }

        // POST: Fields/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tab tab,string FormIdRef)
        {
            if (ModelState.IsValid)
            {
                tab.CreateDate = DateTime.Now;
                tab.CreatedBy = User.Identity.Name;
                tab.UpdateDate = DateTime.Now;
                tab.UpdatedBy = User.Identity.Name;
                _context.Add(tab);
                await _context.SaveChangesAsync();
                if (FormIdRef !=null)
                {
                    string url="/Forms/Edit/" + FormIdRef + "#tab_1_2";
                    return Redirect(url);
                }
                return RedirectToAction("Index");
            }
            ViewData["FormId"] = new SelectList(_context.Forms.OrderBy(f => f.Name).ToList(), "Name", tab.FormId);
          
            return View(tab);

    }

        // GET: Fields/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tab = await _context.Tabs.SingleOrDefaultAsync(m => m.Id == id);
            if (tab == null)
            {
                return NotFound();
            }
            ViewData["FormId"] = new SelectList(_context.Forms.OrderBy(f => f.Name).ToList(), "Id", "Name", tab.FormId);
           
            return View(tab);
        }

        // POST: Fields/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Tab tab)
        {
            if (id != tab.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tab.UpdateDate = DateTime.Now;
                    tab.UpdatedBy = User.Identity.Name;
                    _context.Update(tab);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TabExists(tab.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Edit", "Forms", new { id = tab.FormId });
            }
            ViewData["FormId"] = new SelectList(_context.Forms.OrderBy(f => f.Name).ToList(), "Id", "Name", tab.FormId);
            
            return View(tab);
        }

        // GET: Fields/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @tab = await _context.Tabs
                .Include(x => x.Form)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (@tab == null)
            {
                return NotFound();
            }

            return View(@tab);
        }

        // POST: Fields/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var @tab = await _context.Tabs.SingleOrDefaultAsync(m => m.Id == id);
            _context.Tabs.Remove(@tab);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TabExists(string id)
        {
            return _context.Tabs.Any(e => e.Id == id);
        }
    }
}
