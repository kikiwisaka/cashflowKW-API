using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public class RoleEmployeeType
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public int EmployeeTypeId { get; set; }

        public RoleEmployeeType()
        {

        }

        public RoleEmployeeType(Role role)
        {
            this.Role = role;
        }

        public void Update(Role role)
        {
            this.Role = role;
        }
    }
}
