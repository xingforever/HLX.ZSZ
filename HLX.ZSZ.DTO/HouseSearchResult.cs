using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLX.ZSZ.DTO
{
    public class HouseSearchResult
    {
        public HouseDTO[] result { get; set; }//当前页的数据
        public long totalCount { get; set; }//搜索的结果总条数
    }

}
