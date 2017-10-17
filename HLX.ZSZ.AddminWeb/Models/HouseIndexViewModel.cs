﻿using HLX.ZSZ.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HLX.ZSZ.AdminWeb.Models
{
    public class HouseIndexViewModel
    {
        public HouseDTO House { get; set; }
        public HousePicDTO[] Pics { get; set; }
        public AttachmentDTO[] Attachments { get; set; }
    }
}