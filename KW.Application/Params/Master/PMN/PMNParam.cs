//using System.ComponentModel.DataAnnotations;
using System;

namespace KW.Application.Params
{
    public class PMNParam
    {
        public int PMNToModalDasarCap { get; set; }
        public decimal RecourseDelay { get; set; }
        public decimal DelayYears { get; set; }
        public decimal OpexGrowth { get; set; }
        public decimal Opex { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool? Status { get; set; }
        public decimal ValuePMNToModalDasarCap { get; set; }


        public PMNParam() { }
    }
}
