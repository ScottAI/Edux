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
    public class MenuItemsController : ControllerBase
    {
        

        public MenuItemsController(ApplicationDbContext context):base(context)
        {
            
        }

        // GET: MenuItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MenuItems.Include(m => m.Menu).Include(m => m.ParentMenuItem);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MenuItems/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItem = await _context.MenuItems
                .Include(m => m.Menu)
                .Include(m => m.ParentMenuItem)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (menuItem == null)
            {
                return NotFound();
            }

            return View(menuItem);
        }

        // GET: MenuItems/Create
        public IActionResult Create(string MenuId)
        {
            ViewBag.menuIdRef = MenuId;
            ViewData["MenuId"] = new SelectList(_context.Menus, "Id", "Name");
            ViewData["ParentMenuItemId"] = new SelectList(_context.MenuItems, "Id", "Name");
            var menuItem = new MenuItem();
            return View(menuItem);
        }

        // POST: MenuItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuItem menuItem, string MenuIdRef)
        {
            if (ModelState.IsValid)
            {
                menuItem.CreateDate = DateTime.Now;
                menuItem.CreatedBy = User.Identity.Name;
                menuItem.UpdateDate = DateTime.Now;
                menuItem.UpdatedBy = User.Identity.Name;
                _context.Add(menuItem);
                await _context.SaveChangesAsync();
                if (MenuIdRef!=null)
                  
                    {
                        string url = "/Menus/Edit/" + MenuIdRef + "#tab_1_2";
                        return Redirect(url);
                    }

                return RedirectToAction("Index");
            }
            ViewData["MenuId"] = new SelectList(_context.Menus, "Id", "Id", menuItem.MenuId);
            ViewData["ParentMenuItemId"] = new SelectList(_context.MenuItems, "Id", "Id", menuItem.ParentMenuItemId);
            return View(menuItem);
        }

        // GET: MenuItems/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItem = await _context.MenuItems.SingleOrDefaultAsync(m => m.Id == id);
            if (menuItem == null)
            {
                return NotFound();
            }
            ViewData["MenuId"] = new SelectList(_context.Menus, "Id", "Name", menuItem.MenuId);
            ViewData["ParentMenuItemId"] = new SelectList(_context.MenuItems, "Id", "Name", menuItem.ParentMenuItemId);
            return View(menuItem);
        }

        // POST: MenuItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Url,Target,Position,IsPublished,ParentMenuItemId,MenuId,Icon,CssClass,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] MenuItem menuItem)
        {
            if (id != menuItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    menuItem.UpdateDate = DateTime.Now;
                    menuItem.UpdatedBy = User.Identity.Name;
                    _context.Update(menuItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuItemExists(menuItem.Id))
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
            ViewData["MenuId"] = new SelectList(_context.Menus, "Id", "Id", menuItem.MenuId);
            ViewData["ParentMenuItemId"] = new SelectList(_context.MenuItems, "Id", "Id", menuItem.ParentMenuItemId);
            return View(menuItem);
        }

        // GET: MenuItems/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItem = await _context.MenuItems
                .Include(m => m.Menu)
                .Include(m => m.ParentMenuItem)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (menuItem == null)
            {
                return NotFound();
            }

            return View(menuItem);
        }

        // POST: MenuItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var menuItem = await _context.MenuItems.SingleOrDefaultAsync(m => m.Id == id);
            _context.MenuItems.Remove(menuItem);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MenuItemExists(string id)
        {
            return _context.MenuItems.Any(e => e.Id == id);
        }
    }
}
