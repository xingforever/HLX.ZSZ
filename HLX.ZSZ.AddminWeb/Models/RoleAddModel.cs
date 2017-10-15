﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HLX.ZSZ.AdminWeb.Models
{
    public class RoleAddModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public long [] PermissionIds { get; set; }

    }
}