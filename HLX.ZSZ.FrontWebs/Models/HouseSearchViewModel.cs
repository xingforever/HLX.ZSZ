using HLX.ZSZ.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HLX.ZSZ.FrontWebs.Models
{
    public class HouseSearchViewModel
    {
        public RegionDTO[] regions { get; set; }
        public HouseDTO[] houses { get; set; }
    }
}