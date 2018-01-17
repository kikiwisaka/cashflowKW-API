using System.Collections.Generic;

namespace KW.Application.Params
{
    public class ExpenditureListParameter : PaginationParam
    {
        public IList<GeneralCollectionParameter> Collection { get; set; }
        public IList<GeneralFilterParameter> Filter { get; set; }

        public ExpenditureListParameter()
        {
            this.Collection = new List<GeneralCollectionParameter>();
            this.Filter = new List<GeneralFilterParameter>();
        }
    }
}
