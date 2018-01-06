using KW.Core;
using System;

namespace KW.Domain
{
    public class CorrelatedSektorDetail : Entity
    {
        public int? CreateBy { get; private set; }
        public DateTime? CreateDate { get; private set; }
        public int? UpdateBy { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public bool? IsDelete { get; private set; }
        public DateTime? DeleteDate { get; private set; }

        //foreign key
        public int CorrelatedSektorId { get; private set; }
        public int RiskRegistrasiIdRow { get; private set; }
        public int RiskRegistrasiIdCol { get; private set; }
        public int CorrelationMatrixId { get; private set; }

        //navigarion properties
        public virtual CorrelatedSektor CorrelatedSektor { get; private set; }
        public virtual CorrelationMatrix CorrelationMatrix { get; private set; }

        public CorrelatedSektorDetail() { }

        public CorrelatedSektorDetail(CorrelatedSektor correlatedSector, int riskRegistrasiRow, int riskRegistrasiCol, CorrelationMatrix correlationMatrix, int? createBy, DateTime? createDate)
        {
            this.CreateBy = createBy;
            this.CreateDate = createDate;
            this.IsDelete = false;
            this.CorrelatedSektorId = correlatedSector.Id;
            this.RiskRegistrasiIdRow = riskRegistrasiRow;
            this.RiskRegistrasiIdCol = riskRegistrasiCol;
            this.CorrelationMatrixId = correlationMatrix.Id;
        }

        public virtual void Update(CorrelationMatrix correlationMatrix, int? updateBy, DateTime? updateDate)
        {
            this.IsDelete = false;
            this.CorrelationMatrixId = correlationMatrix.Id;
            this.UpdateBy = updateBy;
            this.UpdateDate = updateDate;
        }
    }
}
