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
    public class AppTenantsController : Controller
    {
        private readonly HostDbContext _context;

        public AppTenantsController(HostDbContext context)
        {
            _context = context;
        }

        // GET: AppTenants
        public async Task<IActionResult> Index()
        {
            var hostDbContext = _context.AppTenants.Include(a => a.Theme);
            return View(await hostDbContext.ToListAsync());
        }

        // GET: AppTenants/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appTenant = await _context.AppTenants
                .Include(a => a.Theme)
                .SingleOrDefaultAsync(m => m.AppTenantId == id);
            if (appTenant == null)
            {
                return NotFound();
            }

            return View(appTenant);
        }

        // GET: AppTenants/Create
        public IActionResult Create()
        {
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "CreatedBy");
            return View();
        }

        // POST: AppTenants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppTenantId,Name,Title,Hostname,ThemeName,ThemeId,ConnectionString,Folder,RequireSSL,OwnerUserName,TrialEndDate,CreateDate")] AppTenant appTenant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appTenant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "CreatedBy", appTenant.ThemeId);
            return View(appTenant);
        }

        // GET: AppTenants/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appTenant = await _context.AppTenants.SingleOrDefaultAsync(m => m.AppTenantId == id);
            if (appTenant == null)
            {
                return NotFound();
            }
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "CreatedBy", appTenant.ThemeId);
            return View(appTenant);
        }

        // POST: AppTenants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("AppTenantId,Name,Title,Hostname,ThemeName,ThemeId,ConnectionString,Folder,RequireSSL,OwnerUserName,TrialEndDate,CreateDate")] AppTenant appTenant)
        {
            if (id != appTenant.AppTenantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appTenant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppTenantExists(appTenant.AppTenantId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "CreatedBy", appTenant.ThemeId);
            return View(appTenant);
        }

        // GET: AppTenants/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appTenant = await _context.AppTenants
                .Include(a => a.Theme)
                .SingleOrDefaultAsync(m => m.AppTenantId == id);
            if (appTenant == null)
            {
                return NotFound();
            }

            return View(appTenant);
        }

        // POST: AppTenants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var appTenant = await _context.AppTenants.SingleOrDefaultAsync(m => m.AppTenantId == id);
            _context.AppTenants.Remove(appTenant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppTenantExists(string id)
        {
            return _context.AppTenants.Any(e => e.AppTenantId == id);
        }
    }
}
