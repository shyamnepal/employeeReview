using AspNetCoreHero.ToastNotification.Abstractions;
using EmployeeReview.Models;
using EmployeeReview.Services.RoleService.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EmployeeReview.Services.RoleService.Implementation
{
    public class RoleServices : IRoleServices
    {
        private readonly ReviewSystemContext _reviewSystemContext;
        private readonly IHttpContextAccessor _Httpcontext;
        private readonly INotyfService _notyfService;

        public RoleServices(ReviewSystemContext reviewSystemContext, IHttpContextAccessor httpContext, INotyfService notyfService)
        {
            _reviewSystemContext = reviewSystemContext;
            _Httpcontext = httpContext;
            _notyfService = notyfService;
            
        }

        //Assign User Role 
        public void AssignUserRole(string UserId, string RoleId)
        {

            // Get userName of the Login user.
            var UserName = _Httpcontext.HttpContext.User.Identity.Name;

            //  Assign the role to the User.
            var userIdParam = new SqlParameter("@UserId", UserId);
            var roleIdParam = new SqlParameter("@RoleId", RoleId);
            var createdByParam = new SqlParameter("@CreatedBy", UserName);
            var createdDateParam = new SqlParameter("@CreatedDate", DateTime.Now);

            _reviewSystemContext.UserRoles.FromSqlRaw("EXEC AssignRole @UserId, @RoleId, @CreatedBy, @CreatedDate",
                userIdParam, roleIdParam, createdByParam, createdDateParam).ToList();
        }

        public bool CheckRoleAlreadyAssignToUser(string UserId, string RoleId)
        {
           

                var roleIdParam = new SqlParameter("@RoleId", RoleId);
                var userIdParam = new SqlParameter("@UserId", UserId);
                var sameRoleCheck = _reviewSystemContext.UserRoles.FromSqlRaw("EXEC spAlreadyRoleAssign @UserId, @RoleId", userIdParam, roleIdParam).ToList();
                if (sameRoleCheck != null && sameRoleCheck.Count > 0)
                {
                    return true;
                }
                return false;
            
        }

        //Create Role 
        public void createRole(string RoleName)
        {
            try
            {
                var UserName = _Httpcontext.HttpContext.User.Identity.Name;

                //Insert Role in the database.
                var roleNameParam = new SqlParameter("@RoleName", RoleName);
                var createdDateParam = new SqlParameter("@CreatedDate", DateTime.Now);
                var createdByParam = new SqlParameter("@CreatedBy", UserName);

                // Execute the stored procedure using ExecuteSqlRaw method
                _reviewSystemContext.Database.ExecuteSqlRaw("EXEC CreateRole @RoleName, @CreatedDate, @CreatedBy",
                    roleNameParam, createdDateParam, createdByParam);

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public List<Role> GetAllRole()
        {
            try
            {
                var result = _reviewSystemContext.Roles.FromSqlRaw("EXEC getAllRole").ToList();
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }

        public List<UserRole> GetRoleIdByUserId(string UserId)
        {
            try
            {
                var UserIdParam = new SqlParameter("@UserId", UserId);
                var getRoleId = _reviewSystemContext.UserRoles.FromSqlRaw("EXEC getRoleIdByUserId @UserId", UserIdParam).ToList() ;
                if(GetRoleIdByUserId!=null || getRoleId.Count > 0)
                {
                    return getRoleId;
                }
                return null;

                
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Check the Role Exist.
        public bool RoleAlreadyExist(string RoleName)
        {
            try
            {
                //Get the role by roleName
                var roleNameParam = new SqlParameter("@RoleName", RoleName);
             var role=   _reviewSystemContext.Roles.FromSqlRaw("EXEC RoleAlreadyExist @RoleName", roleNameParam).ToList();
                if( role.Count > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string RoleNameById(string RoleId)
        {
            var roleIdParam = new SqlParameter("@RoleId", RoleId);
            var roleName = _reviewSystemContext.Roles.FromSqlRaw("EXEC GetRoleNameById @RoleId", roleIdParam).ToList();
            if(roleName!=null || roleName.Count> 0)
            {
                return roleName.FirstOrDefault().RoleName;
            }
            return null;
        }

        //Get all the user with role. 
        public List<GetAllUserWithRole> GetAllUserWithRole()
        {
            var allUserRole = _reviewSystemContext.GetAllUserWithRoles.FromSqlRaw("EXEC GetAllUserWithRole").ToList();
            if (allUserRole != null && allUserRole.Count > 0)
            {
                return allUserRole;
            }
            return null;
        }
        //Get User and their Role for Edit 
        public GetAllUserWithRole GetUserRoleById(string RoleId, string UserId)
        {
            var roleIdParam = new SqlParameter("@RoleId", RoleId);
            var userIdParam = new SqlParameter("@UserId", UserId);

            //Get the value by userid and role for edit
            var getUserRole = _reviewSystemContext.GetAllUserWithRoles.FromSqlRaw("EXEC GetUserWithRole @UserId, @RoleId", roleIdParam, userIdParam).ToList();
            if (getUserRole != null && getUserRole.Count > 0)
            {
                return getUserRole.FirstOrDefault();
            }
            return null;
        }

        public void UpdateUserRoleAssign(EditUserRole edit)
        {
            //Sql parameter.
            //var roleIdParam = new SqlParameter("@RoleId", edit.RoleId);
            var userIdParam = new SqlParameter("@UserId", edit.UserId);
            var OldRoleIdParam = new SqlParameter("@OldRoleId", edit.OldRoleId);

            var NewroleIdParam = new SqlParameter("@NewRoleId", edit.RoleId);
            _reviewSystemContext.Database.ExecuteSqlRaw(
                "EXEC EditUserRole @UserId, @OldRoleId, @NewRoleId", userIdParam, OldRoleIdParam, NewroleIdParam);
        }

        public void DeleteUserRole(string RoleId, string UserId)
        {
            _reviewSystemContext.Database.ExecuteSqlRaw("EXEC DeleteUserRole @UserId, @RoleId",
                new SqlParameter("UserId", UserId),
                new SqlParameter("@RoleId", RoleId));
        }
    }
}
