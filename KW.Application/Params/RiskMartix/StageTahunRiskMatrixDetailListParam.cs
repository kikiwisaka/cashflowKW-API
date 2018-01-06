using System.Collections.Generic;

namespace KW.Application.Params
{
    public class StageTahunRiskMatrixDetailListParam : PaginationDetailParam
    {
        public IList<GeneralCollectionParameter> Collection { get; set; }
        public IList<GeneralFilterParameter> Filter { get; set; }

        public StageTahunRiskMatrixDetailListParam()
        {
            this.Collection = new List<GeneralCollectionParameter>();
            this.Filter = new List<GeneralFilterParameter>();
        }
    }
}
