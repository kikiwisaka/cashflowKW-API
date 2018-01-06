//using System.ComponentModel.DataAnnotations;
using System;

namespace KW.Application.Params
{
    public class RiskMatrixProjectParam
    {
        public int ProjectId { get; set; }
        public int RiskMatrixId { get; set; }
        public int ScenarioId { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }


        public RiskMatrixProjectParam() { }
    }
}
