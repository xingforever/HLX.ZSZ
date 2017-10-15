﻿using HLX.ZSZ.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HLX.ZSZ.AdminWeb.Models
{
    public class RoleEditGetModel
    {
        public RoleDTO Role { get; set; }
        public PermissionDTO[] RolePerms { get; set; }
        public PermissionDTO[] AllPerms { get; set; }
    }
}