using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HLX.ZSZ.FrontWebs
{
    public class FrontUtils
    {
        //获取当前登录ID
        public long? GetUserId(HttpContextBase ctx) {
            return (long?) ctx.Session["UserId"];
            
        }
        /// <summary>
        ///获取 用户的城市
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public long? GetCityId(HttpContextBase ctx){

            return (long?)ctx.Session["CityId"];




        }




    }
}