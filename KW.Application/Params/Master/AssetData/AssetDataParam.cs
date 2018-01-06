//using System.ComponentModel.DataAnnotations;
using System;

namespace KW.Application.Params
{
    public class AssetDataParam
    {
        public string AssetClass { get; set; }
        public int TermAwal { get; set; }
        public int TermAkhir { get; set; }
        public decimal AssumentReturn { get; set; }
        public int OutstandingStartYears { get; set; }
        public int OutstandingEndYears { get; set; }
        public decimal AssetValue { get; set; }
        public decimal Porpotion { get; set; }
        public decimal AssumedReturnPercentage { get; set; }
        public decimal AssumedReturn { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool? Status { get; set; }


        public AssetDataParam() { }
    }
}
