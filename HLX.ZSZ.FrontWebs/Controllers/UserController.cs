using HLX.ZSZ.CommonMVC;
using HLX.ZSZ.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HLX.ZSZ.FrontWebs.Controllers
{
    public class UserController : Controller
    {
        public IUserService userService { get; set; }
        public ISettingService settingService { get; set; }

        // GET: User
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]

        public ActionResult ForgotPassword(string phoneNum,string verifyCode) {
            string serverVerifyCode = (string)TempData["verifyCode"];
            if (serverVerifyCode!=verifyCode)
            {
                return Json(new AjaxResult
                {
                    Status = "error",
                    ErrorMsg = "验证码错误"
                });
            }
            var user = userService.GetByPhoneNum(phoneNum);
            if (user==null )
            {
                return Json(new AjaxResult
                {
                    Status = "error",
                    ErrorMsg = "没有这个手机"
                });
            }
            string appKey = settingService.GetValue("短信平台AppKey");
            string userName = settingService.GetValue("短信平台UserName");
            string tempId = settingService.GetValue("短信平台模板Id");//摸版需要改为换密码的东西

            //发送短信
            string smsCode = new Random().Next(1000, 9999).ToString();
            TempData["smsCode"] = smsCode;
            HLXSMSSender hLXSMSSender = new HLXSMSSender();
            hLXSMSSender.AppKey = "4643878b073baa968bd870";
            hLXSMSSender.UserName = "hlxForNext";
            var senderResult = hLXSMSSender.SendSMS(tempId, smsCode, phoneNum);
            if (senderResult.code == 0)
            {
                Session["ForgotPasswordPhoneNum"] = phoneNum;//防止最后电话改变
                TempData["smsCode"] = smsCode;
                return Json(new AjaxResult { Status = "ok" });
            }
            else
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = senderResult.msg });
            }
           

        }



        [HttpGet]
        public ActionResult ForgotPassword2()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword2(string smsCode)
        {
            string serverSmsCode = (string)TempData["SmsCode"];
            if (smsCode != serverSmsCode)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "短信验证码错误" });
            }
            else
            {
                //告诉第3步“短信验证码验证通过”，防止恶意用户跳过ForgotPassword2直接重置密码
                TempData["ForgotPassword2_OK"] = true;
                return Json(new AjaxResult
                {
                    Status = "ok"
                });
            }
        }

        [HttpGet]
        public ActionResult ForgotPassword3()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword3(string password)
        {
            //防止恶意用户跳过ForgotPassword2直接重置密码
            bool? is2_OK = (bool?)TempData["ForgotPassword2_OK"];
            if (is2_OK != true)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "您没有通过短信验证码的验证" });
            }

            //需要重置密码的手机号
            string phoneNum = (string)TempData["ForgotPasswordPhoneNum"];
            var user = userService.GetByPhoneNum(phoneNum);
            userService.UpdatePwd(user.Id, password);
            return Json(new AjaxResult { Status = "ok" });
        }

        [HttpGet]
        public ActionResult ForgotPassword4()
        {
            return View();
        }

    }
}