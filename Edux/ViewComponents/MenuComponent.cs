using Edux.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.ViewComponents
{
    public class MenuComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public MenuComponent(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(Models.Component component, string location)
        {
           // var menuItems=0;

            if (!String.IsNullOrEmpty(location))
            {
                var menu = await _context.Menus.Include(c => c.MenuItems).ThenInclude(t => t.ChildMenuItems).Where(m => m.MenuLocation == location).FirstOrDefaultAsync();
                if (menu == null)
                {
                    menu = new Models.Menu();
                }
                return View(menu);
            }

            return View();


        }
    }
}

