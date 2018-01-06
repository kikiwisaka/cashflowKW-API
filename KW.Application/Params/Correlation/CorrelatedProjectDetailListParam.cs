using System;
using System.Collections.Generic;

namespace KW.Application.Params
{
    public class CorrelatedProjectDetailListParam : PaginationDetailParam
    {
        public IList<GeneralCollectionParameter> Collection { get; set; }
        public IList<GeneralFilterParameter> Filter { get; set; }

        public CorrelatedProjectDetailListParam()
        {
            this.Collection = new List<GeneralCollectionParameter>();
            this.Filter = new List<GeneralFilterParameter>();
        }
    }
}
