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
            string appSlug = "centralpanel";
            if (context.RouteData.Values["app"] != null) { 
                appSlug = context.RouteData.Values["app"].ToString().ToLowerInvariant();
            }
            var app = apps.FirstOrDefault(a => a.Slug == appSlug);
            ViewBag.Apps = apps;
            ViewBag.App = app;
        }
       
    }
}
