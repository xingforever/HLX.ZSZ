using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLX.ZSZ.DTO
{
    public class RegionDTO : BaseDTO
    {
        public String Name { get; set; }
        public long CityId { get; set; }
        public String CityName { get; set; }
    }

}
