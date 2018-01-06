using System;
using System.Collections.Generic;

namespace KW.Application.Params
{
    public class ProjectParam
    {
        public string NamaProject { get; set; }
        public DateTime TahunAwalProject { get; set; }
        public DateTime TahunAkhirProject { get; set; }
        public int UserId{ get; set; }
        public int TahapanId { get; set; }
        //public IList<string> RiskRegistrasiId { get; set; }
        public string[] RiskRegistrasiId { get; set; }
        public bool? StatusProject { get; set; }
        public decimal Minimum { get; set; }
        public decimal Maximum { get; set; }
        public int SektorId { get; set; }
        public string Keterangan { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }


        public ProjectParam() { }
    }
}
