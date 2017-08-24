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
    public class SettingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SettingsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Settings
        public async Task<IActionResult> Index()
        {
            var setting = await _context.Settings.FirstOrDefaultAsync();
            return View(setting);
        }

        // GET: Settings/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setting = await _context.Settings
                .SingleOrDefaultAsync(m => m.Id == id);
            if (setting == null)
            {
                return NotFound();
            }

            return View(setting);
        }

        // GET: Settings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PageViews,ComponentViews,LayoutViews,SmtpUserName,SmtpPassword,SmtpHost,SmtpPort,SmtpUseSSL,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Setting setting)
        {
            if (ModelState.IsValid)
            {
                _context.Add(setting);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(setting);
        }

        // GET: Settings/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setting = await _context.Settings.SingleOrDefaultAsync(m => m.Id == id);
            if (setting == null)
            {
                return NotFound();
            }
            return View(setting);
        }

        // POST: Settings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PageViews,ComponentViews,LayoutViews,SmtpUserName,SmtpPassword,SmtpHost,SmtpPort,SmtpUseSSL,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Setting setting)
        {
            if (id != setting.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(setting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SettingExists(setting.Id))
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
            return View(setting);
        }

        // GET: Settings/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setting = await _context.Settings
                .SingleOrDefaultAsync(m => m.Id == id);
            if (setting == null)
            {
                return NotFound();
            }

            return View(setting);
        }

        // POST: Settings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var setting = await _context.Settings.SingleOrDefaultAsync(m => m.Id == id);
            _context.Settings.Remove(setting);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SettingExists(string id)
        {
            return _context.Settings.Any(e => e.Id == id);
        }
    }
}
