using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Application.DTO
{
    public class PaginationDTO
    {
        public int PageCount { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public object results { get; set; }
    }
}
