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
    public class FieldsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FieldsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Fields
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Fields.Include(x=> x.Form).Include(x => x.Property).Include(x => x.PropertyValue);
            return View(await applicationDbContext.ToListAsync());
        }


        // GET: Fields/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @field = await _context.Fields
                .Include(x => x.Form)
                .Include(x => x.Property)
                .Include(x => x.PropertyValue)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (@field == null)
            {
                return NotFound();
            }

            return View(@field);
        }

        // GET: Fields/Create
        public IActionResult Create(string formId)
        {
            var field = new Field();
            ViewData["FormId"] = new SelectList(_context.Forms, "Id", "Name");
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Name");
            ViewData["PropertyValueId"] = new SelectList(_context.PropertyValues, "Id", "Value");
            field.FormId = formId;
            ViewBag.FormIdRef = formId;
            return View(field);
            
        }

        // POST: Fields/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,DisplayName,FormId,PropertyId,PropertyValueId,Tab,Row,Col,Position,EditorType,DefaultValue,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Field @field,string FormIdRef)
        {
            if (ModelState.IsValid)
            {
                field.CreateDate = DateTime.Now;
                field.CreatedBy = User.Identity.Name;
                field.UpdateDate = DateTime.Now;
                field.UpdatedBy = User.Identity.Name;
                _context.Add(@field);
                await _context.SaveChangesAsync();
                if (FormIdRef !=null)
                {
                    string url="/Forms/Edit/" + FormIdRef + "#tab_1_2";
                    return Redirect(url);
                }
                return RedirectToAction("Index");
            }
            ViewData["FormId"] = new SelectList(_context.Forms,"Name", field.FormId);
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Name", @field.PropertyId);
            ViewData["PropertyValueId"] = new SelectList(_context.PropertyValues, "Id", "Value", @field.PropertyValueId);
            return View(@field);

    }

        // GET: Fields/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @field = await _context.Fields.SingleOrDefaultAsync(m => m.Id == id);
            if (@field == null)
            {
                return NotFound();
            }
            ViewData["FormId"] = new SelectList(_context.Forms, "Id", "Name", @field.FormId);
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Name", @field.PropertyId);
            ViewData["PropertyValueId"] = new SelectList(_context.PropertyValues, "Id", "Value", @field.PropertyValueId);
            return View(@field);
        }

        // POST: Fields/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,DisplayName,FormId,PropertyId,PropertyValueId,Tab,Row,Col,Position,EditorType,DefaultValue,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Field @field)
        {
            if (id != @field.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    field.UpdateDate = DateTime.Now;
                    field.UpdatedBy = User.Identity.Name;
                    _context.Update(@field);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FieldExists(@field.Id))
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
            ViewData["FormId"] = new SelectList(_context.Forms, "Id", "Name", @field.FormId);
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Name", @field.PropertyId);
            ViewData["PropertyValueId"] = new SelectList(_context.PropertyValues, "Id", "Value", @field.PropertyValueId);
            return View(@field);
        }

        // GET: Fields/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @field = await _context.Fields
                .Include(x => x.Form)
                .Include(x => x.Property)
                .Include(x => x.PropertyValue)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (@field == null)
            {
                return NotFound();
            }

            return View(@field);
        }

        // POST: Fields/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var @field = await _context.Fields.SingleOrDefaultAsync(m => m.Id == id);
            _context.Fields.Remove(@field);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool FieldExists(string id)
        {
            return _context.Fields.Any(e => e.Id == id);
        }
    }
}
