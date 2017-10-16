using HLX.ZSZ.AdminWeb.App_Start;
using HLX.ZSZ.AdminWeb.Models;
using HLX.ZSZ.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HLX.ZSZ.AdminWeb.Controllers
{
    public class PermissionController : Controller
    {
        // GET: Permission
        public IPermissionService PermissionService { get; set; }
        // GET: Permission
        [CheckPermission("Permission.List")]
        public ActionResult List()
        {
            var perms = PermissionService.GetAll();

            return View(perms);
        }
        [CheckPermission("Permission.Delete")]
        public ActionResult Delete2(long id)
        {
            PermissionService.MarkDeleted(id);
            return Json(new AjaxResult { Status = "ok", Data = "", ErrorMsg = "" });

        }
        [CheckPermission("Permission.Delete")]
        public  ActionResult GetDelete(long id)
        {

            PermissionService.MarkDeleted(id);
            //return RedirectToAction("List");//删除之后刷新
            return RedirectToAction(nameof(List));
        }
        [CheckPermission("Permission.Add")]
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [CheckPermission("Permission.Add")]
        [HttpPost]
        public  ActionResult Add(PermissioAddNewModel model)
        {
            var name = Request["name"];
            var description = Request["description"];
            PermissionService.AddPermission(name, description);
            //权限项目名检查
            return Json(new AjaxResult { Status = "ok" });
        }

        [CheckPermission("Permission.Edit")]
        [HttpGet]
        public ActionResult Edit(long id) {

            var perm = PermissionService.GetById(id);
            return View(perm);



        }
        [CheckPermission("Permission.Edit")]
        [HttpPost]
        public ActionResult Edit()
        {
            var id = Convert.ToInt64( Request["id"]);
            var name = Request["name"];
            var description = Request["description"];         
            var perm = PermissionService.GetById(id);
            if (perm!=null)
            {
                PermissionService.UpdatePermission(id, name, description);
                return Json(new AjaxResult { Status = "ok" });
            }
             return Json(new AjaxResult { Status = "no" });

        }



    }
}