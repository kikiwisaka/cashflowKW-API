//using System.ComponentModel.DataAnnotations;
using System;

namespace KW.Application.Params
{
    public class TahapanParam
    {
        public string NamaTahapan { get; set; }
        public string Keterangan { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }


        public TahapanParam() { }
    }
}
