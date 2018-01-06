using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public class APIMenu
    {
        public int Id { get; set; }
        public int ApiId { get; set; }
        public virtual API API { get; set; }
        public int MenuId { get; set; }
        public virtual Menu Menu { get; set; }
    }
}
