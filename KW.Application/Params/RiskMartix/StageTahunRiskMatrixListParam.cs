using System.Collections.Generic;

namespace KW.Application.Params
{
    public class StageTahunRiskMatrixListParameter : PaginationParam
    {
        public IList<GeneralCollectionParameter> Collection { get; set; }
        public IList<GeneralFilterParameter> Filter { get; set; }

        public StageTahunRiskMatrixListParameter()
        {
            this.Collection = new List<GeneralCollectionParameter>();
            this.Filter = new List<GeneralFilterParameter>();
        }
    }
}
