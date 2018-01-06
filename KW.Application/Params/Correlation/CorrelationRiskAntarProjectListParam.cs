using System.Collections.Generic;

namespace KW.Application.Params
{
    public class CorrelationRiskAntarProjectListParameter : PaginationDetailParam
    {
        public IList<GeneralCollectionParameter> Collection { get; set; }
        public IList<GeneralFilterParameter> Filter { get; set; }

        public CorrelationRiskAntarProjectListParameter()
        {
            this.Collection = new List<GeneralCollectionParameter>();
            this.Filter = new List<GeneralFilterParameter>();
        }
    }
}
