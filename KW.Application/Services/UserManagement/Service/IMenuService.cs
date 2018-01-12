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
    public interface IMenuService
    {
        IQueryable<Menu> GetMenu();
        Menu GetMenu(int Id);
        IList<Menu> GetChild(int parentId);
        IList<MenuDTO> GetMenuByGeneralAccess(bool IsGeneralAccess);
        IList<MenuDTO> GetByRole(int RoleID);
        IList<MenuAccessLiteWithChildDTO> GetByListControllerAndActionMenu(List<MenuAccessLiteParameters> model);
        IList<MenuAccessRoleDTO> GetAllDifferentiateByRole(int RoleID);
        IList<MenuDTO> GetAvailableByRole(int RoleID);
        IList<MenuAccessRoleDTO> GetRolePermission(int RoleID);
        IList<MenuAccessRoleDTO> BuildTreeNew(IList<MenuAccessRoleDTO> source);
        void AddChildrenNew(MenuAccessRoleDTO node, List<MenuAccessRoleDTO> source);
        void AddChildren(MenuAccessLiteWithChildDTO node, List<Menu> source);
    }
}
