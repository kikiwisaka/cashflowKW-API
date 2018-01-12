using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public bool Active { get; set; }
        public bool IsGeneralAccess { get; set; }
        public bool IsOnMenu { get; set; }
        public string StyleClass { get; set; }
        public string Icon { get; set; }
        public int Sequence { get; set; }
        public bool IsSuperAdminOnly { get; set; }
        public int? ParentId { get; set; }
        public virtual Menu Parent { get; set; }
        public ICollection<APIMenu> APIMenuList { get; set; }
        public ICollection<RoleAccess> RoleAccessList { get; set; }

        public Menu()
        {
            this.APIMenuList = new List<APIMenu>();
            this.RoleAccessList = new List<RoleAccess>();
        }
        public void AddRoleAccess(RoleAccess mod)
        {
            this.RoleAccessList.Add(mod);
        }
        public void RemoveRoleAccess(RoleAccess mod)
        {
            this.RoleAccessList.Remove(mod);
        }
    }
}
