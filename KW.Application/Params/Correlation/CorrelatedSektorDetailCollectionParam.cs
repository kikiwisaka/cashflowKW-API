using System;
using System.Collections.Generic;

namespace KW.Application.Params
{
    public class CorrelatedSektorDetailCollectionParam
    {
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }
        public int CorrelatedSektorId { get; set; }
        public CorrelatedSektorDetailCollection[] CorrelatedSektorDetailCollection { get; set; }

        public CorrelatedSektorDetailCollectionParam() { }
    }

    public class CorrelatedSektorDetailCollection
    {
        public int RiskRegistrasiId { get; set; }
        public RiskRegistrasiValues[] RiskRegistrasiValues { get; set; }

        public CorrelatedSektorDetailCollection() { }
    }

    public class RiskRegistrasiValues
    {
        public int RiskRegistrasiIdRow { get; set; }
        public int RiskRegistrasiIdCol { get; set; }
        public int CorrelationMatrixId { get; set; }

        public RiskRegistrasiValues() { }
    }
}
