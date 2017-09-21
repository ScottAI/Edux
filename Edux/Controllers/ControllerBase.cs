using Edux.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.Controllers
{
    public class ControllerBase:Controller
    {
        protected readonly ApplicationDbContext _context;
        public ControllerBase(ApplicationDbContext context)
        {
            this._context = context;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var apps = _context.Apps.ToList();
            var app = apps.FirstOrDefault();
            ViewBag.Apps = apps;
            ViewBag.App = app;
        }
       
    }
}
