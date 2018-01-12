using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public class API
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public bool Active { get; set; }
        public bool IsGeneralAccess { get; set; }
        public virtual ICollection<APIMenu> APIMenuList { get; set; }
    }
}
