using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1
{
    public class JsonNetActionFilter : IActionFilter
    {
        //Action 执行完成
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //把  filterContext.Result  从JsonResult 换成JsonNetResult
            if (filterContext.Result is JsonResult &&!(filterContext.Result is JsonNetResult))
            {
                JsonResult jsonResult = (JsonResult)filterContext.Result;
                JsonNetResult jsonNetResult = new JsonNetResult();
      
                jsonNetResult.ContentEncoding = jsonResult.ContentEncoding;
                jsonNetResult.ContentType = jsonResult.ContentType;
                jsonNetResult.Data = jsonResult.Data;
                jsonNetResult.JsonRequestBehavior = jsonResult.JsonRequestBehavior;
                jsonNetResult.MaxJsonLength = jsonResult.MaxJsonLength;
                jsonNetResult.RecursionLimit = jsonResult.RecursionLimit;

                filterContext.Result = jsonNetResult;
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
        }
    }
}