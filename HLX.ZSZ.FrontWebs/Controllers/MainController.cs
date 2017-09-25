using HLX.ZSZ.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HLX.ZSZ.FrontWebs.Controllers
{
    public class MainController : Controller
    {
        public ICityService cityService { get; set; }
        // GET: Main
        public ActionResult Index()
        {
            cityService.AddNew("北京");
            return Content("ok");
            
        }
    }
}