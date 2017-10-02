using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HLX.ZSZ.AddminWeb.Models
{
    public class RoleAddModel
    {

        public string Name { get; set; }

        public long [] PermissionIds { get; set; }

    }
}