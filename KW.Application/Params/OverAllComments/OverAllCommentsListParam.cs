using System.Collections.Generic;

namespace KW.Application.Params
{
    public class OverAllCommentsListParameter : PaginationDetailParam
    {
        public IList<GeneralCollectionParameter> Collection { get; set; }
        public IList<GeneralFilterParameter> Filter { get; set; }

        public OverAllCommentsListParameter()
        {
            this.Collection = new List<GeneralCollectionParameter>();
            this.Filter = new List<GeneralFilterParameter>();
        }
    }
}
