using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.Models
{
    public class EntityRow : BaseEntity
    {
        public EntityRow() : base()
        {
            CreateDate = DateTime.Now;
            CreatedBy = "username";
            UpdateDate = DateTime.Now;
            UpdatedBy = "username";
        }
        public long RowId { get; set; }
        public string EntityId { get; set; }
        [ForeignKey("EntityId")]
        public Entity Entity { get; set; }
        public string RowValue { get; set; }
        [NotMapped]
        public Dictionary<string, string> Values
        {
            get { return JsonConvert.DeserializeObject<Dictionary<string, string>>(this.RowValue); }
            set { RowValue = JsonConvert.SerializeObject(value); }
        }
    }
}
