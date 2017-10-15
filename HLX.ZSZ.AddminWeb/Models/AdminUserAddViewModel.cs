using HLX.ZSZ.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HLX.ZSZ.AdminWeb.Models
{
    public class AdminUserAddViewModel
    {
        public CityDTO[] Cities { get; set; }
        public RoleDTO[] Roles { get; set; }

    }
}