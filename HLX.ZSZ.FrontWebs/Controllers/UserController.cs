using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HLX.ZSZ.FrontWebs.Controllers
{
    public class UserController : Controller
    {

       
        // GET: User
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]

        public ActionResult ForgotPassword(string phone,string verifyCode) {


            return View();

        }
    }
}