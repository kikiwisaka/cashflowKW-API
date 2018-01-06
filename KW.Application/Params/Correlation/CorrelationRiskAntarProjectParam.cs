using System.ComponentModel.DataAnnotations;
using System;

namespace KW.Application.Params
{
    public class CorrelationRiskAntarProjectParam
    {
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }
        public int CorrelationRiskAntarSektorId { get; set; }
        public CorrelationRiskAntarProjectColletion[] CorrelationRiskAntarProjectColletion { get; set; }

        public CorrelationRiskAntarProjectParam() { }
    }

    public class CorrelationRiskAntarProjectColletion
    {
        public int ProjectId { get; set; }
        public ProjectCorrelationMatrixValues[] ProjectCorrelationMatrixValues { get; set; }
        public CorrelationRiskAntarProjectColletion() { }
    }

    public class ProjectCorrelationMatrixValues
    {
        public int ProjectIdRow { get; set; }
        public int ProjectIdCol { get; set; }
        public int CorrelationMatrixId { get; set; }

        public ProjectCorrelationMatrixValues() { }
    }
}
