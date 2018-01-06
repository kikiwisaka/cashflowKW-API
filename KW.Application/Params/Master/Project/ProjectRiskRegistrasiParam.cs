using System;

namespace KW.Application.Params
{
    public class ProjectRiskRegistrasiParam
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int RiskRegistrasiId { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }
        public ProjectRiskRegistrasiParam() { }
    }
}
