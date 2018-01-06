﻿using System;

namespace KW.Application.Params
{
    public class LikehoodParam
    {
        public string NamaLikehood { get; set; }
        //public decimal Incres { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool? Status { get; set; }


        public LikehoodParam() { }
    }
}
