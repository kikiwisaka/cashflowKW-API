using KW.Core;
using KW.Domain;
using System;
using System.Collections.Generic;
using KW.Application.DTO;
using KW.Application.Params;
using System.Linq;

namespace KW.Application
{
    public class MenuService : IMenuService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMenuRepository _menuRepository;
        public MenuService(IUnitOfWork unitOfWork, IMenuRepository menuRepository)
        {
            _unitOfWork = unitOfWork;
            _menuRepository = menuRepository;
        }
        public IQueryable<Menu> GetMenu()
        {
            return _menuRepository.GetMenu();
        }

        public Menu GetMenu(int Id)
        {
            return _menuRepository.Get(Id);
        }

        public IList<Menu> GetChild(int parentId)
        {
            return _menuRepository.GetChild(parentId);
        }

        public IList<MenuDTO> GetMenuByGeneralAccess(bool IsGeneralAccess)
        {
            var result = _menuRepository.GetMenu().Where(x => x.IsGeneralAccess == IsGeneralAccess).ToList();
            return MenuDTO.From(result);
        }

        public IList<MenuDTO> GetByRole(int RoleID)
        {
            var result = _menuRepository.GetMenu().Where(x => x.RoleAccessList.Any(y => y.Role.Id == RoleID)).ToList();
            return MenuDTO.From(result);
        }

        public IList<MenuAccessLiteWithChildDTO> GetByListControllerAndActionMenu(List<MenuAccessLiteParameters> model)
        {
            IList<Menu> menus = _menuRepository.GetMenu().ToList();
            IList<Menu> menusResult = menus
                                    .Where(x => model.Any(y => (y.ControllerName.Equals(x.ControllerName) && y.ActionName.Equals(x.ActionName))))
                                    .ToList();
            return MenuAccessLiteWithChildDTO.OrderedFrom(menusResult);
        }

        public IList<MenuAccessRoleDTO> GetAllDifferentiateByRole(int RoleID)
        {
            try
            {
                List<MenuAccessRoleDTO> menuList = new List<MenuAccessRoleDTO>();
                IList<Menu> menus = _menuRepository.GetMenu().Where(x => x.RoleAccessList.Any(y => y.Role.Id == RoleID)).ToList();
                if (menus != null && menus.Count > 0)
                {
                    menuList = MenuAccessRoleDTO.From(menus, true).ToList();
                }
                menus = _menuRepository.GetMenu().Where(x => !x.RoleAccessList.Any(y => y.Role.Id == RoleID)).ToList();
                if (menus != null && menus.Count > 0)
                {
                    menuList.AddRange(MenuAccessRoleDTO.From(menus, false).ToList());
                }

                var menuExceptMainMenu = menuList.Where(x => x.ControllerName != "Main").ToList();
                return menuExceptMainMenu;
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        public IList<MenuDTO> GetAvailableByRole(int RoleID)
        {
            try
            {
                IList<Menu> menus = _menuRepository.GetMenu().Where(x => x.RoleAccessList.Any(y => y.Role.Id != RoleID)).ToList();
                return MenuDTO.From(menus);
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        public IList<MenuAccessRoleDTO> GetRolePermission(int RoleID)
        {
            try
            {
                List<MenuAccessRoleDTO> menuList = new List<MenuAccessRoleDTO>();
                IList<Menu> menus = _menuRepository.GetMenu().Where(x => x.RoleAccessList.Any(y => y.Role.Id == RoleID)).ToList();

                if (menus != null && menus.Count > 0)
                {
                    menuList = MenuAccessRoleDTO.From(menus, true).ToList();
                }
                menus = _menuRepository.GetMenu().Where(x => !x.RoleAccessList.Any(y => y.Role.Id == RoleID)).ToList();
                if (menus != null && menus.Count > 0)
                {
                    menuList.AddRange(MenuAccessRoleDTO.From(menus, false).ToList());
                }
                var mn = BuildTreeNew(menuList);
                return mn.OrderBy(x => x.Id)
                        .ThenBy(x => x.Sequence)
                        .ThenByDescending(x => x.IsOnMenu)
                        .ThenBy(x => x.ControllerName)
                        .ThenBy(x => x.Name)
                        .ToList();
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        public IList<MenuAccessRoleDTO> BuildTreeNew(IList<MenuAccessRoleDTO> source)
        {
            try
            {
                var menuExceptMainMenu = source.Where(x => x.Name != "Main Menu").ToList();
                IList<MenuAccessRoleDTO> roots = menuExceptMainMenu.Where(x => x.ParentId < 1).ToList();

                IList<MenuAccessRoleDTO> rootsDTO = new List<MenuAccessRoleDTO>();
                if (roots.Count > 0)
                {
                    var nonroots = menuExceptMainMenu.Except(roots).ToList();
                    for (int i = 0; i < roots.Count; i++)
                    {
                        MenuAccessRoleDTO rootDTO = MenuAccessRoleDTO.From(roots[i]);
                        AddChildrenNew(rootDTO, nonroots);
                        rootsDTO.Add(rootDTO);
                    }
                }

                return rootsDTO;
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        public void AddChildrenNew(MenuAccessRoleDTO node, List<MenuAccessRoleDTO> source)
        {
            try
            {
                node.Childs = new List<MenuAccessRoleDTO>();
                if (source.Where(x => x.ParentId == node.Id).Count() > 0)
                {
                    IList<MenuAccessRoleDTO> mnChild = source.Where(x => x.ParentId == node.Id).ToList();
                    node.Childs = mnChild;
                    for (int i = 0; i < node.Childs.Count; i++)
                        AddChildrenNew(node.Childs[i], source);
                }
                else
                {
                    node.Childs = new List<MenuAccessRoleDTO>();
                }
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        public void AddChildren(MenuAccessLiteWithChildDTO node, List<Menu> source)
        {
            try
            {
                if (source.Where(x => x.Parent.Id == node.Id).Count() > 0)
                {
                    List<Menu> mnChild = source.Where(x => x.Parent.Id == node.Id).ToList();
                    IList<MenuAccessLiteWithChildDTO> childs = MenuAccessLiteWithChildDTO.From(mnChild);
                    node.Children = childs;
                    for (int i = 0; i < node.Children.Count; i++)
                        AddChildren((MenuAccessLiteWithChildDTO)node.Children[i], source);
                }
                else
                {
                    node.Children = new List<MenuAccessLiteWithChildDTO>();
                }
            }
            catch (Exception x)
            {
                throw x;
            }
        }
    }
}
