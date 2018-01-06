using KW.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public class RoleAccess : Entity
    {
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public int MenuId { get; set; }
        public virtual Menu Menu { get; set; }
    }
}
