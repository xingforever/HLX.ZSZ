using HLX.ZSZ.AdminWeb.Controllers;
using HLX.ZSZ.CommonMVC;
using HLX.ZSZ.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HLX.ZSZ.AdminWeb.App_Start
{
    /// <summary>
    /// 统一权限控制
    /// </summary>
    public class ZSZAuthorizeFilter : IAuthorizationFilter
    {
       // public IAdminUserService Service { get; set; }
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            //获取标注的信息 实例对象
           CheckPermissionAttribute[] permAtts= (CheckPermissionAttribute[]) filterContext.ActionDescriptor.GetCustomAttributes(typeof(CheckPermissionAttribute),false);
            //如果没有
            if (permAtts.Length <= 0)
            {
                return;//登录等这些不要求有用户登录 的功能
            }
            //获取用户ID
            long? userId = (long?)filterContext.HttpContext.Session["LoginUserId"];
            if (userId==null)
            {
                //// filterContext.HttpContext.Response.Write("没有登录");
                //filterContext.Result = new ContentResult() { Content="没有登录"};
                //return;
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    CommonMVC.AjaxResult ajaxResult = new CommonMVC.AjaxResult();
                    ajaxResult.Status = "redirect";
                    ajaxResult.Data = "/Main/Login";
                    ajaxResult.ErrorMsg = "没有登录";
                    filterContext.Result = new JsonNetResult { Data = ajaxResult };
                }
                else
                {
                    filterContext.Result = new RedirectResult("~/Main/Login");
                }
                return;
            }
            IAdminUserService userService =
              DependencyResolver.Current.GetService<IAdminUserService>(); 
            //检查是否有权限
            foreach (var permAtt in permAtts)
            {
                if (!userService.HasPermission((long)userId, permAtt.Permission))
                {
                    //只要有一个没有权限 则禁止访问  
                    filterContext.Result = new ContentResult() { Content = "没有"+permAtt.Permission+"这个权限" };
                    return;
                }
            }

            

        }
    }
}