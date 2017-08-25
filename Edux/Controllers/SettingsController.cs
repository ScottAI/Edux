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





        [HttpPost]
        public async Task <IActionResult> Index (Setting setting)
        {
            Setting allSetting = _context.Settings.FirstOrDefault();
            var firstTime = false;
            if (allSetting == null)
            {
                allSetting = new Setting();
                firstTime = true;
            }

                    allSetting.PageViews = setting.PageViews;
                    allSetting.ComponentViews = setting.ComponentViews;
                    allSetting.LayoutViews = setting.LayoutViews;
                    allSetting.SmtpUserName = setting.SmtpUserName;
                    allSetting.SmtpPassword = setting.SmtpPassword;
                    allSetting.SmtpHost = setting.SmtpHost;
                    allSetting.SmtpPort = setting.SmtpPort;
                    allSetting.SmtpUseSSL = setting.SmtpUseSSL;
                    allSetting.Id = setting.Id;
                    allSetting.SmtpPassword = setting.SmtpPassword;
                    allSetting.CreateDate = setting.CreateDate;
                    allSetting.CreatedBy = setting.CreatedBy;                

                    allSetting.UpdateDate = DateTime.Now;
                    allSetting.UpdatedBy = User.Identity.Name ?? "username";
                    allSetting.AppTenantId = setting.AppTenantId;
            if (firstTime)
            {
                _context.Settings.Add(allSetting);
            }
                    await _context.SaveChangesAsync();
                       ViewBag.Message = "Ayarlar baþarýyla kaydedildi";
            return View(setting);
        }



      
    }
}
