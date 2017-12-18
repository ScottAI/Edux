using Edux.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.ViewComponents
{
    public class DataTableAddRemoveComponent : ViewComponent
    {

        private readonly ApplicationDbContext _context;
        public DataTableAddRemoveComponent(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(Models.Component component)
        {
            var viewName = component.View ?? "Default";
            var dtId = component.ParameterValues.FirstOrDefault(f => f.Parameter.Name == "DataTable").Value;
            var datatable = await _context.DataTables.Include(e => e.Columns).ThenInclude(e => e.Property).ThenInclude(pv => pv.PropertyValues).FirstOrDefaultAsync(e => e.Id == dtId);
            ViewBag.DataTable = datatable;
            var CreateButtonText = component.ParameterValues.FirstOrDefault(f => f.Parameter.Name == "CreateButtonText")?.Value;
            ViewBag.CreateButtonText = CreateButtonText;
            var CreateButtonHref = component.ParameterValues.FirstOrDefault(f => f.Parameter.Name == "CreateButtonHref")?.Value;
            ViewBag.CreateButtonHref = CreateButtonHref;
            var EditButtonText = component.ParameterValues.FirstOrDefault(f => f.Parameter.Name == "EditButtonText")?.Value;
            ViewBag.EditButtonText = EditButtonText;
            var EditButtonHref = component.ParameterValues.FirstOrDefault(f => f.Parameter.Name == "EditButtonHref")?.Value;
            ViewBag.EditButtonHref = EditButtonHref;
            var DeleteButtonText = component.ParameterValues.FirstOrDefault(f => f.Parameter.Name == "DeleteButtonText")?.Value;
            ViewBag.DeleteButtonText = DeleteButtonText;
            var DeleteButtonHref = component.ParameterValues.FirstOrDefault(f => f.Parameter.Name == "DeleteButtonHref")?.Value;
            ViewBag.DeleteButtonHref = DeleteButtonHref;
            var AddButtonText = component.ParameterValues.FirstOrDefault(f => f.Parameter.Name == "AddButtonText")?.Value;
            ViewBag.AddButtonText = AddButtonText;
            var AddButtonHref = component.ParameterValues.FirstOrDefault(f => f.Parameter.Name == "AddButtonHref")?.Value;
            ViewBag.AddButtonHref = AddButtonHref;
            var RemoveButtonText = component.ParameterValues.FirstOrDefault(f => f.Parameter.Name == "RemoveButtonText")?.Value;
            ViewBag.RemoveButtonText = RemoveButtonText;
            var RemoveButtonHref = component.ParameterValues.FirstOrDefault(f => f.Parameter.Name == "RemoveButtonHref")?.Value;
            ViewBag.RemoveButtonHref = RemoveButtonHref;
            
            


            var entityId = datatable.EntityId;
            var values = _context.PropertyValues.Include(i => i.Entity).Include(i => i.Property).ThenInclude(t => t.DataSourceProperty).ThenInclude(v => v.PropertyValues).Where(x => x.EntityId == entityId && _context.PropertyValues.Where(
                  p => (datatable.Columns.Any(c => c.FilterOperator != Models.FilterOperator.None) ? (datatable.Columns.FirstOrDefault(c => c.PropertyId == p.PropertyId).FilterOperator == Models.FilterOperator.Equals ?
                      (p.Value == string.Format(datatable.Columns.FirstOrDefault(c => c.PropertyId == p.PropertyId).FilterValue, "37174a5c-6df2-4dda-b2af-0f4194aa5a9f")) : (datatable.Columns.FirstOrDefault(c => c.PropertyId == p.PropertyId).FilterOperator == Models.FilterOperator.Contains ? (p.Value.Contains(string.Format(datatable.Columns.FirstOrDefault(c => c.PropertyId == p.PropertyId).FilterValue, "37174a5c-6df2-4dda-b2af-0f4194aa5a9f"))) : ((datatable.Columns.FirstOrDefault(c => c.PropertyId == p.PropertyId).FilterOperator == Models.FilterOperator.DoesNotContain ? (!p.Value.Contains(string.Format(datatable.Columns.FirstOrDefault(c => c.PropertyId == p.PropertyId).FilterValue, "37174a5c-6df2-4dda-b2af-0f4194aa5a9f"))) : ((datatable.Columns.FirstOrDefault(c => c.PropertyId == p.PropertyId).FilterOperator == Models.FilterOperator.LessThan ? (p.Value.CompareTo(string.Format(datatable.Columns.FirstOrDefault(c => c.PropertyId == p.PropertyId).FilterValue, "37174a5c-6df2-4dda-b2af-0f4194aa5a9f")) > 0) : ((datatable.Columns.FirstOrDefault(c => c.PropertyId == p.PropertyId).FilterOperator == Models.FilterOperator.LessThanOrEquals ? (p.Value.CompareTo(string.Format(datatable.Columns.FirstOrDefault(c => c.PropertyId == p.PropertyId).FilterValue,"37174a5c-6df2-4dda-b2af-0f4194aa5a9f")) >= 0) : ((datatable.Columns.FirstOrDefault(c => c.PropertyId == p.PropertyId).FilterOperator == Models.FilterOperator.GreaterThan ? (p.Value.CompareTo(string.Format(datatable.Columns.FirstOrDefault(c => c.PropertyId == p.PropertyId).FilterValue, "37174a5c-6df2-4dda-b2af-0f4194aa5a9f")) < 0) : ((datatable.Columns.FirstOrDefault(c => c.PropertyId == p.PropertyId).FilterOperator == Models.FilterOperator.GreaterThanOrEquals ? (p.Value.CompareTo(string.Format(datatable.Columns.FirstOrDefault(c => c.PropertyId == p.PropertyId).FilterValue, "37174a5c-6df2-4dda-b2af-0f4194aa5a9f")) <= 0) : false)))))))))))) : true)).Any(f => f.RowId == x.RowId)).OrderBy(r => r.RowId).Take(datatable.Top).ToList();

            ViewBag.Values = values;
            return await Task.FromResult(View(viewName, component));


        }

        }
}
