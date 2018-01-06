using KW.Common;
using KW.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public class Role : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreateDate { get; set; }
        public string CreateTime { get; set; }
        public bool Status { get; set; }
        public bool IsMasking { get; set; }
        public bool IsAllJobs { get; set; }

        public int? CreatorId { get; set; }
        public virtual User Creator { get; set; }

        public virtual ICollection<RoleAccess> RoleAccesses { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<RoleEmployeeType> RoleEmployeeTypes { get; set; }


        public Role()
        {
            this.RoleAccesses = new List<RoleAccess>();
            this.UserRoles = new List<UserRole>();
            this.CreateDate = DateHelper.DateStampNow().ToString();
            this.CreateTime = DateHelper.TimeStampNow().ToString();
            this.Status = true;
            this.RoleEmployeeTypes = new List<RoleEmployeeType>();
        }

        public Role(string name, string description, bool isMasking, bool isAllJob) : this()
        {
            this.Name = name;
            this.Description = description;
            this.IsMasking = isMasking;
            this.IsAllJobs = isAllJob;
        }
        public virtual void Update(string name, string description, bool isMasking, bool isAllJob)
        {
            this.Name = name;
            this.Description = description;
            this.IsMasking = isMasking;
            this.IsAllJobs = isAllJob;
        }
        public virtual void ChangeStatus(bool status)
        {
            this.Status = status;
        }

        public virtual void AddRoleEmployeeType(RoleEmployeeType item)
        {
            this.RoleEmployeeTypes.Add(item);
        }

        public virtual void DeleteRoleEmployeeType(RoleEmployeeType item)
        {
            this.RoleEmployeeTypes.Remove(item);
        }

        public virtual void AddRoleAccess(RoleAccess roleAccess)
        {
            this.RoleAccesses.Add(roleAccess);
        }
        public virtual void RemoveRoleAccess(RoleAccess roleAccess)
        {
            this.RoleAccesses.Remove(roleAccess);
        }

    }
}
