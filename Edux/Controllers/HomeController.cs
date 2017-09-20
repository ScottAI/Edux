using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Edux.Data;
using Microsoft.AspNetCore.Mvc;
using Edux.Models.PageViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Edux.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Edux.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IHostingEnvironment env;
        private readonly ApplicationDbContext _context;

        public HomeController(IHostingEnvironment _env,ApplicationDbContext context)
        {
            _context = context;
            this.env = _env;
        }

        public async Task<IActionResult> Index(string slug)
        {
            if (slug == null)
            {
                return Redirect("/tr/Giris");
            }
            else
            {
                var apps = await _context.Apps.ToListAsync();
                var app = apps.FirstOrDefault();
                ViewBag.Apps = apps;
                ViewBag.App = app;
                var model = new DisplayViewModel();

                // Getting the page with the slug that user entered
                var page = await _context.Pages
                    .Include(p => p.ParentPage).Include("Components.ComponentType").Include("Components.ChildComponents")
                    .Include(p => p.Components)
                            .ThenInclude(x => x.ParameterValues)
                                .ThenInclude(x => x.Parameter)
                    .FirstOrDefaultAsync(m => m.Slug.Equals(slug.ToLower()) && m.IsPublished == true);


                if (page == null)
                {
                    return Content($"'{slug}' adresli bulunamadı!");
                }
                else
                {
                    // Incrementing the ViewCount
                    page.ViewCount++;
                    _context.SaveChanges();
                    model.Page = page;
                    //ViewData["ComponentTypeId"] = new SelectList(_context.ComponentTypes, "Id", "DisplayName");
                    //ViewData["ParentComponentId"] = new SelectList(_context.Components, "Id", "DisplayName");
                    return View(page.View, model);
                }
            }
        }
        [HttpPost]
        public async Task<IActionResult> SaveForm(IFormCollection form, IFormFile[] upload)
        {
            if (ModelState.IsValid)
            {
                long rowId = 1;
                string mode = form["Mode"].ToString().ToLowerInvariant();
                if (String.IsNullOrEmpty(mode))
                {
                    mode = "create";
                }

                if (mode == "edit" || mode == "delete")
                {
                    rowId = Convert.ToInt64(form["RowId"].ToString());
                }
                else
                {
                    if (_context.PropertyValues.Any())
                    {
                        rowId = _context.PropertyValues.Max(m => m.RowId) + 1;
                    }
                }
                if (mode == "delete")
                {
                    var eName = _context.Forms.FirstOrDefault(frm => frm.Id == form["FormId"].ToString()).EntityName;
                    foreach (PropertyValue item in _context.PropertyValues.Include(i=>i.Entity).Where(pv=>pv.Entity.Name == eName && pv.RowId == rowId).ToList()) {
                        _context.Remove(item);
                    }
                    _context.SaveChanges();
                }
                else { 

                foreach (var key in form.Keys)
                {
                    if (_context.Fields.Any(f => f.FormId == form["FormId"].ToString() && f.PropertyId == key))
                    {

                        PropertyValue value;
                        if (mode == "create")
                        {
                            value = new PropertyValue();
                        }
                        else
                        {
                            value = _context.PropertyValues.Where(pv => pv.PropertyId == key && pv.RowId == rowId).FirstOrDefault();
                            if (value == null)
                            {
                                value = new PropertyValue();
                                value.PropertyId = key;
                                value.RowId = rowId;
                                value.CreateDate = DateTime.Now;
                                value.CreatedBy = User.Identity.Name;
                            }
                        }
                        if (mode == "create" || mode == "edit")
                        {
                            value.Value = form[key];
                            value.EntityId = form[key + ".EntityId"];
                            if (!String.IsNullOrEmpty(form[key + ".UploadIndex"]))
                            {
                                int uploadIndex = Convert.ToInt32(form[key + ".UploadIndex"]);
                                if (upload != null && upload.Count() >= (uploadIndex + 1) && upload[uploadIndex] != null)
                                {
                                    string category = DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString();
                                    string uploadLocation = env.WebRootPath + "\\uploads\\" + category + "\\";
                                    string fileName = Path.GetFileName(upload[uploadIndex].FileName);
                                    var filePath = Path.Combine(uploadLocation, fileName);
                                    if (!Directory.Exists(uploadLocation))
                                    {
                                        Directory.CreateDirectory(uploadLocation); //Eğer klasör yoksa oluştur    
                                    }
                                    using (var stream = new FileStream(filePath, FileMode.Create))
                                    {
                                        await upload[uploadIndex].CopyToAsync(stream);
                                    }
                                    value.Value = "/uploads/" + category + "/" + fileName;

                                }
                            
                            }
                            if (mode == "create")
                            {
                                value.PropertyId = key;
                                value.RowId = rowId;
                                value.CreateDate = DateTime.Now;
                                value.CreatedBy = User.Identity.Name;
                            }
                            value.UpdateDate = DateTime.Now;
                            value.UpdatedBy = User.Identity.Name;
                            value.AppTenantId = "1";
                        }
                        if (mode == "create")
                        {
                            _context.Add(value);
                        }
                        else if (mode == "edit")
                        {
                            _context.Update(value);
                        }
                        _context.SaveChanges();
                    }

                }
                }
                return Redirect(form["ReturnUrl"].ToString() + "?status=ok");
            }
            return Redirect(form["ReturnUrl"].ToString() + "?status=validationerror");
        }

        public JsonResult AjaxUpload(string Title, string Description, IFormFile uploadFile)
        {
             
            IFormFile file = Request.Form.Files[0];
            if (ModelState.IsValid)
            {
              
                if (uploadFile != null)
                {
                    Media media = new Media();
                    media.Name = uploadFile.FileName;
                    media.Description = Description;
                    media.FileSize = (uploadFile.Length / 1024);
                    media.CreatedBy = User.Identity.Name ?? "username";
                    media.CreateDate = DateTime.Now;
                    media.UpdatedBy = User.Identity.Name ?? "username";
                    media.UpdateDate = DateTime.Now;


                    string category = DateTime.Now.Month + "-" + DateTime.Now.Year + "-ProductImages";
                    string FilePath = env.WebRootPath + "\\uploads\\" + category + "\\";
                    string dosyaismi = Path.GetFileName(uploadFile.FileName);
                    var yuklemeYeri = Path.Combine(FilePath, dosyaismi);
                    media.FilePath = "uploads/" + category + "/" + dosyaismi;

                    if (!Directory.Exists(FilePath))
                    {
                        Directory.CreateDirectory(FilePath);//Eðer klasör yoksa oluþtur    
                    }
                    using (var stream = new FileStream(yuklemeYeri, FileMode.Create))
                    {
                        uploadFile.CopyTo(stream);
                    }


                    _context.Add(media);
                    _context.SaveChangesAsync();
                    return Json(new { result = FilePath + media.FilePath + media.Name });

                }
                else
                {
                    ModelState.AddModelError("FileName", "Dosya uzantýsý izin verilen uzantýlardan olmalýdýr.");
                }
            }
            else { ModelState.AddModelError("FileExist", "Lütfen bir dosya seçiniz!"); }        
            return Json(new { result = "false" });
        }

public IActionResult About()
{
    ViewData["Message"] = "Your application description page.";

    return View();
}

public IActionResult Contact()
{
    ViewData["Message"] = "Your contact page.";

    return View();
}

public IActionResult Error()
{
    return View();
}
    }
}
