using Edux.Data;
using Edux.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.ViewComponents
{
    public class FormComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public FormComponent(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(Models.Component component)
        {
            var viewName = component.View ?? "Default";
            var formId = component.ParameterValues.FirstOrDefault(f => f.Parameter.Name == "Form")?.Value;
           
            var initialValues = component.ParameterValues.FirstOrDefault(f => f.Parameter.Name == "InitialValues")?.Value;
            if (initialValues != null) { 
                ViewBag.InitialValues = JsonConvert.DeserializeObject<Dictionary<string, string>>(initialValues);
            }
            if (String.IsNullOrEmpty(Request.Query["returnUrl"].ToString())) {
                ViewBag.ReturnUrl = Request.Path;
            } else
            {
                ViewBag.ReturnUrl = Request.Query["returnUrl"].ToString();
            }
            string mode = Request.Query["mode"].ToString().ToLowerInvariant();
            if (String.IsNullOrEmpty(mode))
            {
                mode = "create";
            }
            ViewBag.Mode = mode;
            var rowId = Request.Query["id"].ToString();
            ViewBag.RowId = rowId;
            var frm = await _context.Forms.Include(c=>c.Components).ThenInclude(pv=>pv.ParameterValues).ThenInclude(p=>p.Parameter).ThenInclude(ct=>ct.ComponentType).Include(fs=>fs.Fieldsets).Include(t=>t.Tabs).ThenInclude(f => f.Fields).ThenInclude(ff => ff.Property).ThenInclude(d=>d.DataSourceProperty).ThenInclude(ds=>ds.DataSourceProperty).Include("Fields.Property.PropertyValues").SingleOrDefaultAsync(f => f.Id == formId);
            if (frm == null)
            {
                throw new Exception($"\"{formId}\" id'li bir form bulunamadı.");
            }
            ViewBag.Form = frm;
            var formEntityId = ((Form)ViewBag.Form).EntityId;
            if ((mode == "edit" || mode == "delete") && !String.IsNullOrEmpty(rowId))
            {
                ViewBag.EntityRow = _context.EntityRows.FirstOrDefault(f => f.EntityId == formEntityId && f.RowId.ToString() == rowId);               
            }
            IDictionary<String, IList<EntityRow>> DataSourcePropertyValues = new Dictionary<string, IList<EntityRow>>();
            foreach (var item in ((Form)ViewBag.Form).Fields)
            {
                if (item.Property.DataSourceProperty != null && !String.IsNullOrEmpty(item.Property.DataSourceProperty.Id))
                {
                    var entityId = item.Property.DataSourceProperty.EntityId;
                    var pvs = _context.EntityRows.Where(pv => pv.Entity.Id == entityId).OrderBy(r => r.RowId).ToList();
                    if (!DataSourcePropertyValues.ContainsKey(item.Property.DataSourceProperty.Id)) { 
                        DataSourcePropertyValues.Add(item.Property.DataSourceProperty.Id, pvs);
                    }
                }
            }
            ViewBag.DataSourcePropertyValues = DataSourcePropertyValues;
            //IDictionary<String, IList<EntityRow>> DataSourcePropertyValues2 = new Dictionary<string, IList<EntityRow>>();
            //foreach (var item in ((Form)ViewBag.Form).Fields)
            //{
            //    if (item.Property.DataSourceProperties2 != null && !String.IsNullOrEmpty(item.Property.DataSourceProperty.Id))
            //    {
            //        var entityId = item.Property.DataSourceProperty.EntityId;
            //        var pvs = _context.EntityRows.Where(pv => pv.Entity.Id == entityId).OrderBy(r => r.RowId).ToList();
            //        if (!DataSourcePropertyValues2.ContainsKey(item.Property.DataSourceProperty.Id))
            //        {
            //            DataSourcePropertyValues2.Add(item.Property.DataSourceProperty.Id, pvs);
            //        }
            //    }
            //}
            //ViewBag.DataSourcePropertyValues2 = DataSourcePropertyValues2;
            //IDictionary<String, IList<EntityRow>> DataSourcePropertyValues3 = new Dictionary<string, IList<EntityRow>>();
            //foreach (var item in ((Form)ViewBag.Form).Fields)
            //{
            //    if (item.Property.DataSourceProperties3 != null && !String.IsNullOrEmpty(item.Property.DataSourceProperty.Id))
            //    {
            //        var entityId = item.Property.DataSourceProperty.EntityId;
            //        var pvs = _context.EntityRows.Where(pv => pv.Entity.Id == entityId).OrderBy(r => r.RowId).ToList();
            //        if (!DataSourcePropertyValues3.ContainsKey(item.Property.DataSourceProperty.Id))
            //        {
            //            DataSourcePropertyValues3.Add(item.Property.DataSourceProperty.Id, pvs);
            //        }
            //    }
            //}
            //ViewBag.DataSourcePropertyValues3 = DataSourcePropertyValues3;
            return await Task.FromResult(View(viewName, component));
        }
    }
}
