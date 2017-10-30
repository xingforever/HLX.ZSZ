using HLX.ZSZ.CommonMVC;
using HLX.ZSZ.DTO;
using HLX.ZSZ.FrontWebs.Models;
using HLX.ZSZ.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HLX.ZSZ.FrontWebs.Controllers
{
    public class HouseController : Controller
    {
         public  IHouseService houseService { get; set; }

        public IAttachmentService attService { get; set; }
        public ICityService cityService { get; set; }
        public IRegionService regionService { get; set; }

        public IHouseAppointmentService appService { get; set; }

        // GET: House
        public ActionResult Index(long id)
        {
           // var house = houseService.GetById(id);
            //if (house==null)
            //{
            //    return View("error",(object)"不存在房源信息");

            //}
            //var pics = houseService.GetPics(id);
            //var attachments = attService.GetAttachments(id);
            //HouseIndexViewModel model = new HouseIndexViewModel();
            //model.House = house;
            //model.Pics = pics;
            //model.Attachments = attachments;
            //return View(model);
            string cacheKey = "HouseIndex_" + id;
            //先尝试去缓存中找

            HouseIndexViewModel model = (HouseIndexViewModel)HttpContext.Cache[cacheKey];
            if (model == null)//缓存中没有找到
            {
                var house = houseService.GetById(id);
                if (house == null)
                {
                    return View("Error", (object)"不存在的房源id");
                }
                var pics = houseService.GetPics(id);
                var attachments = attService.GetAttachments(id);

                model = new HouseIndexViewModel();
                model.House = house;
                model.Pics = pics;
                model.Attachments = attachments;
                //存入缓存
                HttpContext.Cache.Insert("key", model, null, DateTime.Now.AddSeconds(40), System.Web.Caching.Cache.NoSlidingExpiration);
            }
            return View(model);
        }

        public ActionResult Test()
        {
            return View();
        }
        public  ActionResult AA()
        {
            HouseSearchOptions opt = new HouseSearchOptions();
            opt.CityId = 1;
            opt.TypeId = 11;
            opt.StartMonthRent = 300;
            opt.OrderByType = HouseSearchOrderByType.AreaDesc;
            opt.Keywords = "楼";
            opt.PageSize = 10;
            opt.CurrentIndex = 1;
            var result = houseService.Search(opt);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("总结过条数：" + result.totalCount);
            foreach (var h in result.result)
            {
                sb.AppendLine(h.CommunityName + "," + h.Area + "," + h.MonthRent);
            }
            return Content(sb.ToString());
        }

        public ActionResult Search(long typeId, string keyWords, string monthRent,
            string orderByType, long? regionId)
        {
            //获得当前用户城市Id
            long cityId = FrontUtils.GetCityId(HttpContext);

            //获取城市下所有区域
            var regions = regionService.GetAll(cityId);
            HouseSearchViewModel model = new HouseSearchViewModel();
            model.regions = regions;

            HouseSearchOptions searchOpt = new HouseSearchOptions();
            searchOpt.CityId = cityId;
            searchOpt.CurrentIndex = 1;

            //解析月租部分
            int? startMonthRent;
            int? endMonthRent;
            //ref/out
            ParseMonthRent(monthRent, out startMonthRent, out endMonthRent);
            searchOpt.EndMonthRent = endMonthRent;
            searchOpt.StartMonthRent = startMonthRent;

            searchOpt.Keywords = keyWords;
            switch (orderByType)
            {
                case "MonthRentAsc":
                    searchOpt.OrderByType = HouseSearchOrderByType.MonthRentAsc;
                    break;
                case "MonthRentDesc":
                    searchOpt.OrderByType = HouseSearchOrderByType.MonthRentDesc;
                    break;
                case "AreaAsc":
                    searchOpt.OrderByType = HouseSearchOrderByType.AreaAsc;
                    break;
                case "AreaDesc":
                    searchOpt.OrderByType = HouseSearchOrderByType.AreaDesc;
                    break;
            }
            searchOpt.PageSize = 10;
            searchOpt.RegionId = regionId;
            searchOpt.TypeId = typeId;

            //开始搜索
            var searchResult = houseService.Search(searchOpt);
            model.houses = searchResult.result;
            //当前用户城市Id

            return View(model);
        }
        public ActionResult Search2(long typeId, string keyWords, string monthRent,
            string orderByType, long? regionId)
        {
            long cityId = FrontUtils.GetCityId(HttpContext);
            var regions = regionService.GetAll(cityId);
            return View(regions);
        }
        private void ParseMonthRent(string value,
          out int? startMonthRent, out int? endMonthRent)
        {
            //如果没有传递MonthRent参数，说明“不限制房租”
            if (string.IsNullOrEmpty(value))
            {
                startMonthRent = null;
                endMonthRent = null;
                return;
            }

            string[] values = value.Split('-');
            string strStart = values[0];
            string strEnd = values[1];
            if (strStart == "*")
            {
                startMonthRent = null;//不设限
            }
            else
            {
                startMonthRent = Convert.ToInt32(strStart);
            }
            if (strEnd == "*")
            {
                endMonthRent = null;//不设限
            }
            else
            {
                endMonthRent = Convert.ToInt32(strEnd);
            }
        }

        public  ActionResult MakeAppointment(HouseMakeAppointmentModel model)
        {
            if (!ModelState.IsValid)
            {
                string msg = MVCHelper.GetValidMsg(ModelState);
                return Json(new AjaxResult { Status = "error", ErrorMsg = msg });
            }
            long? userId = FrontUtils.GetUserId(HttpContext);
            appService.AddNew(userId, model.Name,
             model.PhoneNum, model.HouseId, model.VisitDate);
            return Json(new AjaxResult
            {
                Status = "ok"
            });
        }
    }
}