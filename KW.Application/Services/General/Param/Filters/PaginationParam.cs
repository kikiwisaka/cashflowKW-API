using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Application.Params
{
    public class PaginationParam
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; }
        public string SearchBy { get; set; }
        public string SortBy { get; set; }
        public string SortDirection { get; set; }
        private int Skip { get; set; }

        public PaginationParam()
        {
            this.Search = string.Empty;
            this.SearchBy = string.Empty;
        }

        public void Validate()
        {
            if (this.PageNo > 1)
            {
                this.Skip = (this.PageNo - 1) * this.PageSize;
            }
        }

        public bool IsPagination()
        {
            if (this.PageNo > 0 && this.PageSize > 0)
                return true;
            return false;
        }

        public bool IsDefault()
        {
            if (this.PageNo > 0 && this.PageSize > 0)
                return true;
            return false;
        }

        //public bool IsAllData()
        //{
        //    if (this.AllData == true)
        //        return true;
        //    return false;
        //}
    }
}
