//using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace KW.Application.Params
{
    public class StageTahunRiskMatrixParam
    {
        public int RiskMatrixProjectId { get; set; }
        public int? Tahun { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public bool? IsUpdate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }

        public DateTime StartProject { get; set; }
        public DateTime EndProject { get; set; }
        public IList<StageValue> StageValue { get; set; }

        public StageTahunRiskMatrixParam() { }
    }

    public class StageValue
    {
        public int StageId { get; set; }
        public int[] Values { get; set; } // index[0] = Start Value, index[1] = End Value

        public StageValue() { }
    }
}
