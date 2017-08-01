using Edux.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.ViewComponents
{
    public class MenuItemsDataTable : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public MenuItemsDataTable(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string menuId)
        {
            var menus = _context.MenuItems.Include(c => c.Menu).Include(c => c.ParentMenuItem).Where(c => c.MenuId == menuId);
            ViewBag.MenuId = menuId;
            return View(await menus.ToListAsync());
        }
    }
}
