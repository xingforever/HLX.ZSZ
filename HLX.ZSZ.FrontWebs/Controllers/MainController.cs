using HLX.ZSZ.Common;
using HLX.ZSZ.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaptchaGen;
using HLX.ZSZ.CommonMVC;
using HLX.ZSZ.FrontWebs.Models;

namespace HLX.ZSZ.FrontWebs.Controllers
{


    public class MainController : Controller
    {
        public ICityService cityService { get; set; }
        public ISettingService settingService { get; set; }
        public IUserService userService { get; set; }
        // GET: Main
        public ActionResult Index()
        {
            long cityId = FrontUtils.GetCityId(HttpContext);
            //当前城市的名字
            string cityName = cityService.GetById(cityId).Name;
            ViewBag.cityName = cityName;
            var cities = cityService.GetAll();
            return View(cities);
        }
        [HttpGet]
        public ActionResult Register() {
            
            return View();
        }

        public ActionResult CreateVerifyCode() {
            string verifyCode = CommonHelper.CreateVerifyCode(4);
            //验证码保存到TempData中最安全
            TempData["verifyCode"] = verifyCode;
            MemoryStream ms = ImageFactory.GenerateImage(verifyCode, 60, 100, 15, 6);
            return File(ms, "image/jpeg");
        }

        public  ActionResult SendSmsVerifyCode(string phoneNum,string verifyCode)
        {
            string serverVerifyCode = (string)TempData["verifyCode"];//取服务器中验证码
            if (serverVerifyCode!=verifyCode)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "图形验证码填写错误" });
            }
            string appKey = settingService.GetValue("短信平台AppKey");
            string userName = settingService.GetValue("短信平台UserName");
            string tempId = settingService.GetValue("短信平台模板Id");

            //发送短信
            string smsCode = new Random().Next(1000, 9999).ToString();
            TempData["smsCode"] = smsCode;
            HLXSMSSender hLXSMSSender = new HLXSMSSender();
            hLXSMSSender.AppKey = "4643878b073baa968bd870";
            hLXSMSSender.UserName = "hlxForNext";
           var senderResult= hLXSMSSender.SendSMS(tempId, smsCode, phoneNum);
            if (senderResult.code == 0) {
                TempData["RegphoneNum"] = phoneNum;//防止最后电话改变
                return Json(new AjaxResult { Status = "ok" });
            }
            else
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = senderResult.msg });
            }
        }
        [HttpPost]
        public ActionResult Register(UserRegModel model)
        {

            if (ModelState.IsValid==false)
            {
                return Json(new AjaxResult
                {
                    Status = "error",
                    ErrorMsg = MVCHelper.GetValidMsg(ModelState)
                });
            }
            //比较输入验证码与服务器的验证码是否一致
            string serverSmsCode = TempData["smsCode"].ToString();
            if (model.SmsCode!=serverSmsCode)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "短信验证码错误" });
            }
            //比较电话号码是否一致
            string serverPhoneNum = (string)TempData["RegphoneNum"];
            if (serverPhoneNum!=model.PhoneNum)
            {
                return Json(new AjaxResult
                {
                    Status = "error",
                    ErrorMsg = "注册的手机号和获取验证码手机号不同"

                });
            }


            //漏洞
            if (userService.GetByPhoneNum(model.PhoneNum)!=null)
            {
                return Json(new AjaxResult
                {
                    Status = "error",
                    ErrorMsg = "此手机号已经被注册"
                });
            }

            userService.AddNew(model.PhoneNum, model.PassWord);
            return Json(new AjaxResult { Status = "ok" });
        }

        [HttpGet]
        public  ActionResult Login()
        {

            return View();

        }

        [HttpPost]
        public ActionResult Login(UserLoginModel model) {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult
                {
                    Status = "error",
                    ErrorMsg = MVCHelper.GetValidMsg(ModelState)

                });
            }

            var user = userService.GetByPhoneNum(model.PhoneNum);
            if (user!=null )
            {
                if (userService.IsLocked(user.Id))
                {
                    TimeSpan? leftTimespan = TimeSpan.FromMinutes(30) - (DateTime.Now - user.LastLoginErrorDateTime);
                    return Json(new AjaxResult
                    {
                        Status = "error",
                        ErrorMsg = "账号被锁定,请" + (leftTimespan.Value.TotalMinutes + "分钟后,重试")


                    });
                } ;
            }
            
             bool isOk=userService.CheckLogin(model.PhoneNum, model.Password);
        

            if (isOk)
            {

                //登录成功后重置等录戳去信息
                userService.ResetLoginError(user.Id);

                Session["UserId"] = user.Id;
                Session["CityId"] = user.CityId;


                return Json(new AjaxResult
                {
                    Status = "ok"

                });
            }
            else
            {
                if (user!=null)
                {

                }
                else
                {
                    return Json(new AjaxResult
                    {
                        Data = "",
                        ErrorMsg = "手机号或者密码错误"

                    });
                }

            }


            return View();

        }
    }
}