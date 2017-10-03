using HLX.ZSZ.AddminWeb.Models;
using HLX.ZSZ.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace HLX.ZSZ.AddminWeb.Controllers
{
    public class RoleController : Controller
    {
        public IRoleService roleService { get; set; }
        public  IPermissionService permisionService { get; set; }
        // GET: Role
        public ActionResult List()
        {
            var roles= roleService.GetAll();
            return View(roles);
        }

        public ActionResult Delete(long id)
        {

            roleService.MarkDeleted(id);
            return Json(new AjaxResult{ Status = "ok" });
        }

        [HttpGet]
        public ActionResult Add() {
            var prem = permisionService.GetAll();//所有可用项
            return View(prem);


        }
        [HttpPost]
        public ActionResult Add(RoleAddModel model)
        {
          long roleId=  roleService.AddNew(model.Name);
            permisionService.AddPermIds(roleId, model.PermissionIds);
            return Json(new AjaxResult { Status = "ok" });


        }
        [HttpGet]
        public ActionResult Edit(long id) {
          var role=  roleService.GetById(id);
            var rolePermissions = permisionService.GetByRoleId(id);//权限
            var allPerms = permisionService.GetAll();
            RoleEditGetModel roleEditGetModel = new RoleEditGetModel();
            roleEditGetModel.Role = role;
            roleEditGetModel.RolePerms = rolePermissions;
            roleEditGetModel.AllPerms = allPerms;
            return View(roleEditGetModel);
        }
        [HttpPost]
        public  ActionResult Edit(RoleEditModel model )
        {
            roleService.Update(model.Id, model.Name);
            roleService.UpdateRoleIds(model.Id, model.PermissionIds);
            return Json(new AjaxResult { Status = "ok" });
        }

        public ActionResult BatchDelete(long [] selectdIds)
        {
            foreach (var id in selectdIds)
            {
                roleService.MarkDeleted(id);
            }
            return Json(new AjaxResult { Status = "ok" });

        }

    }
}