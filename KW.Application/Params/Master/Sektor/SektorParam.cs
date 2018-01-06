using System.ComponentModel.DataAnnotations;
using System;

namespace KW.Application.Params
{
    public class SektorParam
    {
        [Required]
        public string NamaSektor { get; set; }
        public decimal Minimum { get; set; }
        public decimal Maximum { get; set; }
        public string Definisi { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool? Status { get; set; }

        public SektorParam() { }
    }
}
