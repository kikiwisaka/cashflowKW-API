using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Application.Params
{
    public class GeneralFilterParameter
    {
        public string Field { get; set; }
        public string Equal { get; set; }
        public string Like { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string LessThan { get; set; }
        public string MoreThan { get; set; }
        public string Option { get; set; }
        public string Type { get; set; }

        public GeneralFilterParameter()
        {

        }
    }
}
