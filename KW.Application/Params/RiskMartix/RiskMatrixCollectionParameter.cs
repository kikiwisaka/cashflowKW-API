using System;
using System.Collections.Generic;

namespace KW.Application.Params
{
    public class RiskMatrixCollectionParameter
    {
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }
        public RiskMatrixCollection[] RiskMatrixCollection { get; set; }

        public RiskMatrixCollectionParameter() { }
    }

    public class RiskMatrixCollection
    {
        public int StageTahunRiskMatrixId { get; set; } //Tahun
        public IList<RiskMatrixValue> RiskMatrixValue { get; set; }

        public RiskMatrixCollection() { }
    }

    public class RiskMatrixValue
    {
        public int RiskRegistrasiId { get; set; }
        public string[] Values { get; set; } // index[0] = Exposure Value, index[1] = LikelihoodDetailId

        public RiskMatrixValue() { }
    }
}
