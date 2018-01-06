using KW.Core;
using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Application.DTO
{
    public class RoleAccess : Entity
    {
        public Role Role { get; set; }
        public Menu Menu { get; set; }
    }
}
