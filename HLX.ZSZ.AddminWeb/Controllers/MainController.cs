using CaptchaGen;
using HLX.ZSZ.AdminWeb.Models;
using HLX.ZSZ.Common;
using HLX.ZSZ.CommonMVC;
using HLX.ZSZ.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HLX.ZSZ.AdminWeb.Controllers
{
    public class MainController : Controller
    {

        public  IUserService userService { get; set; }
        // GET: Main
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public  ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = MVCHelper.GetValidMsg(ModelState) });
            }
            //if (model.VerifyCode!=(string)Session["verifyCode"])
            //{
            //    var df = (string)Session["verifyCode"];
            //    return Json(new AjaxResult { Status = "error", ErrorMsg = "验证码错误" });
                    
            //}
            if (model.VerifyCode != (string)TempData["verifyCode"])
            {
               
                return Json(new AjaxResult { Status = "error", ErrorMsg = "验证码错误" });

            }
            bool result =userService.CheckLogin(model.PhoneNum, model.Password);
            if (result)
            {
                //用户登录Id
                Session["LoginUserId"] = userService.GetByPhoneNum(model.PhoneNum).Id;
                return Json(new AjaxResult { Status = "ok" });
            }
            else
            {
                return Json(new AjaxResult { Status = "error",ErrorMsg="用户名或者密码错误" });
            }

        }

        public  ActionResult CreateVerifyCode()
        {

            string verifyCode = CommonHelper.CreateVerifyCode(4);
            Session["verifyCode"] = verifyCode;
            TempData["verifyCode"] = verifyCode;
            //Session["verifyCode"] = verifyCode;
            /*
            using (MemoryStream ms = ImageFactory.GenerateImage(verifyCode, 60, 100, 20, 6))
            {
                return File(ms, "image/jpeg");
            }*/
            System.IO.MemoryStream ms = ImageFactory.GenerateImage(verifyCode, 60, 100, 20, 6);
            return File(ms, "image/jpeg");
        }
    }
}