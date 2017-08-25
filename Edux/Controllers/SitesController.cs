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
    public class SitesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SitesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Sites
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sites.ToListAsync());
        }

        // GET: Sites/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var site = await _context.Sites
                .SingleOrDefaultAsync(m => m.Id == id);
            if (site == null)
            {
                return NotFound();
            }

            return View(site);
        }

        // GET: Sites/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Slug,DefaultLayout,AllowedRoles,DefaultPage,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Site site)
        {
            if (ModelState.IsValid)
            {
                site.CreatedBy = User.Identity.Name;
                site.UpdateDate = DateTime.Now;
                site.UpdatedBy = User.Identity.Name;
                site.CreateDate = DateTime.Now;
                _context.Add(site);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(site);
        }

        // GET: Sites/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var site = await _context.Sites.SingleOrDefaultAsync(m => m.Id == id);
            if (site == null)
            {
                return NotFound();
            }
            return View(site);
        }

        // POST: Sites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Slug,DefaultLayout,AllowedRoles,DefaultPage,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Site site)
        {
            if (id != site.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(site);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SiteExists(site.Id))
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
            return View(site);
        }

        // GET: Sites/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var site = await _context.Sites
                .SingleOrDefaultAsync(m => m.Id == id);
            if (site == null)
            {
                return NotFound();
            }

            return View(site);
        }

        // POST: Sites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var site = await _context.Sites.SingleOrDefaultAsync(m => m.Id == id);
            _context.Sites.Remove(site);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SiteExists(string id)
        {
            return _context.Sites.Any(e => e.Id == id);
        }
    }
}
