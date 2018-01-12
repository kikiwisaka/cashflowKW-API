using KW.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Application.Params
{
    public class EmployeeListParameter : PaginationParam
    {
        //public PaginationParam Paging { get; set; }
        public string Gender { get; set; }
        public string Active { get; set; }
        public string PayType { get; set; }
        public string EmployeeTypeId { get; set; }
        public IList<GeneralCollectionParameter> Collection { get; set; }
        public IList<GeneralFilterParameter> Filter { get; set; }

        public EmployeeListParameter()
        {
            //this.Paging = new PaginationParam();
            this.Collection = new List<GeneralCollectionParameter>();
            this.Filter = new List<GeneralFilterParameter>();
        }

        public void ValidateDate()
        {
            if (Filter != null && Filter.Count > 0)
            {
                foreach (var item in Filter)
                {
                    if (item.Field == "HireDate" || item.Field == "BirthDate")
                    {
                        if (!string.IsNullOrEmpty(item.From))
                            item.From = DateHelper.ToStringDate(item.From);
                        if (!string.IsNullOrEmpty(item.To))
                            item.To = DateHelper.ToStringDate(item.To);
                    }
                }
            }
        }
    }
}
