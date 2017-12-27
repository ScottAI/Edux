using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.Models
{
    public class UserGroupRole
    {
        public string UserGroupId { get; set; }
        public UserGroup UserGroup { get; set; }

        public string RoleId { get; set; }
        public Role Role { get; set; }
    }
}
