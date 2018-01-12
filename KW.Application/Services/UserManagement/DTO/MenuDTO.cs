using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Application.DTO
{
    public class MenuDTO : MenuAccessLiteWithChildDTO
    {
        public string Description { get; set; }
        public bool Active { get; set; }
        public MenuDTO Parent { get; set; }

        public MenuDTO()
        {

        }

        public MenuDTO(Menu menu)
        {
            if (menu == null) return;
            //if (menu.IsSuperAdminOnly == true) return;

            this.Id = menu.Id;
            this.Name = menu.Name;
            this.Description = menu.Description;
            this.ControllerName = menu.ControllerName;
            this.ActionName = menu.ActionName;
            this.Active = menu.Active;
            this.IsSuperAdminOnly = menu.IsSuperAdminOnly;

            if (menu.Parent != null)
                this.Parent = MenuDTO.From(menu.Parent);
        }

        public MenuDTO(MenuDTO menu)
        {
            if (menu == null) return;
            //if (menu.IsSuperAdminOnly == true) return;

            this.Id = menu.Id;
            this.Name = menu.Name;
            this.Description = menu.Description;
            this.ControllerName = menu.ControllerName;
            this.ActionName = menu.ActionName;
            this.Active = menu.Active;
            this.ParentId = menu.ParentId;
            this.IsSuperAdminOnly = menu.IsSuperAdminOnly;
            if (this.ParentId < 1 && this.ParentId != this.Id)
                this.Parent = MenuDTO.From(menu.Parent);
        }

        public static MenuDTO From(Menu menu)
        {
            return new MenuDTO(menu);
        }

        public static MenuDTO From(MenuDTO menu)
        {
            return new MenuDTO(menu);
        }

        public static IList<MenuDTO> From(IList<Menu> menus)
        {
            IList<MenuDTO> menuDTOs = new List<MenuDTO>();
            foreach (var menu in menus)
            {
                menuDTOs.Add(new MenuDTO(menu));
            }
            return menuDTOs;
        }

        public static IList<MenuDTO> From(IList<MenuDTO> menuDTOs)
        {
            return menuDTOs;
        }
    }

    public class MenuAccessRoleDTO : MenuAccessLiteWithChildDTO
    {
        public string Description { get; set; }
        public bool Active { get; set; }
        public bool HasAccess { get; set; }
        public IList<MenuAccessRoleDTO> Childs { get; set; }
        public MenuAccessRoleDTO()
        {
            this.Childs = new List<MenuAccessRoleDTO>();
        }

        public MenuAccessRoleDTO(Menu menu)
        {
            if (menu == null) return;
            //if (menu.IsSuperAdminOnly == true) return;

            this.Id = menu.Id;
            this.Name = menu.Name;
            this.Description = menu.Description;
            //this.URL = menu.URL;
            this.ControllerName = menu.ControllerName;
            this.ActionName = menu.ActionName;
            this.Active = menu.Active;
            this.IsSuperAdminOnly = menu.IsSuperAdminOnly;
            if (menu.Parent != null)
            {
                this.ParentId = menu.Parent.Id;
            }
            else
            {
                this.ParentId = 0;
            }
        }

        public MenuAccessRoleDTO(Menu menu, bool hasAccess) : this(menu)
        {
            this.HasAccess = hasAccess;
        }

        public MenuAccessRoleDTO(MenuAccessRoleDTO menu)
        {
            if (menu == null) return;
            //if (menu.IsSuperAdminOnly == true) return;

            this.Id = menu.Id;
            this.Name = menu.Name;
            this.Description = menu.Description;
            this.ControllerName = menu.ControllerName;
            this.ActionName = menu.ActionName;
            this.Active = menu.Active;
            this.HasAccess = menu.HasAccess;
            this.IsSuperAdminOnly = menu.IsSuperAdminOnly;
        }

        public static MenuAccessRoleDTO From(Menu menu)
        {
            return new MenuAccessRoleDTO(menu);
        }

        public static MenuAccessRoleDTO From(MenuAccessRoleDTO menu)
        {
            return new MenuAccessRoleDTO(menu);
        }

        public static IList<MenuAccessRoleDTO> From(IList<Menu> menus)
        {
            IList<MenuAccessRoleDTO> menuDTOs = new List<MenuAccessRoleDTO>();
            foreach (var menu in menus)
            {
                menuDTOs.Add(new MenuAccessRoleDTO(menu));
            }
            return menuDTOs;
        }

        public static IList<MenuAccessRoleDTO> From(IList<Menu> menus, bool hasAccess)
        {
            IList<MenuAccessRoleDTO> menuDTOs = new List<MenuAccessRoleDTO>();
            foreach (var menu in menus)
            {
                menuDTOs.Add(new MenuAccessRoleDTO(menu, hasAccess));
            }
            return menuDTOs;
        }

        public static IList<MenuAccessRoleDTO> From(IList<MenuAccessRoleDTO> menuDTOs)
        {
            return menuDTOs;
        }
    }

    public class MenuAccessLiteWithChildDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsGeneralAccess { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string StyleClass { get; set; }
        public string Icon { get; set; }
        public int Sequence { get; set; }
        public bool IsOnMenu { get; set; }
        public bool IsSuperAdminOnly { get; set; }
        public IList<MenuAccessLiteWithChildDTO> Children { get; set; }
        public int ParentId { get; set; }
        public MenuAccessLiteWithChildDTO()
        {

        }

        public MenuAccessLiteWithChildDTO(Menu menu)
        {
            if (menu == null) return;
            //if (menu.IsSuperAdminOnly == true) return;

            this.Id = menu.Id;
            this.Name = menu.Name;
            this.ControllerName = menu.ControllerName;
            this.IsGeneralAccess = menu.IsGeneralAccess;
            this.ActionName = menu.ActionName;
            this.Sequence = menu.Sequence;
            this.IsOnMenu = menu.IsOnMenu;
            this.Icon = menu.Icon;
            this.StyleClass = menu.StyleClass;
            this.IsSuperAdminOnly = menu.IsSuperAdminOnly;
            if (menu.Parent != null)
                this.ParentId = menu.Parent.Id;
        }

        public MenuAccessLiteWithChildDTO(Menu menu, List<MenuAccessLiteWithChildDTO> children) : this(menu)
        {
            this.Children = children;
        }

        public MenuAccessLiteWithChildDTO(MenuDTO menu)
        {
            if (menu == null) return;

            this.ControllerName = menu.ControllerName;
            this.ActionName = menu.ActionName;
        }

        public MenuAccessLiteWithChildDTO(MenuAccessLiteWithChildDTO menu)
        {
            if (menu == null) return;

            this.ControllerName = menu.ControllerName;
            this.ActionName = menu.ActionName;
        }
        public MenuAccessLiteWithChildDTO(MenuAccessRoleDTO menu)
        {
            if (menu == null) return;

            this.ControllerName = menu.ControllerName;
            this.ActionName = menu.ActionName;
        }

        public static MenuAccessLiteWithChildDTO From(Menu menu)
        {
            return new MenuAccessLiteWithChildDTO(menu);
        }

        public static MenuAccessLiteWithChildDTO From(MenuAccessRoleDTO menu)
        {
            return new MenuAccessLiteWithChildDTO(menu);
        }

        public static IList<MenuAccessLiteWithChildDTO> From(IList<Menu> menus)
        {
            IList<MenuAccessLiteWithChildDTO> menuDTOs = new List<MenuAccessLiteWithChildDTO>();
            foreach (var menu in menus)
            {
                menuDTOs.Add(new MenuAccessLiteWithChildDTO(menu));
            }
            return menuDTOs;
        }

        public static IList<MenuAccessLiteWithChildDTO> OrderedFrom(IList<Menu> menus)
        {
            return OrderedFrom(menus, null);
        }

        public static IList<MenuAccessLiteWithChildDTO> OrderedFrom(IList<Menu> menus, Menu parent)
        {
            IList<MenuAccessLiteWithChildDTO> menuDTOs = new List<MenuAccessLiteWithChildDTO>();
            List<Menu> menuNotInserted = new List<Menu>();
            List<Menu> currentList = menus.Where(x => x.Parent == parent).OrderBy(y => y.Sequence).ToList();
            foreach (var menu in currentList)
            {
                List<Menu> currentChildren = new List<Menu>();
                currentChildren = menus.Where(x => x.Parent == menu).ToList();
                List<MenuAccessLiteWithChildDTO> childList = OrderedFrom(menus, menu).ToList();

                menuDTOs.Add(new MenuAccessLiteWithChildDTO(menu, childList));
            }
            return menuDTOs;
        }
        public static IList<MenuAccessLiteWithChildDTO> From(IList<MenuAccessLiteWithChildDTO> menuDTOs)
        {
            return menuDTOs;
        }
    }
}
