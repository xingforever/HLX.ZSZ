﻿using HLX.ZSZ.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HLX.ZSZ.AdminWeb.Models
{
    public class HouseAddViewModel
    {
        public RegionDTO[] regions { get; set; }
        public IdNameDTO[] roomTypes { get; set; }
        public IdNameDTO[] statuses { get; set; }
        public IdNameDTO[] decorateStatuses { get; set; }
        public IdNameDTO[] types { get; set; }
        public AttachmentDTO[] attachments { get; set; }
    }
}