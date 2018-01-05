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

        public async Task<IViewComponentResult> InvokeAsync(Models.Component component, string relatedId="")
        {
            var viewName = component.View ?? "Default";
            var dtId = component.ParameterValues.FirstOrDefault(f => f.Parameter.Name == "DataTable").Value;
            var datatable = await _context.DataTables.Include("Columns.Property.DataSourceProperty").Include(e => e.Columns).ThenInclude(e => e.Property).ThenInclude(pv => pv.PropertyValues).FirstOrDefaultAsync(e => e.Id == dtId);
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



            //var entityId = datatable.EntityId;
            //string pId = datatable.Columns.FirstOrDefault(c => c.PropertyId == p.PropertyId).Property.DataSourceProperty.PropertyValues.FirstOrDefault(v => v.RowId.ToString() == Request.Query["id"].ToString()).Value           
            //var values = _context.PropertyValues
            //    .Include(i => i.Entity).Include(i => i.Property)
            //    .ThenInclude(t => t.DataSourceProperty)
            //    .ThenInclude(v => v.PropertyValues)
            //    // veri tablosunun kullandığı varlığın değerlerine ulaş
            //    .Where(pv => pv.EntityId == entityId
            //        // eğer veri tablosunun sütunları filtre içeriyorsa bu filtreleri uygula
            //        && _context.PropertyValues.Where(pv2 => (datatable.Columns.Any(c => c.FilterOperator != Models.FilterOperator.None)
            //        // sütunların özellikleri ile değerlerin özelliklerini joinle, değeri olan her bir sütun filtre operatörü "eşittir" ise
            //        ? (datatable.Columns.FirstOrDefault(c => c.PropertyId == pv2.PropertyId).FilterOperator == Models.FilterOperator.Equals
            //        // bu özelliğin değerini filtre değeri ile karşılaştır
            //        ? (pv2.Value == string.Format(datatable.Columns.FirstOrDefault(c => c.PropertyId == pv2.PropertyId).FilterValue, relatedId))
            //        // filtre operatörü "eşittir" değil "contains" ise
            //        : (datatable.Columns.FirstOrDefault(c => c.PropertyId == pv2.PropertyId).FilterOperator == Models.FilterOperator.Contains
            //        // bu özelliğin değeri filtre değerini içeriyor mu diye bak
            //        ? (pv2.Value.Contains(string.Format(datatable.Columns.FirstOrDefault(c => c.PropertyId == pv2.PropertyId).FilterValue, relatedId)))
            //        // filtre operatörü "does not contain" ise
            //        : ((datatable.Columns.FirstOrDefault(c => c.PropertyId == pv2.PropertyId).FilterOperator == Models.FilterOperator.DoesNotContain
            //        // özellik değeri filtre değerini içermiyor mu diye kontrol et
            //        ? (!pv2.Value.Contains(string.Format(datatable.Columns.FirstOrDefault(c => c.PropertyId == pv2.PropertyId).FilterValue, relatedId)))
            //        // filtre operatörü "less than" ise
            //        : ((datatable.Columns.FirstOrDefault(c => c.PropertyId == pv2.PropertyId).FilterOperator == Models.FilterOperator.LessThan
            //        // özellik değeri filtre değerinden küçük mü diye kontrol et
            //        ? (pv2.Value.CompareTo(string.Format(datatable.Columns.FirstOrDefault(c => c.PropertyId == pv2.PropertyId).FilterValue, relatedId)) > 0)
            //        // filtre operatörü "less than or equals" ise
            //        : ((datatable.Columns.FirstOrDefault(c => c.PropertyId == pv2.PropertyId).FilterOperator == Models.FilterOperator.LessThanOrEquals
            //        // özellik değeri filtre değerinden küçük mü veya eşit mi diye kontrol et
            //        ? (pv2.Value.CompareTo(string.Format(datatable.Columns.FirstOrDefault(c => c.PropertyId == pv2.PropertyId).FilterValue, relatedId)) >= 0)
            //        // filtre operatörü "greater than" ise
            //        : ((datatable.Columns.FirstOrDefault(c => c.PropertyId == pv2.PropertyId).FilterOperator == Models.FilterOperator.GreaterThan
            //        // özellik değeri filtre değerinden büyük mü diye kontrol et
            //        ? (pv2.Value.CompareTo(string.Format(datatable.Columns.FirstOrDefault(c => c.PropertyId == pv2.PropertyId).FilterValue, relatedId)) < 0)
            //        // filtre operatörü "greater than or equals" ise
            //        : ((datatable.Columns.FirstOrDefault(c => c.PropertyId == pv2.PropertyId).FilterOperator == Models.FilterOperator.GreaterThanOrEquals
            //        // özellik değeri filtre değeriden büyük veya eşit mi diye kontrol et
            //        ? (pv2.Value.CompareTo(string.Format(datatable.Columns.FirstOrDefault(c => c.PropertyId == pv2.PropertyId).FilterValue, relatedId)) <= 0)
            //        // filtre operatörü başka bir şeyse false uygula
            //        : false))))))))))))
            //        // sütunlar filtre içermiyorsa tüm kayıtları getir
            //        : true)).Any(f => f.RowId == pv.RowId)).OrderBy(r => r.RowId).Take(datatable.Top).ToList();

            var entityId = datatable.EntityId;
            var values = _context.EntityRows
                .Where(r => r.EntityId == entityId && (datatable.Columns.Any(a => a.FilterOperator != Models.FilterOperator.None) ? datatable.Columns.Join(r.Values.Keys.ToList(), o => o.PropertyId, i => i, (o, i) => new { column = o, propertyId = i, values = r.Values })
                .Where(w => datatable.Columns.Any(a => a.FilterOperator != Models.FilterOperator.None)
                ? (w.column.FilterOperator == Models.FilterOperator.Equals ? w.values.GetValueOrDefault(w.propertyId) == string.Format(w.column.FilterValue,relatedId) :
                (w.column.FilterOperator == Models.FilterOperator.NotEquals ? w.values.GetValueOrDefault(w.propertyId) != string.Format(w.column.FilterValue) :
                (w.column.FilterOperator == Models.FilterOperator.Contains ? w.values.GetValueOrDefault(w.column.PropertyId).Contains(string.Format(w.column.FilterValue)) :
                (w.column.FilterOperator == Models.FilterOperator.DoesNotContain ? !(w.values.GetValueOrDefault(w.column.PropertyId).Contains(string.Format(w.column.FilterValue))) :
                (w.column.FilterOperator == Models.FilterOperator.In ? (w.column.FilterValue.Contains(w.values.GetValueOrDefault(string.Format(w.column.PropertyId)))) :
                (w.column.FilterOperator == Models.FilterOperator.NotIn ? !(w.column.FilterValue.Contains(w.values.GetValueOrDefault(string.Format(w.column.PropertyId)))) :
                (w.column.FilterOperator == Models.FilterOperator.GreaterThan ? (w.values.GetValueOrDefault(w.column.PropertyId).CompareTo(string.Format(w.column.FilterValue)) < 0) :
                (w.column.FilterOperator == Models.FilterOperator.GreaterThanOrEquals ? (w.values.GetValueOrDefault(w.column.PropertyId).CompareTo(string.Format(w.column.FilterValue)) <= 0) :
                (w.column.FilterOperator == Models.FilterOperator.LessThan ? (w.values.GetValueOrDefault(w.column.PropertyId).CompareTo(string.Format(w.column.FilterValue)) > 0) :
                (w.column.FilterOperator == Models.FilterOperator.LessThanOrEquals ? (w.values.GetValueOrDefault(w.column.PropertyId).CompareTo(string.Format(w.column.FilterValue)) >= 0) :
                false)))))))))) :
                true).Select(j => j.propertyId).Count() == datatable.Columns.Where(h => h.FilterOperator != Models.FilterOperator.None).Count() : true))
                .OrderBy(o => o.RowId).Select(e => e).Distinct().ToList();

            ViewBag.Values = values;
            return await Task.FromResult(View(viewName, component));


        }

        }
}
