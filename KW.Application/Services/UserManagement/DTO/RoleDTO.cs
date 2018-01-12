using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Application.DTO
{
    public class RoleDTO : RoleListDTO
    {
        public IList<MenuDTO> Accesses { get; set; }

        public RoleDTO()
        {
        }

        public RoleDTO(Role role)
        {
            if (role == null) return;

            this.Id = role.Id;
            this.Name = role.Name;
            this.Description = role.Description;
            this.Status = role.Status;
            this.IsMasking = role.IsMasking;
            this.IsAllJobs = role.IsAllJobs;

            if (role.RoleAccesses != null)
                this.Accesses = role.RoleAccesses.Select(x => MenuDTO.From(x.Menu)).ToList();  //APIDTO.From(role.Accesses);

            this.EmployeeTypes = new List<int>();
            if (role.RoleEmployeeTypes != null && role.RoleEmployeeTypes.Count > 0)
            {
                foreach (var roleEmpType in role.RoleEmployeeTypes)
                {
                }
            }
        }

        public static RoleDTO From(Role role)
        {
            return new RoleDTO(role);
        }

        public static IList<RoleDTO> From(IList<Role> roles)
        {
            IList<RoleDTO> roleDTOs = new List<RoleDTO>();
            foreach (var role in roles)
            {
                roleDTOs.Add(new RoleDTO(role));
            }
            return roleDTOs;
        }

        internal static RoleDTO From(IQueryable<Role> roles)
        {
            throw new NotImplementedException();
        }
    }

    public class RoleListCompleteDTO : RoleListDTO
    {
        public string CreateDate { get; set; }
        public string CreateTime { get; set; }
        public User Creator { get; set; }
        public string CreatorPosition { get; set; }
        public int UserCount { get; set; }

        public RoleListCompleteDTO()
        {

        }
        public RoleListCompleteDTO(Role role)
        {
            if (role == null) return;

            this.Id = role.Id;
            this.Name = role.Name;
            this.Description = role.Description;
            this.CreateDate = role.CreateDate;
            this.CreateTime = role.CreateTime;
            this.Status = role.Status;
            this.IsMasking = role.IsMasking;
            this.IsAllJobs = role.IsAllJobs;

            //if (role.Creator != null)
            //{
            //    this.Creator = role.Creator;
            //    //JobContract job = role.Creator.Employee.JobContract.Where(x => x.IsActive == 1).LastOrDefault();
            //    //if (job != null)
            //    //    this.CreatorPosition = "";
            //}
            //else
            //{
            //    this.CreatorPosition = "";
            //    //this.Creator = new User("", "", false, "");
            //}

            //if (role.UserRoles != null && role.UserRoles.Count > 0)
            //{
            //    UserCount = role.UserRoles.Count;
            //}
            //else
            //{
            //    UserCount = 0;
            //}

            this.EmployeeTypes = new List<int>();
            if (role.RoleEmployeeTypes != null && role.RoleEmployeeTypes.Count > 0)
            {
                foreach (var item in role.RoleEmployeeTypes)
                {
                    RoleEmployeeType roleEmpType = item;
                    //this.EmployeeTypes.Add(roleEmpType.EmployeeType.Id);
                }
            }
        }

        public static RoleListCompleteDTO From(Role role)
        {
            return new RoleListCompleteDTO(role);
        }

        public static IList<RoleListCompleteDTO> From(IList<Role> roles)
        {
            IList<RoleListCompleteDTO> roleDTOs = new List<RoleListCompleteDTO>();
            foreach (var role in roles)
            {
                roleDTOs.Add(new RoleListCompleteDTO(role));
            }
            return roleDTOs;
        }
    }

    public class RoleListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<int> EmployeeTypes { get; set; }
        public bool Status { get; set; }
        public bool IsMasking { get; set; }
        public bool IsAllJobs { get; set; }

        public RoleListDTO()
        {

        }
        public RoleListDTO(Role role)
        {
            if (role == null) return;

            this.Id = role.Id;
            this.Name = role.Name;
            this.Description = role.Description;
            this.IsMasking = role.IsMasking;
            this.IsAllJobs = role.IsAllJobs;
            this.EmployeeTypes = new List<int>();
        }

        public RoleListDTO(RoleListCompleteDTO role)
        {
            if (role == null) return;

            this.Id = role.Id;
            this.Name = role.Name;
            this.Description = role.Description;
            this.IsMasking = role.IsMasking;
            this.IsAllJobs = role.IsAllJobs;
            this.EmployeeTypes = new List<int>();
            if (role.EmployeeTypes != null && role.EmployeeTypes.Count > 0)
            {
                foreach (var item in role.EmployeeTypes)
                {
                    EmployeeTypes.Add(item);
                }
            }
        }

        public static RoleListDTO From(Role role)
        {
            return new RoleListDTO(role);
        }

        public static IList<RoleListDTO> From(IList<Role> roles)
        {
            IList<RoleListDTO> roleDTOs = new List<RoleListDTO>();
            foreach (var role in roles)
            {
                roleDTOs.Add(new RoleListDTO(role));
            }
            return roleDTOs;
        }

        public static RoleListDTO From(RoleListCompleteDTO role)
        {
            return new RoleListDTO(role);
        }

        public static IList<RoleListDTO> From(IList<RoleListCompleteDTO> roles)
        {
            IList<RoleListDTO> roleDTOs = new List<RoleListDTO>();
            foreach (var role in roles)
            {
                roleDTOs.Add(new RoleListDTO(role));
            }
            return roleDTOs;
        }
    }
}
