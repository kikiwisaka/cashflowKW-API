using KW.Core;
using System;

namespace KW.Domain
{
    public class StageTahunRiskMatrix : Entity
    {
        public int Tahun { get; private set; }
        public int? CreateBy { get; private set; }
        public DateTime? CreateDate { get; private set; }
        public bool? IsUpdate { get; private set; }
        public int? UpdateBy { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public bool? IsDelete { get; private set; }
        public DateTime? DeleteDate { get; private set; }

        //Foreign Key
        public int RiskMatrixProjectId { get; private set; }
        public int StageId { get; private set; }

        // Navigation properties
        public virtual RiskMatrixProject RiskMatrixProject { get; set; }
        public virtual Stage Stage { get; set; }

        public StageTahunRiskMatrix()
        { }

        public StageTahunRiskMatrix(RiskMatrixProject RiskMatrixProject, Stage Stage, int tahun, int? createBy, DateTime? createDate)
        {
            this.RiskMatrixProjectId = RiskMatrixProject.Id;
            this.StageId = Stage.Id;
            this.Tahun = tahun;
            this.CreateBy = createBy;
            this.CreateDate = createDate;
            this.IsDelete = false;
            this.IsUpdate = false;
        }

        public virtual void Update(RiskMatrixProject RiskMatrixProject, Stage Stage, int tahun, int? updateBy, DateTime? updateDate)
        {
            this.RiskMatrixProjectId = RiskMatrixProject.Id;
            this.StageId = Stage.Id;
            this.Tahun = tahun;
            this.UpdateBy = updateBy;
            this.UpdateDate = updateDate;
            this.IsUpdate = true;
        }

        public virtual void Delete(int deleteBy, DateTime deleteDate)
        {
            this.IsDelete = true;
            this.UpdateBy = deleteBy;
            this.DeleteDate = deleteDate;
        }
    }

    public class StageTahunRiskMatrixLite : Entity
    {
        public int Tahun { get; private set; }
        public int StageId { get; private set; }
        public int RiskMatrixProjectId { get; private set; }

        public StageTahunRiskMatrixLite(int tahun, int stageId, int riskMatrixProjectId)
        {
            this.Tahun = tahun;
            this.StageId = stageId;
            this.RiskMatrixProjectId = riskMatrixProjectId;
        }
    }
}
