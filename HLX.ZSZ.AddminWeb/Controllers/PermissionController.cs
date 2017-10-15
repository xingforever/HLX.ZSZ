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
        public  ActionResult GetDelete(long id)
        {

            PermissionService.MarkDeleted(id);
            //return RedirectToAction("List");//删除之后刷新
            return RedirectToAction(nameof(List));
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public  ActionResult Add(PermissioAddNewModel model)
        {
            var name = Request["name"];
            var description = Request["description"];
            PermissionService.AddPermission(name, description);
            //权限项目名检查
            return Json(new AjaxResult { Status = "ok" });
        }


        [HttpGet]
        public ActionResult Edit(long id) {

            var perm = PermissionService.GetById(id);
            return View(perm);



        }
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