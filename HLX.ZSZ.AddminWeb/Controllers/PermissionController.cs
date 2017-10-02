using HLX.ZSZ.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HLX.ZSZ.AddminWeb.Controllers
{
    public class PermissionController : Controller
    {
        // GET: Permission
        public IPermissionService PermissionService { get; set; }
        // GET: Permission
        public ActionResult List()
        {
            var perms = PermissionService.GetAll();

            return View(perms);
        }
        public ActionResult Delete2(long id)
        {
            PermissionService.MarkDeleted(id);
            return Json(new AjaxResult { Status = "ok", Data = "", ErrorMsg = "" });

        }
    }
}