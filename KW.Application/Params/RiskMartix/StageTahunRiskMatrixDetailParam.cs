using System;

namespace KW.Application.Params
{
    public class StageTahunRiskMatrixDetailParam
    {
        
        public int StageTahunRiskMatrixId { get; set; }
        public int RiskRegistrasiId { get; set; }
        public int LikehoodDetailId { get; set; }
        public decimal? NilaiExpose { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }

        public StageTahunRiskMatrixDetailParam() { }
    }
}
