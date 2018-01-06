using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Application.Params
{
    public class GeneralCollectionParameter
    {
        public string Field { get; set; }
        public IList<string> Values { get; set; }
        public GeneralCollectionParameter()
        {

        }
    }
}
