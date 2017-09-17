using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestIservice;

namespace WebApplication1.Controllers
{
    public class DefaultController : Controller
    {

        public IUserService UserService { get; set; }//利用AutoFac
        // GET: Default
        public ActionResult Index()
        {
            // bool f = UserService.CheckLogin("你好", "123");
            // TestHelper.Test();
            // return Content(f.ToString());
            return Content("");
        }

        [HttpPost]
        public  ActionResult Test()
        {
            Dog dog = new Dog() { BirthDate=DateTime.Now,Id=1,Name="LMH"};
            return  new JsonNetResult() { Data=dog};


        }
        [HttpGet]
        public ActionResult TestJson()
        {
            return View();
        }
    }
}