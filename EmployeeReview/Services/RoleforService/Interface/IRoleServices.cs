using EmployeeReview.Models;
using Microsoft.Data.SqlClient;

namespace EmployeeReview.Services.RoleService.Interface
{
    public interface IRoleServices
    {
        void createRole(string RoleName);
        bool RoleAlreadyExist(string RoleName);
        List<UserRole> GetRoleIdByUserId(String UserId);
        string RoleNameById(string RoleId);

        void AssignUserRole(string UserId, string RoleId);

        bool CheckRoleAlreadyAssignToUser(string UserId, string RoleId);

        List<Role> GetAllRole();

        List<GetAllUserWithRole> GetAllUserWithRole();
        GetAllUserWithRole GetUserRoleById(string RoleId, string UserId);

        void UpdateUserRoleAssign(EditUserRole edit);
        public void DeleteUserRole(string RoleId, string UserId);
        






    }
}
