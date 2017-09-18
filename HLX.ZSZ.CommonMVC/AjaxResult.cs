using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLX.ZSZ.CommonMVC
{

   /// <summary>
   /// 所有Ajax 都返回这个类型的对象
   /// </summary>
 public    class AjaxResult
    {

        public string Status { get; set; }
        public string ErrorMsg { get; set; }
        public object Data { get; set; }
    }
}
