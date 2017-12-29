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
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using System.IO;
using Microsoft.CodeAnalysis.Emit;
using System.Reflection;
using System.Runtime.Loader;

namespace Edux.Controllers
{
    [Authorize]
    public class FunctionsController : ControllerBase
    {
        public FunctionsController(ApplicationDbContext context):base(context)
        {

            string codeToCompile = @"string message = ""Mesaj""";

            message = "Mesaj1";
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(codeToCompile);

            string assemblyName = Path.GetRandomFileName();
            MetadataReference[] references = new MetadataReference[]
            {
                MetadataReference.CreateFromFile(typeof(object).GetTypeInfo().Assembly.Location)
            };

        
            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: new[] { syntaxTree },
                references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using (var ms = new MemoryStream())
            {
                EmitResult result = compilation.Emit(ms);

                if (!result.Success)
                {
                   
                    IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error);

                    foreach (Diagnostic diagnostic in failures)
                    {
                        Console.Error.WriteLine("\t{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
                    }
                }
                else
                {
                    
                    ms.Seek(0, SeekOrigin.Begin);

                    Assembly assembly = AssemblyLoadContext.Default.LoadFromStream(ms);
                    var type = assembly.GetType("RoslynCompileSample.Writer");
                    var instance = assembly.CreateInstance("RoslynCompileSample.Writer");
                    var meth = type.GetMember("Write").First() as MethodInfo;
                    meth.Invoke(instance, new[] { "joel" });
                }
            }
        }



        public async Task<IActionResult> Run()
        {
            return View();
        }




        // GET: Functions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Functions.ToListAsync());
        }

        // GET: Functions/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var function = await _context.Functions
                .SingleOrDefaultAsync(m => m.Id == id);
            if (function == null)
            {
                return NotFound();
            }

            return View(function);
        }

        // GET: Functions/Create
        public IActionResult Create()
        {
            var function = new Function();
            return View(function);
        }

        // POST: Functions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Code,IsAnonymous,AllowedRoles,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Function function)
        {
            if (ModelState.IsValid)
            {
                function.CreatedBy = User.Identity.Name;
                function.UpdateDate = DateTime.Now;
                function.UpdatedBy = User.Identity.Name;
                function.CreateDate = DateTime.Now;
                _context.Add(function);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(function);
        }

        // GET: Functions/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var function = await _context.Functions.SingleOrDefaultAsync(m => m.Id == id);
            if (function == null)
            {
                return NotFound();
            }
            return View(function);
        }

        // POST: Functions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Code,IsAnonymous,AllowedRoles,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Function function)
        {
            if (id != function.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    function.UpdateDate = DateTime.Now;
                    function.UpdatedBy = User.Identity.Name;
                    _context.Update(function);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FunctionExists(function.Id))
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
            return View(function);
        }

        // GET: Functions/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var function = await _context.Functions
                .SingleOrDefaultAsync(m => m.Id == id);
            if (function == null)
            {
                return NotFound();
            }

            return View(function);
        }

        // POST: Functions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var function = await _context.Functions.SingleOrDefaultAsync(m => m.Id == id);
            _context.Functions.Remove(function);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool FunctionExists(string id)
        {
            return _context.Functions.Any(e => e.Id == id);
        }


       
    }
}
