using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KW.Application.Params;
using KW.Application.DTO;

namespace KW.Application
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User Get(int id);
        User GetByUserName(string userName);
        UserRoleDTO GetUserRole(int id);
        bool isExistUserName(string userName);
        ////void AssertIdNotAlreadyExist(string userName);
        ////void AssertIdNotAlreadyExistUpdate(int id, string userName);
        ////User Disable(int id);
        ////User ChangeStatus(int id);
        ////User Enable(int id);
        ////User ChangeLanguage(int id, string lang);
        ////User ChangePassword(int id, string oldPassword, string newPassword);
        ////User Delete(int id);
        ////void SetNewUserRole(User user, Role role);
        ////void RemoveUserRole(User user, Role role, UserRole olduserRole);
        ////User UpdateWithRole(int id, string userName, bool status, int? employeeId, bool v, string expiryDate, int roleId);
        ////User Update(int id, string userName, bool status, int? employeeId, bool withTransaction, string expiryDate = "");
        ////User Update(int id, string userName, string password, bool status, int? employeeId, bool withTransaction, string expiryDate = "");
        ////User Create(string userName, string password, bool status, int? employeeId, bool withTransaction, string expiryDate = "");
        ////User Create(string userName, string password, bool status, bool withTransaction, string expiryDate = "");
    }
}
