﻿//using System.ComponentModel.DataAnnotations;
using System;

namespace KW.Application.Params
{
    public class FunctionalRiskParam
    {
        public int? MatrixId { get; set; }
        public int? ScenarioId { get; set; }
        public int? ColorCommentId { get; set; }
        public string Definisi { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }
        public int[] Scenarios { get; set; }

        public FunctionalRiskParam() { }
    }
}
