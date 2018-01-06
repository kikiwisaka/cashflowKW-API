using System.Collections.Generic;

namespace KW.Application.Params
{
    public class CorrelatedSektorDetailListParam : PaginationDetailParam
    {
        public IList<GeneralCollectionParameter> Collection { get; set; }
        public IList<GeneralFilterParameter> Filter { get; set; }

        public CorrelatedSektorDetailListParam()
        {
            this.Collection = new List<GeneralCollectionParameter>();
            this.Filter = new List<GeneralFilterParameter>();
        }
    }
}
