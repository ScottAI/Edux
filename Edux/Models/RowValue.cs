using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.Models
{
    public class RowValue
    {
        public RowValue()
        {
            Values = new Dictionary<string, string>();
        }
        public Dictionary<string, string> Values { get; set; }
    }
}
