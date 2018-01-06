using System;
using System.ComponentModel.DataAnnotations;

namespace KW.Application.Params
{
    public class SubRiskRegistrasiParam
    {
        public int RiskRegistrasiId { get; set; }
        public string KodeRisk { get; set; }
        public string RiskEvenClaim { get; set; }
        public string DescriptionRiskEvenClaim { get; set; }
        public string SugestionMigration { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }

        public SubRiskRegistrasiParam()
        {

        }
    }
}
