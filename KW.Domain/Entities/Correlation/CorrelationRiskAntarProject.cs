using KW.Core;
using System;

namespace KW.Domain
{
    public class CorrelationRiskAntarProject : Entity
    {
        public int? Status{ get; private set; }
        public int? CreateBy { get; private set; }
        public DateTime? CreateDate { get; private set; }
        public int? UpdateBy { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public bool? IsDelete { get; private set; }
        public DateTime? DeleteDate { get; private set; }

        // Foreign key
        public int CorrelationRiskAntarSektorId { get; private set; }
        public int ProjectIdRow { get; private set; }
        public int ProjectIdCol { get; private set; }
        public int CorrelationMatrixId { get; private set; }

        // Navigation properties
        public virtual CorrelationRiskAntarProject CorrelationRiskAntarSektor { get; set; }
        public virtual CorrelationMatrix CorrelationMatrix { get; set; }


        public CorrelationRiskAntarProject()
        {
        }

        public CorrelationRiskAntarProject(CorrelationRiskAntarSektor correlationRiskAntarSektor, int projectIdRow, int projectIdCol, CorrelationMatrix correlationMatrix, int? createBy, DateTime? createDate)
        {
            this.CorrelationRiskAntarSektorId = correlationRiskAntarSektor.Id;
            this.ProjectIdRow = projectIdRow;
            this.ProjectIdCol = projectIdCol;
            this.CorrelationMatrixId = correlationMatrix.Id;
            this.IsDelete = false;
            this.CreateBy = createBy;
            this.CreateDate = createDate;
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
