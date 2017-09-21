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
    public class AppsController : ControllerBase
    {
   

        public AppsController(ApplicationDbContext context):base(context)
        {
        }

        // GET: Sites
        public async Task<IActionResult> Index()
        {
            return View(await _context.Apps.ToListAsync());
        }

        // GET: Sites/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var app = await _context.Apps
                .SingleOrDefaultAsync(m => m.Id == id);
            if (app == null)
            {
                return NotFound();
            }

            return View(app);
        }

        // GET: Sites/Create
        public IActionResult Create()
        {
            var app = new App();
            return View(app);
        }

        // POST: Sites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Slug,DisplayName,Icon,DefaultLayout,AllowedRoles,DefaultPage,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] App app)
        {
            if (ModelState.IsValid)
            {
                app.CreatedBy = User.Identity.Name;
                app.UpdateDate = DateTime.Now;
                app.UpdatedBy = User.Identity.Name;
                app.CreateDate = DateTime.Now;
                _context.Add(app);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(app);
        }

        // GET: Sites/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var app = await _context.Apps.SingleOrDefaultAsync(m => m.Id == id);
            if (app == null)
            {
                return NotFound();
            }
            return View(app);
        }

        // POST: Sites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Slug,DisplayName,Icon,DefaultLayout,AllowedRoles,DefaultPage,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] App app)
        {
            if (id != app.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    app.CreatedBy = User.Identity.Name;
                    app.UpdateDate = DateTime.Now;
                    app.UpdatedBy = User.Identity.Name;
                    app.CreateDate = DateTime.Now;
                    _context.Update(app);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppExists(app.Id))
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
            return View(app);
        }

        // GET: Sites/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var app = await _context.Apps
                .SingleOrDefaultAsync(m => m.Id == id);
            if (app == null)
            {
                return NotFound();
            }

            return View(app);
        }

        // POST: Sites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var app = await _context.Apps.SingleOrDefaultAsync(m => m.Id == id);
            _context.Apps.Remove(app);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AppExists(string id)
        {
            return _context.Apps.Any(e => e.Id == id);
        }
    }
}
