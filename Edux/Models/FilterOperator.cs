using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.Models
{
    public enum FilterOperator
    {
        None = 0,
        Equals = 1,
        NotEquals = 2,
        Contains = 3,
        DoesNotContain = 4,
        LessThan = 5,
        LessThanOrEquals = 6,
        GreaterThan = 7,
        GreaterThanOrEquals = 8
    }
}
