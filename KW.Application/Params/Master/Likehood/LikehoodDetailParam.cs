using System;

namespace KW.Application.Params
{
    public class LikehoodDetailParam
    {
        public string DefinisiLikehood { get; set; }
        public decimal Lower { get; set; }
        public decimal Upper { get; set; }
        public decimal Average { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool? Status { get; set; }
        public int LikehoodId { get; set; }

        public LikehoodDetailParam() { }
    }
}
