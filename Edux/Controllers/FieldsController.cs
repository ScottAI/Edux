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
    public class FieldsController : ControllerBase
    {
        

        public FieldsController(ApplicationDbContext context):base(context)
        {
            
        }

        // GET: Fields
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Fields.Include(x=> x.Form).Include(x => x.Property);
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
            ViewData["FormId"] = new SelectList(_context.Forms.OrderBy(f=>f.Name).ToList(), "Id", "Name");
            ViewData["EntityId"] = new SelectList(_context.Entities.OrderBy(f => f.Name).ToList(), "Id", "Name");
            ViewData["PropertyId"] = new SelectList(_context.Properties.OrderBy(f => f.Name).ToList(), "Id", "Name");
            ViewData["ComponentId"] = new SelectList(_context.Components.OrderBy(f => f.Name).ToList(), "Id", "Name");

            ViewBag.Tabs = new SelectList(_context.Tabs.Where(t => t.FormId == formId).OrderBy(o => o.Position).ToList(), "Id", "Name");
            ViewBag.Fieldsets = new SelectList(_context.Fieldsets.Where(t => t.FormId == formId).OrderBy(o => o.Position).ToList(), "Id", "DisplayName");
            ViewBag.Components = new SelectList(_context.Components.Where(t => t.FormId == formId).OrderBy(o => o.Position).ToList(), "Id", "Name");

            var form = _context.Forms.FirstOrDefault(f => f.Id == formId);
            if (form!=null)
            {
                field.EntityId = form.EntityId;
            }
            field.FormId = formId;
            ViewBag.FormIdRef = formId;
            return View(field);
            
        }

        // POST: Fields/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FieldsetId,EntityId,OptionLabel,Name,DisplayName,FormId,PropertyId,ComponentId,PropertyValueId,TabId,Row,Col,Position,EditorType,DefaultValue,Id,IsReadOnly,IsVisible,InvisibleToRoles,VisibleToRoles,ReadOnlyToRoles,EditableToRoles,CreateDate,CreatedBy,UpdateDate,OnChange,OnClick,CssClass,UpdatedBy,AppTenantId,Visibility")] Field field,string FormIdRef)
        {
            if (ModelState.IsValid)
            {
                field.CreateDate = DateTime.Now;
                field.CreatedBy = User.Identity.Name;
                field.UpdateDate = DateTime.Now;
                field.UpdatedBy = User.Identity.Name;
                _context.Add(field);
                await _context.SaveChangesAsync();
                if (FormIdRef !=null)
                {
                    string url="/Forms/Edit/" + FormIdRef + "#tab_1_3";
                    return Redirect(url);
                }
                return RedirectToAction("Index");
            }
            ViewData["FormId"] = new SelectList(_context.Forms.OrderBy(f => f.Name).ToList(), "Name", field.FormId);
            ViewData["EntityId"] = new SelectList(_context.Entities.OrderBy(f => f.Name).ToList(), "Id", "Name", field.EntityId);
            ViewData["PropertyId"] = new SelectList(_context.Properties.OrderBy(f => f.Name).ToList(), "Id", "Name", field.PropertyId);
            ViewData["ComponentId"] = new SelectList(_context.Components.OrderBy(f => f.Name).ToList(), "Id", "Name", field.ComponentId);
            ViewBag.Tabs = new SelectList(_context.Tabs.Where(t => t.FormId == field.FormId).OrderBy(o => o.Position).ToList(), "Id", "Name", field.TabId);
            ViewBag.Fieldsets = new SelectList(_context.Fieldsets.Where(t => t.FormId == field.FormId).OrderBy(o => o.Position).ToList(), "Id", "DisplayName");
            return View(field);

    }

        // GET: Fields/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var field = await _context.Fields.SingleOrDefaultAsync(m => m.Id == id);
            if (field == null)
            {
                return NotFound();
            }
            ViewData["FormId"] = new SelectList(_context.Forms.OrderBy(f => f.Name).ToList(), "Id", "Name", field.FormId);
            ViewData["EntityId"] = new SelectList(_context.Entities.OrderBy(f => f.Name).ToList(), "Id", "Name", field.EntityId);
            ViewData["PropertyId"] = new SelectList(_context.Properties.OrderBy(f => f.Name).ToList(), "Id", "Name", field.PropertyId);
            ViewData["ComponentId"] = new SelectList(_context.Components.OrderBy(f => f.Name).ToList(), "Id", "Name");

            ViewBag.Components = new SelectList(_context.Components.Where(t => t.FormId == field.FormId).OrderBy(o => o.Position).ToList(), "Id", "Name", field.ComponentId);
            ViewBag.Tabs = new SelectList(_context.Tabs.Where(t => t.FormId == field.FormId).OrderBy(o => o.Position).ToList(), "Id", "Name", field.TabId);
            ViewBag.Fieldsets = new SelectList(_context.Fieldsets.Where(t => t.FormId == field.FormId).OrderBy(o => o.Position).ToList(), "Id", "DisplayName");
            return View(field);
        }

        // POST: Fields/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("FieldsetId,EntityId,OptionLabel,Name,DisplayName,FormId,PropertyId,ComponentId,OnChange,OnClick,CssClass,PropertyValueId,TabId,Row,Col,Position,EditorType,DefaultValue,Id,IsReadOnly,IsVisible,InvisibleToRoles,VisibleToRoles,ReadOnlyToRoles,EditableToRoles,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId,Visibility")] Field field)
        {
            if (id != field.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    field.UpdateDate = DateTime.Now;
                    field.UpdatedBy = User.Identity.Name;
                    _context.Update(field);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FieldExists(field.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Edit", "Forms", new { id = field.FormId });
            }
            ViewData["FormId"] = new SelectList(_context.Forms.OrderBy(f => f.Name).ToList(), "Id", "Name", field.FormId);
            ViewData["EntityId"] = new SelectList(_context.Entities.OrderBy(f => f.Name).ToList(), "Id", "Name", field.EntityId);
            ViewData["PropertyId"] = new SelectList(_context.Properties.OrderBy(f => f.Name).ToList(), "Id", "Name", field.PropertyId);
            ViewData["ComponentId"] = new SelectList(_context.Components.OrderBy(f => f.Name).ToList(), "Id", "Name", field.ComponentId);
            ViewBag.Tabs = new SelectList(_context.Tabs.Where(t => t.FormId == field.FormId).OrderBy(o => o.Position).ToList(), "Id", "Name", field.TabId);
            ViewBag.Fieldsets = new SelectList(_context.Fieldsets.Where(t => t.FormId == field.FormId).OrderBy(o => o.Position).ToList(), "Id", "DisplayName");

            return View(field);
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
        public IActionResult GetProperties(string entityId)
        {
            var properties = _context.Properties.Where(p => p.EntityId == entityId).OrderBy(f => f.Name).Select(p => new { Id = p.Id, Name = p.Name }).ToList();

            return Ok(properties);
        }
    }
}
