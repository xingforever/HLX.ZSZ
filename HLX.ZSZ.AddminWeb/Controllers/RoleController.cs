using HLX.ZSZ.AdminWeb.App_Start;
using HLX.ZSZ.AdminWeb.Models;
using HLX.ZSZ.CommonMVC;
using HLX.ZSZ.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace HLX.ZSZ.AdminWeb.Controllers
{
    public class RoleController : Controller
    {
        public IRoleService roleService { get; set; }
        public  IPermissionService permisionService { get; set; }
        // GET: Role
        [CheckPermission("Role.List")]
        public ActionResult List()
        {
            var roles= roleService.GetAll();
            return View(roles);
        }
        [CheckPermission("Role.Delete")]
        public ActionResult Delete(long id)
        {

            roleService.MarkDeleted(id);
            return Json(new AjaxResult{ Status = "ok" });
        }
        [CheckPermission("Role.Add")]
        [HttpGet]
        public ActionResult Add() {
            var prem = permisionService.GetAll();//所有可用项
            return View(prem);


        }
        [CheckPermission("Role.Add")]
        [HttpPost]
        public ActionResult Add(RoleAddModel model)
        {
            //检查验证是否通过
            if(!ModelState.IsValid)
            {
                return Json(new AjaxResult { Status = "error",ErrorMsg= MVCHelper.GetValidMsg(ModelState )});
            }
          long roleId=  roleService.AddNew(model.Name);
            permisionService.AddPermIds(roleId, model.PermissionIds);
            return Json(new AjaxResult { Status = "ok" });


        }
        [CheckPermission("Role.Edit")]
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
        [CheckPermission("Role.Edit")]
        [HttpPost]
        public  ActionResult Edit(RoleEditModel model )
        {
            roleService.Update(model.Id, model.Name);
            roleService.UpdateRoleIds(model.Id, model.PermissionIds);
            return Json(new AjaxResult { Status = "ok" });
        }
        [CheckPermission("Role.Delete")]
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