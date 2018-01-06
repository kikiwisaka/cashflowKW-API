using KW.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public class RiskMatrixStage : Entity
    {
        public int? CreateBy { get; private set; }
        public DateTime? CreateDate { get; private set; }
        public int? UpdateBy { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public bool? IsDelete { get; private set; }
        public DateTime? DeleteDate { get; private set; }

        //Foreign Key
        public int ProjectId { get; private set; }
        public int RiskMatrixId { get; private set; }
        public int StageId { get; private set; }


        // Navigation properties
        public virtual Project Project { get; set; }
        public virtual RiskMatrix RiskMatrix { get; set; }
        public virtual Stage Stage { get; set; }

        public RiskMatrixStage()
        { }

        public RiskMatrixStage(Project Project, RiskMatrix RiskMatrix, Stage Stage, int? createBy, DateTime? createDate)
        {
            this.ProjectId = Project.Id;
            this.RiskMatrixId = RiskMatrix.Id;
            this.StageId = Stage.Id;
            this.CreateBy = createBy;
            this.CreateDate = createDate;
            this.IsDelete = false;
        }

        public virtual void Update(Project Project, RiskMatrix RiskMatrix, Stage Stage, int? updateBy, DateTime? updateDate)
        {
            this.ProjectId = Project.Id;
            this.RiskMatrixId = RiskMatrix.Id;
            this.StageId = Stage.Id;
            this.UpdateBy = updateBy;
            this.UpdateDate = updateDate;
        }

        public virtual void Delete(int deleteBy, DateTime deleteDate)
        {
            this.IsDelete = true;
            this.DeleteDate = deleteDate;
        }
    }
}
