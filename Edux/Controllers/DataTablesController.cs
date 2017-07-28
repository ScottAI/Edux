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
    public class DataTablesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DataTablesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: DataTables
        public async Task<IActionResult> Index()
        {
            return View(await _context.DataTables.ToListAsync());
        }

        // GET: DataTables/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataTable = await _context.DataTables
                .SingleOrDefaultAsync(m => m.Id == id);
            if (dataTable == null)
            {
                return NotFound();
            }

            return View(dataTable);
        }

        // GET: DataTables/Create
        public IActionResult Create()
        {
            var datatable = new DataTable();

            return View(datatable);
        }

        // POST: DataTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,DisplayName,Top,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] DataTable dataTable)
        {
            if (ModelState.IsValid)
            {
                dataTable.UpdateDate = DateTime.Now;
                dataTable.CreateDate = DateTime.Now;
                dataTable.CreatedBy = User.Identity.Name;
                dataTable.UpdatedBy = User.Identity.Name;
                _context.Add(dataTable);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(dataTable);
        }

        // GET: DataTables/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataTable = await _context.DataTables.SingleOrDefaultAsync(m => m.Id == id);
            if (dataTable == null)
            {
                return NotFound();
            }
            return View(dataTable);
        }

        // POST: DataTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,DisplayName,Top,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] DataTable dataTable)
        {
            if (id != dataTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dataTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DataTableExists(dataTable.Id))
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
            return View(dataTable);
        }

        // GET: DataTables/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataTable = await _context.DataTables
                .SingleOrDefaultAsync(m => m.Id == id);
            if (dataTable == null)
            {
                return NotFound();
            }

            return View(dataTable);
        }

        // POST: DataTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var dataTable = await _context.DataTables.SingleOrDefaultAsync(m => m.Id == id);
            _context.DataTables.Remove(dataTable);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool DataTableExists(string id)
        {
            return _context.DataTables.Any(e => e.Id == id);
        }
    }
}
