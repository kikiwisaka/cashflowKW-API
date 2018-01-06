using System.Collections.Generic;

namespace KW.Application.Params
{
    public class RiskMatrixProjectListParameter : PaginationParam
    {
        public IList<GeneralCollectionParameter> Collection { get; set; }
        public IList<GeneralFilterParameter> Filter { get; set; }
        public bool IsAllData { get; set; }

        public RiskMatrixProjectListParameter()
        {
            this.Collection = new List<GeneralCollectionParameter>();
            this.Filter = new List<GeneralFilterParameter>();
        }
    }
}
