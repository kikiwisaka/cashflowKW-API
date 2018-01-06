using System;

namespace KW.Application.Params
{
    public class CorrelatedProjectDetailCollectionParam
    {
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }
        public int CorrelatedProjectId { get; set; }
        public CorrelatedProjectDetailCollection[] CorrelatedProjectDetailCollection { get; set; }

        public CorrelatedProjectDetailCollectionParam() { }
    }

    public class CorrelatedProjectDetailCollection
    {
        public int ProjectId { get; set; }
        public CorrelatedProjectMatrixValues[] CorrelatedProjectMatrixValues { get; set; }

        public CorrelatedProjectDetailCollection() { }
    }

    public class CorrelatedProjectMatrixValues
    {
        public int ProjectIdRow { get; set; }
        public int ProjectIdCol { get; set; }
        public int CorrelationMatrixId { get; set; }

        public CorrelatedProjectMatrixValues() { }
    }
}
