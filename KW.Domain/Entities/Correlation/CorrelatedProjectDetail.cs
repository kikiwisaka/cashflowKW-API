using KW.Core;
using System;

namespace KW.Domain
{
    public class CorrelatedProjectDetail : Entity
    {
        public int? CreateBy { get; private set; }
        public DateTime? CreateDate { get; private set; }
        public int? UpdateBy { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public bool? IsDelete { get; private set; }
        public DateTime? DeleteDate { get; private set; }

        //foreign key
        public int CorrelatedProjectId { get; private set; }
        public int ProjectIdRow { get; private set; }
        public int ProjectIdCol { get; private set; }
        public int CorrelationMatrixId { get; private set; }

        //navigarion properties
        public virtual CorrelatedProject CorrelatedProject { get; private set; }
        public virtual CorrelationMatrix CorrelationMatrix { get; private set; }

        public CorrelatedProjectDetail() { }

        public CorrelatedProjectDetail(CorrelatedProject correlatedProject, int projectIdRow, int projectIdCol, CorrelationMatrix correlationMatrix, int? createBy, DateTime? createDate)
        {
            this.CreateBy = createBy;
            this.CreateDate = createDate;
            this.IsDelete = false;
            this.CorrelatedProjectId = correlatedProject.Id;
            this.ProjectIdRow = projectIdRow;
            this.ProjectIdCol = projectIdCol;
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
