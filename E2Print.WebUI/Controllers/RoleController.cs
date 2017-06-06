using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E2Print.BL.Interfaces;
using E2Print.Domain.Entities;

namespace E2Print.WebUI.Controllers
{
    public class RoleController : Controller
    {
        IUserRole roleRepository;
        public RoleController(IUserRole repository)
        {
            this.roleRepository = repository;
        }

        [HttpPost]
        public JsonResult  RoleList()
        {
            try
            {
                List<UserRole> userRoles = roleRepository.GetAll();
                return Json(new { Result = "OK", Records = userRoles });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult GetRoleList()
        {
            try
            {
                var roles = roleRepository.GetAll().Select(c => new { DisplayText = c.RoleName, Value = c.RoleName });
                return Json(new { Result = "OK", Options = roles });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Create(UserRole userRole)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new
                    {
                        Result = "ERROR",
                        Message = "Form is not valid! " + "Please correct it and try again."
                    });
                }

                var newRole = roleRepository.Create(userRole);
                return Json(new { Result = "OK", Record = newRole });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Update(UserRole userRole)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new
                    {
                        Result = "ERROR",
                        Message = "Form is not valid! " + "Please correct it and try again."
                    });
                }

                roleRepository.Update(userRole);
                return Json(new { Result = "OK"});
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


        [HttpPost]
        public JsonResult Delete(UserRole role)
        {
            try
            {
                roleRepository.Delete(role.Id);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        public ActionResult ManageRole()
        {
            return View(roleRepository.GetAll());
        }

        public ActionResult ManageRoleUsers()
        {
            return View();
        }
    }
}