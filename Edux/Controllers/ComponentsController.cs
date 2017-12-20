using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Edux.Data;
using Edux.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using System.Collections;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Edux.Controllers
{
    [Authorize]
    public class ComponentsController : ControllerBase
    {

        private readonly IHostingEnvironment hostingEnvironment;
        public ComponentsController(IHostingEnvironment environment, ApplicationDbContext context):base(context)
        {
            hostingEnvironment = environment;
        }

        // GET: Components
        public async Task<IActionResult> Index()
        {
            var components = _context.Components.Include(c => c.ComponentType).ThenInclude(ct => ct.Parameters).Include(c => c.ParentComponent).Include(c => c.ParameterValues);
            return View(components.ToList());
        }

        // GET: Components/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var component = await _context.Components
                .Include(c => c.ComponentType)
                .Include(c => c.ParentComponent)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (component == null)
            {
                return NotFound();
            }

            return View(component);
        }

        // GET: Components/Create
        public IActionResult Create(string PageId)
        {
            ViewData["ComponentTypeId"] = new SelectList(_context.ComponentTypes, "Id", "Name");
            ViewData["ParentComponentId"] = new SelectList(_context.Components.Where(c => c.PageId == PageId), "Id", "Name");
            ViewData["Pages"] = new SelectList(_context.Pages, "Id", "Title");
            var component = new Component();
            component.PageId = PageId;
            ViewBag.PageId = PageId;
            return View(component);
        }

        // POST: Components/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,DisplayName,ComponentTypeId,View,ParentComponentId,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId,PageId,Position")] Component component, string PageIdRef, IFormCollection form, IFormFile[] upload)
        {
            
            if (ModelState.IsValid)
            {

                _context.Add(component);
                await _context.SaveChangesAsync();
                // upload iþlemi
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
                    foreach (PropertyValue item in _context.PropertyValues.Include(i => i.Entity).Where(pv => pv.Entity.Name == eName && pv.RowId == rowId).ToList())
                    {
                        _context.Remove(item);
                    }
                    _context.SaveChanges();
                }
                else
                {

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
                                        string uploadLocation = hostingEnvironment.WebRootPath + "\\uploads\\" + category + "\\";
                                        string fileName = Path.GetFileName(upload[uploadIndex].FileName);
                                        var filePath = Path.Combine(uploadLocation, fileName);
                                        if (!Directory.Exists(uploadLocation))
                                        {
                                            Directory.CreateDirectory(uploadLocation); //Eðer klasör yoksa oluþtur    
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


                foreach (var key in form.Keys)
                {
                    Guid result;
                    if (Guid.TryParse(key, out result))
                    {
                        var value = new ParameterValue();
                        value.ParameterId = key;
                        value.ComponentId = component.Id;
                        value.Value = form[key].ToString();
                        value.CreateDate = DateTime.Now;
                        value.UpdateDate = DateTime.Now;
                        value.UpdatedBy = User.Identity.Name;
                        value.CreatedBy = User.Identity.Name;

                        _context.ParameterValues.Add(value);
                    }

                }
                await _context.SaveChangesAsync();
                if (PageIdRef != null)
                {
                    string url = "/Pages/Edit/" + PageIdRef + "#tab_1_2";
                    return Redirect(url);
                }

                return RedirectToAction("Index");
            }
            ViewData["ComponentTypeId"] = new SelectList(_context.ComponentTypes, "Id", "Name", component.ComponentTypeId);
            ViewData["ParentComponentId"] = new SelectList(_context.Components.Where(c => c.PageId == component.PageId), "Id", "Name", component.ParentComponentId);
            ViewData["Pages"] = new SelectList(_context.Pages, "Id", "Title", component.PageId);

            return View(component);
        }

        // GET: Components/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var component = await _context.Components.SingleOrDefaultAsync(m => m.Id == id);
            if (component == null)
            {
                return NotFound();
            }
            ViewData["ComponentTypeId"] = new SelectList(_context.ComponentTypes, "Id", "Name", component.ComponentTypeId);
            ViewData["ParentComponentId"] = new SelectList(_context.Components.Where(c=>c.PageId==component.PageId),  "Id", "Name", component.ParentComponentId);
            ViewData["Pages"] = new SelectList(_context.Pages, "Id", "Title", component.PageId);

            return View(component);
        }

        // POST: Components/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,DisplayName,ComponentTypeId,View,ParentComponentId,PageId,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Component component, IFormCollection form, IFormFile[] upload)
        {
            if (id != component.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                    // upload iþlemi
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
                        foreach (PropertyValue item in _context.PropertyValues.Include(i => i.Entity).Where(pv => pv.Entity.Name == eName && pv.RowId == rowId).ToList())
                        {
                            _context.Remove(item);
                        }
                        _context.SaveChanges();
                    }
                    else
                    {

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
                                            string uploadLocation = hostingEnvironment.WebRootPath + "\\uploads\\" + category + "\\";
                                            string fileName = Path.GetFileName(upload[uploadIndex].FileName);
                                            var filePath = Path.Combine(uploadLocation, fileName);
                                            if (!Directory.Exists(uploadLocation))
                                            {
                                                Directory.CreateDirectory(uploadLocation); //Eðer klasör yoksa oluþtur    
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
                
                try
                {

                    _context.Update(component);
                    await _context.SaveChangesAsync();
                    _context.ParameterValues.RemoveRange(_context.ParameterValues.Where(v => v.ComponentId == component.Id).ToArray());
                    await _context.SaveChangesAsync();
                    foreach (var key in form.Keys)
                    {
                        Guid result;
                        if (Guid.TryParse(key, out result))
                        {
                            var value = new ParameterValue();
                            value.ParameterId = key;
                            value.ComponentId = component.Id;
                            value.Value = form[key].ToString();
                            value.CreateDate = DateTime.Now;
                            value.UpdateDate = DateTime.Now;
                            value.UpdatedBy = User.Identity.Name;
                            value.CreatedBy = User.Identity.Name;

                            _context.ParameterValues.Add(value);
                        }

                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComponentExists(component.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Edit", "Pages", new { id = component.PageId });
            }
            ViewData["ComponentTypeId"] = new SelectList(_context.ComponentTypes, "Id", "Name", component.ComponentTypeId);
            ViewData["ParentComponentId"] = new SelectList(_context.Components.Where(c => c.PageId == component.PageId), "Id", "Name", component.ParentComponentId);
            ViewData["Pages"] = new SelectList(_context.Pages, "Id", "Title", component.PageId);


            return View(component);
        }

        // GET: Components/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var component = await _context.Components
                .Include(c => c.ComponentType)
                .Include(c => c.ParentComponent)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (component == null)
            {
                return NotFound();
            }

            return View(component);
        }

        // POST: Components/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {            
            List<ParameterValue> list = await _context.ParameterValues.Where(m => m.ComponentId == id).ToListAsync();
            foreach (ParameterValue item in list)
            {
                _context.ParameterValues.Remove(item);
            }
            var component = await _context.Components.SingleOrDefaultAsync(m => m.Id == id);
            _context.Components.Remove(component);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ComponentExists(string id)
        {
            return _context.Components.Any(e => e.Id == id);
        }
    }
}
