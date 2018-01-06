using System;
using System.ComponentModel.DataAnnotations;

namespace KW.Application.Params
{
    public class RiskRegistrasiParam
    {
        public string KodeMRisk { get; set; }
        public string NamaCategoryRisk { get; set; }
        public string Definisi { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }

        public RiskRegistrasiParam()
        {

        }
    }
}
