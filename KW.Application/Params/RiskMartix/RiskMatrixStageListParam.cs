using System.Collections.Generic;

namespace KW.Application.Params
{
    public class RiskMatrixStageListParameter : PaginationParam
    {
        public IList<GeneralCollectionParameter> Collection { get; set; }
        public IList<GeneralFilterParameter> Filter { get; set; }

        public RiskMatrixStageListParameter()
        {
            this.Collection = new List<GeneralCollectionParameter>();
            this.Filter = new List<GeneralFilterParameter>();
        }
    }
}
