using HLX.ZSZ.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.Service.Entities;

namespace ZSZ.Service
{
    public class CityService : ICityService
    {
        /// <summary>
        /// 新增城市
        /// </summary>
        /// <param name="cityName">城市名称</param>
        /// <returns>城市ID</returns>
        public long AddNew(string cityName)
        {
            using (ZSZDbContext ctx = new ZSZDbContext() ) {
                BaseService<CityEntity> bs = new BaseService<CityEntity>(ctx);
                //Any效率高
             bool exists=   bs.GetAll().Any(c => c.Name == cityName);
                if (exists)
                {
                    throw new ArgumentException("城市已经存在");
                }
                CityEntity city = new CityEntity();
                city.Name = cityName;
                ctx.Cities.Add(city);
                ctx.SaveChanges();
                return city.Id;




            }
        }
    }
}
