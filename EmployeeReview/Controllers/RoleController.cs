using AspNetCoreHero.ToastNotification.Abstractions;
using EmployeeReview.Models;
using EmployeeReview.Services.Account.Interface;
using EmployeeReview.Services.RoleService.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeReview.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly INotyfService _notyf;
        private readonly IRoleServices _roleServices;
        private readonly IAccountInfoServices _accountInfo;
        public RoleController(INotyfService notyf,IRoleServices roleServices, IAccountInfoServices accountInfo)
        {
            _notyf = notyf;
            _roleServices = roleServices;
            _accountInfo = accountInfo;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles ="SupperAdmin")]
        public IActionResult CreateRole(string roleName)
        {
            if (roleName != null)
            {
                //First check the role Already exist.
                bool check=_roleServices.RoleAlreadyExist(roleName);
                if (check)
                {
                    _notyf.Error("Role Already Exist");
                    return RedirectToAction("Index");
                }
                //Create Role
                _roleServices.createRole(roleName);
                _notyf.Success("Create Role Successfully");
                return RedirectToAction("Index");
            }
            _notyf.Error("Role is Empty");
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "SupperAdmin")]
        public IActionResult AssignRoleView()
        {
            //Get All the Users.
            var allUser = _accountInfo.GetAllUser();
            if (allUser.Count > 0)
            {
                var dropdown = allUser.Select(x => new SelectListItem()
                {
                    Value = x.UserId.ToString(),
                    Text = x.UserName


                }).ToList();
                ViewData["allUser"] = dropdown;
            }

            //Get All the Role.
            var allRole = _roleServices.GetAllRole();
            if (allRole.Count > 0)
            {
                var roleDropDown = allRole.Select(x => new SelectListItem()
                {
                    Value = x.RoleId.ToString(),
                    Text = x.RoleName
                }).ToList();
                ViewData["allRole"] = roleDropDown;
            }
            return View();
        }


        //Assign the role to the user. 
        [Authorize(Roles = "SupperAdmin")]
        public IActionResult RoleAssign(string UserId, string RoleId)
        {
            try
            {
                //First check the RoleId and  UserId is not null.
                if (UserId != null && RoleId != null)
                {

                    //Check if the role aready assign.
                    bool sameRoleCheck = _roleServices.CheckRoleAlreadyAssignToUser(UserId, RoleId);


                    if (sameRoleCheck)
                    {
                        _notyf.Error("The Role Id is already assign to the user");
                        return RedirectToAction("AssignRoleView");
                    }

                    //Assign the role to the user. 
                    _roleServices.AssignUserRole(UserId, RoleId);

                    _notyf.Success($"Role is assign to the user Id {UserId}", 5);
                    return RedirectToAction("AssignRoleView");


                }

                _notyf.Error("User id or Role Name is Empty", 5);
                return RedirectToAction("AssignRoleView");
            }
            catch (Exception ex)
            {
                _notyf.Error(ex.ToString(), 5);
                return RedirectToAction("AssignRoleView");
            }


        }


        //Get all User that is assign role. 
        [Authorize(Roles = "SupperAdmin")]
        public IActionResult GetRole()
        {
            try
            {
                var getAllRoleWithUser = _roleServices.GetAllUserWithRole();
                return View(getAllRoleWithUser);
            }
            catch (Exception ex)
            {
                _notyf.Error(ex.ToString());
                return View();
            }


        }

        public IActionResult EditRole(string RoleId, string UserId)
        {
            try
            {
                //Get the user role information for edit.
                var user = _roleServices.GetUserRoleById(RoleId, UserId);

                //Get All the Role.
                var allRole = _roleServices.GetAllRole();
                if (allRole.Count > 0)
                {
                    var roleDropDown = allRole.Select(x => new SelectListItem()
                    {
                        Value = x.RoleId.ToString(),
                        Text = x.RoleName
                    }).ToList();
                    ViewData["allRole"] = roleDropDown;

                }

                return View(user);
            }
            catch (Exception ex)
            {
                _notyf.Error(ex.ToString());
                return RedirectToAction("GetRole");
            }

        }

        //Edit Role of the User
        [Authorize(Roles = "SupperAdmin")]
        public IActionResult Edit(EditUserRole edit)
        {
            try
            {


                //Check the same role assign to the user.
                var sameRoleCheck = _roleServices.CheckRoleAlreadyAssignToUser(edit.UserId.ToString(), edit.RoleId.ToString());


                if (sameRoleCheck)
                {
                    _notyf.Error("The Role Id is already assign to the user");
                    return RedirectToAction("GetRole");
                }

                //Update the Assign role of the user.

                _notyf.Success("Successfully update the role");
                return RedirectToAction("GetRole");

            }
            catch (Exception ex)
            {
                _notyf.Error(ex.ToString());
                return RedirectToAction("GetRole");
            }
        }

        //Delte the User Role 
        [Authorize(Roles = "SupperAdmin")]
        public IActionResult DeleteUserRole(string UserId, string RoleId)
        {
            try
            {
                _roleServices.DeleteUserRole(UserId, RoleId);

                _notyf.Success("Successfully Delete the role of the user.");
                return RedirectToAction("GetRole");

            }
            catch (Exception ex)
            {
                _notyf.Error(ex.ToString());
                return RedirectToAction("GetRole");
            }
        }


    }
}
