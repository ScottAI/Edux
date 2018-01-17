using Edux.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.ViewComponents
{
    public class DataTableComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public DataTableComponent(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(Models.Component component)
        {
            var viewName = component.View ?? "Default";
            var dtId = component.ParameterValues.FirstOrDefault(f => f.Parameter.Name == "DataTable").Value;
            var datatable = await _context.DataTables.Include(e => e.Columns).ThenInclude(e => e.Property).ThenInclude(pv => pv.DataSourceProperties).FirstOrDefaultAsync(e => e.Id == dtId);
            ViewBag.DataTable = datatable;
            var CreateButtonText = component.ParameterValues.FirstOrDefault(f => f.Parameter.Name == "CreateButtonText")?.Value;
            ViewBag.CreateButtonText = CreateButtonText;
            var CreateButtonHref = component.ParameterValues.FirstOrDefault(f => f.Parameter.Name == "CreateButtonHref")?.Value;
            ViewBag.CreateButtonHref = CreateButtonHref;
            var ReturnUrl = component.ParameterValues.FirstOrDefault(f => f.Parameter.Name == "ReturnUrl")?.Value;
            ViewBag.ReturnUrl = ReturnUrl;
            var EditButtonText = component.ParameterValues.FirstOrDefault(f => f.Parameter.Name == "EditButtonText")?.Value;
            ViewBag.EditButtonText = EditButtonText;
            var EditButtonHref = component.ParameterValues.FirstOrDefault(f => f.Parameter.Name == "EditButtonHref")?.Value;
            ViewBag.EditButtonHref = EditButtonHref;
            var DeleteButtonText = component.ParameterValues.FirstOrDefault(f => f.Parameter.Name == "DeleteButtonText")?.Value;
            ViewBag.DeleteButtonText = DeleteButtonText;
            var DeleteButtonHref = component.ParameterValues.FirstOrDefault(f => f.Parameter.Name == "DeleteButtonHref")?.Value;
            ViewBag.DeleteButtonHref = DeleteButtonHref;
            var actionsMenuId = component.ParameterValues.FirstOrDefault(f => f.Parameter.Name == "ActionsMenu")?.Value;
            if (!string.IsNullOrEmpty(actionsMenuId))
            {
                var ActionMenu = await _context.Menus.Include(m => m.MenuItems).ThenInclude(i => i.ParentMenuItem).FirstOrDefaultAsync(f => f.Id == actionsMenuId);
                ViewBag.ActionsMenu = ActionMenu;
            }

            var toolbarsMenuId = component.ParameterValues.FirstOrDefault(f => f.Parameter.Name == "ToolbarsMenu")?.Value;
            if (!string.IsNullOrEmpty(toolbarsMenuId))
            {
                var ToolbarMenu = await _context.Menus.Include(m => m.MenuItems).ThenInclude(i => i.ParentMenuItem).FirstOrDefaultAsync(f => f.Id == toolbarsMenuId);
                ViewBag.ToolbarsMenu = ToolbarMenu;
            }






                    var entityId = datatable.EntityId;
                    var values = _context.EntityRows
                        .Where(r => r.EntityId == entityId && (datatable.Columns.Any(a => a.FilterOperator != Models.FilterOperator.None) ? datatable.Columns.Join(r.Values.Keys.ToList(), o => o.PropertyId, i => i, (o, i) => new { column = o, propertyId = i, values = r.Values })
                        .Where(w => datatable.Columns.Any(a => a.FilterOperator != Models.FilterOperator.None)
                        ? (w.column.FilterOperator == Models.FilterOperator.Equals ? w.values.GetValueOrDefault(w.propertyId) == w.column.FilterValue :
                        (w.column.FilterOperator == Models.FilterOperator.NotEquals ? w.values.GetValueOrDefault(w.propertyId) != w.column.FilterValue :
                        (w.column.FilterOperator == Models.FilterOperator.Contains ? w.values.GetValueOrDefault(w.column.PropertyId).Contains(w.column.FilterValue) :
                        (w.column.FilterOperator == Models.FilterOperator.DoesNotContain ? !(w.values.GetValueOrDefault(w.column.PropertyId).Contains(w.column.FilterValue)) :
                        (w.column.FilterOperator == Models.FilterOperator.In ? (w.column.FilterValue.Contains(w.values.GetValueOrDefault(w.column.PropertyId))) :
                        (w.column.FilterOperator == Models.FilterOperator.NotIn ? !(w.column.FilterValue.Contains(w.values.GetValueOrDefault(w.column.PropertyId))) :
                        (w.column.FilterOperator == Models.FilterOperator.GreaterThan ? (w.values.GetValueOrDefault(w.column.PropertyId).CompareTo(w.column.FilterValue) < 0) :
                        (w.column.FilterOperator == Models.FilterOperator.GreaterThanOrEquals ? (w.values.GetValueOrDefault(w.column.PropertyId).CompareTo(w.column.FilterValue) <= 0) :
                        (w.column.FilterOperator == Models.FilterOperator.LessThan ? (w.values.GetValueOrDefault(w.column.PropertyId).CompareTo(w.column.FilterValue) > 0) :
                        (w.column.FilterOperator == Models.FilterOperator.LessThanOrEquals ? (w.values.GetValueOrDefault(w.column.PropertyId).CompareTo(w.column.FilterValue) >= 0) :
                        false)))))))))) :
                        true).Select(j => j.propertyId).Count() == datatable.Columns.Where(h => h.FilterOperator != Models.FilterOperator.None).Count() : true))
                        .OrderBy(o => o.RowId).Select(e => e).Distinct().Take(datatable.Top).ToList();


                    /*var values = (from pv in _context.PropertyValues
                         .Include(i => i.Entity).Include(i => i.Property)
                         .ThenInclude(t => t.DataSourceProperty)
                         .ThenInclude(v => v.PropertyValues)
                                  join c in _context.Columns.Where(cl => cl.DataTableId == dtId)
                                  on pv.PropertyId equals c.PropertyId 
                                  where pv.EntityId == entityId && (from v2 in _context.PropertyValues where v2.PropertyId == pv.PropertyId && c.FilterOperator == Models.FilterOperator.Equals
                            ? v2.Value == c.FilterValue : (c.FilterOperator == Models.FilterOperator.Contains
                                ? v2.Value.Contains(c.FilterValue) : (c.FilterOperator == Models.FilterOperator.NotEquals
                                    ? v2.Value != c.FilterValue : (c.FilterOperator == Models.FilterOperator.DoesNotContain
                                        ? !v2.Value.Contains(c.FilterValue) : (c.FilterOperator == Models.FilterOperator.In
                                            ? c.FilterValue.Contains(v2.Value) : (c.FilterOperator == Models.FilterOperator.NotIn
                                                ? !c.FilterValue.Contains(v2.Value) : (c.FilterOperator == Models.FilterOperator.GreaterThan
                                                    ? v2.Value.CompareTo(c.FilterValue) < 0 : (c.FilterOperator == Models.FilterOperator.GreaterThanOrEquals
                                                        ? v2.Value.CompareTo(c.FilterValue) <= 0 : (c.FilterOperator == Models.FilterOperator.LessThan
                                                            ? v2.Value.CompareTo(c.FilterValue) > 0 : (c.FilterOperator == Models.FilterOperator.LessThanOrEquals
                                                                ? v2.Value.CompareTo(c.FilterValue) >= 0 : false
                                                                )
                                                            )
                                                        )
                                                    )
                                                )
                                            )
                                        )
                                    )
                                ) select v2.RowId).Contains(pv.RowId)
                                  orderby pv.RowId
                                  select pv).Take(datatable.Top).ToList();

                    */

                    /*var values = _context.PropertyValues
                         .Include(i => i.Entity).Include(i => i.Property)
                         .ThenInclude(t => t.DataSourceProperty)
                         .ThenInclude(v => v.PropertyValues)
                         // veri tablosunun kullandığı varlığın değerlerine ulaş
                         .Where(pv => pv.EntityId == entityId
                             // eğer veri tablosunun sütunları filtre içeriyorsa bu filtreleri uygula
                             && (_context.PropertyValues.Where(pv2 => pv2.EntityId == entityId && pv.RowId == pv2.RowId
                             && (datatable.Columns.Any(c => c.FilterOperator != Models.FilterOperator.None)
                             // sütunların özellikleri ile değerlerin özelliklerini joinle, değeri olan her bir sütun filtre operatörü "eşittir" ise
                             ? (datatable.Columns.FirstOrDefault(c => c.PropertyId == pv2.PropertyId).FilterOperator == Models.FilterOperator.Equals
                             // bu özelliğin değerini filtre değeri ile karşılaştır
                             ? (pv2.Value == datatable.Columns.FirstOrDefault(c => c.PropertyId == pv2.PropertyId).FilterValue)
                             // filtre operatörü "eşittir" değil "contains" ise
                             : (datatable.Columns.FirstOrDefault(c => c.PropertyId == pv2.PropertyId).FilterOperator == Models.FilterOperator.Contains
                             // bu özelliğin değeri filtre değerini içeriyor mu diye bak
                             ? (pv2.Value.Contains(datatable.Columns.FirstOrDefault(c => c.PropertyId == pv2.PropertyId).FilterValue))
                             // filtre operatörü "does not contain" ise
                             : ((datatable.Columns.FirstOrDefault(c => c.PropertyId == pv2.PropertyId).FilterOperator == Models.FilterOperator.DoesNotContain
                             // özellik değeri filtre değerini içermiyor mu diye kontrol et
                             ? (!pv2.Value.Contains(datatable.Columns.FirstOrDefault(c => c.PropertyId == pv2.PropertyId).FilterValue))
                             // filtre operatörü "less than" ise
                             : ((datatable.Columns.FirstOrDefault(c => c.PropertyId == pv2.PropertyId).FilterOperator == Models.FilterOperator.LessThan
                             // özellik değeri filtre değerinden küçük mü diye kontrol et
                             ? (pv2.Value.CompareTo(datatable.Columns.FirstOrDefault(c => c.PropertyId == pv2.PropertyId).FilterValue) > 0)
                             // filtre operatörü "less than or equals" ise
                             : ((datatable.Columns.FirstOrDefault(c => c.PropertyId == pv2.PropertyId).FilterOperator == Models.FilterOperator.LessThanOrEquals
                             // özellik değeri filtre değerinden küçük mü veya eşit mi diye kontrol et
                             ? (pv2.Value.CompareTo(datatable.Columns.FirstOrDefault(c => c.PropertyId == pv2.PropertyId).FilterValue) >= 0)
                             // filtre operatörü "greater than" ise
                             : ((datatable.Columns.FirstOrDefault(c => c.PropertyId == pv2.PropertyId).FilterOperator == Models.FilterOperator.GreaterThan
                             // özellik değeri filtre değerinden büyük mü diye kontrol et
                             ? (pv2.Value.CompareTo(datatable.Columns.FirstOrDefault(c => c.PropertyId == pv2.PropertyId).FilterValue) < 0)
                             // filtre operatörü "greater than or equals" ise
                             : ((datatable.Columns.FirstOrDefault(c => c.PropertyId == pv2.PropertyId).FilterOperator == Models.FilterOperator.GreaterThanOrEquals
                             // özellik değeri filtre değeriden büyük veya eşit mi diye kontrol et
                             ? (pv2.Value.CompareTo(datatable.Columns.FirstOrDefault(c => c.PropertyId == pv2.PropertyId).FilterValue) <= 0)
                             // filtre operatörü başka bir şeyse false uygula
                             : false))))))))))))
                             // sütunlar filtre içermiyorsa tüm kayıtları getir
                             : true)).Select(s=>s.RowId).Contains(pv.RowId))).OrderBy(r => r.RowId).Take(datatable.Top).ToList();
                    //var filterRows = (from v in _context.PropertyValues
                    //                  join c in _context.Columns on v.PropertyId equals c.PropertyId
                    //                  where v.EntityId == entityId && c.DataTableId == dtId && c.FilterOperator == Models.FilterOperator.Equals && v.Value == c.FilterValue
                    //                  select v.Value).ToList();
                    //var values = _context.PropertyValues.Include(i => i.Entity).Include(i => i.Property)
                    //     .ThenInclude(t => t.DataSourceProperty)
                    //     .ThenInclude(v => v.PropertyValues).Where(pv => pv.EntityId == entityId && filterRows.Contains(pv.Value)).OrderBy(o => o.RowId).Take(datatable.Top).ToList();
                    */
                    ViewBag.Values = values;

                    return await Task.FromResult(View(viewName, component));
                }
            }
        }
    

