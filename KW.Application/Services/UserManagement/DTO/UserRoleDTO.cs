using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Application.DTO
{
    public class UserRoleDTO
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public RoleDTO Role { get; set; }
        public UserRoleDTO()
        {

        }
        public UserRoleDTO(UserRole model) : this()
        {
            if (model == null) return;

            this.UserId = model.UserId;
            this.RoleId = model.RoleId;

            if(model.Role != null)
            {
                Role role = model.Role;
                this.Role = RoleDTO.From(role);
            }

        }

        public static UserRoleDTO From(UserRole role)
        {
            return new UserRoleDTO(role);
        }
    }
}
