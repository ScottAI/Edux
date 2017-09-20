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
    public class ColumnsController : ControllerBase
    {
        public ColumnsController(ApplicationDbContext context):base(context)
        {
        }

        // GET: Columns
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Columns.Include(c => c.DataTable).Include(c => c.Property).Include(c=> c.Entity);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Columns/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var column = await _context.Columns
                .Include(c => c.DataTable)
                .Include(c => c.Property)
               .Include(c => c.Entity)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (column == null)
            {
                return NotFound();
            }

            return View(column);
        }

        // GET: Columns/Create
        public IActionResult Create(string DataTableId)
        {
            var column = new Column();
            column.DataTableId = DataTableId;
            ViewData["DataTableId"] = new SelectList(_context.DataTables, "Id", "Name", DataTableId);
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Name");
            ViewData["EntityId"] = new SelectList(_context.Entities, "Id", "Name");
            return View(column);
        }

        // POST: Columns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DataTableId,PropertyId,EntityId,Position,OrderBy,FilterOperator,FilterValue,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Column column)
        {
            if (ModelState.IsValid)
            {
                column.UpdateDate = DateTime.Now;
                column.CreateDate = DateTime.Now;
                column.CreatedBy = User.Identity.Name;
                column.UpdatedBy = User.Identity.Name;

                _context.Add(column);
                await _context.SaveChangesAsync();
                return RedirectToAction("Edit", "DataTables", new { id = column.DataTableId });
            }
            ViewData["DataTableId"] = new SelectList(_context.DataTables, "Id", "Name", column.DataTableId);
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Name", column.PropertyId);
            ViewData["EntityId"] = new SelectList(_context.Entities, "Id", "Name", column.EntityId);
            return View(column);
        }

        // GET: Columns/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var column = await _context.Columns.SingleOrDefaultAsync(m => m.Id == id);
            if (column == null)
            {
                return NotFound();
            }
            ViewData["DataTableId"] = new SelectList(_context.DataTables, "Id", "Name", column.DataTableId);
            ViewBag.SelectedPropertyId = column.PropertyId;
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Name", column.PropertyId);
            ViewData["EntityId"] = new SelectList(_context.Entities, "Id", "Name", column.EntityId);
            return View(column);
        }

        // POST: Columns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("DataTableId,PropertyId,EntityId,Position,OrderBy,FilterOperator,FilterValue,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Column column)
        {
            if (id != column.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {  
                    column.CreateDate = DateTime.Now;
                    column.UpdateDate = DateTime.Now;   
                    column.UpdatedBy = User.Identity.Name;
                    column.CreatedBy = User.Identity.Name;
                    _context.Update(column);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ColumnExists(column.Id))
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
            ViewData["DataTableId"] = new SelectList(_context.DataTables, "Id", "Name", column.DataTableId);
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Name", column.PropertyId);
            ViewData["EntityId"] = new SelectList(_context.Entities, "Id", "Name");
            return View(column);
        }

        // GET: Columns/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            { 
                return NotFound();
            }

            var column = await _context.Columns
                .Include(c => c.DataTable)
                .Include(c => c.Property)
                 .Include(c => c.Entity)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (column == null)
            {
                return NotFound();
            }

            return View(column);
        }

        // POST: Columns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var column = await _context.Columns.SingleOrDefaultAsync(m => m.Id == id);
            _context.Columns.Remove(column);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ColumnExists(string id)
        {
            return _context.Columns.Any(e => e.Id == id);
        }
        public IActionResult GetProperties(string entityId)
        {
            var properties = _context.Properties.Where(p => p.EntityId == entityId).Select(p => new { Id = p.Id, Name = p.Name }).ToList();
;           

            return Ok(properties);
        }
    }
}
