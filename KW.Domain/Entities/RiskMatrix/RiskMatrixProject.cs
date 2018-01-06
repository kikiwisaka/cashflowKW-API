using KW.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public class RiskMatrixProject : Entity
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
        public int ScenarioId { get; private set; }


        // Navigation properties
        public virtual Project Project { get; set; }
        public virtual RiskMatrix RiskMatrix { get; set; }
        public virtual Scenario Scenario { get; set; }
        public virtual IList<StageTahunRiskMatrix> StageTahunRiskMatrix { get; set; }

        public RiskMatrixProject()
        {
            this.StageTahunRiskMatrix = new List<StageTahunRiskMatrix>();
        }

        public RiskMatrixProject(Project Project, RiskMatrix RiskMatrix, Scenario Scenario, int? createBy, DateTime? createDate)
        {
            this.ProjectId = Project.Id;
            this.RiskMatrixId = RiskMatrix.Id;
            this.ScenarioId = Scenario.Id;
            this.CreateBy = createBy;
            this.CreateDate = createDate;
            this.IsDelete = false;
        }

        public virtual void Update(Project Project, RiskMatrix RiskMatrix, Scenario Scenario, int? updateBy, DateTime? updateDate)
        {
            this.ProjectId = Project.Id;
            this.RiskMatrixId = RiskMatrix.Id;
            this.ScenarioId = Scenario.Id;
            this.UpdateBy = updateBy;
            this.UpdateDate = updateDate;
        }

        public virtual void Delete(int? deleteBy, DateTime? deleteDate)
        {
            this.IsDelete = true;
            this.UpdateBy = deleteBy;
            this.DeleteDate = deleteDate;
        }

        public virtual void SetActive(int? updateBy, DateTime? updateDate)
        {
            this.IsDelete = false;
            this.UpdateBy = updateBy;
            this.UpdateDate = updateDate;
        }

        public virtual void AddStageTahun(IList<StageTahunRiskMatrix> stageTahunRiskMatrixs)
        {
            if (!this.Equals(stageTahunRiskMatrixs))
                this.StageTahunRiskMatrix = stageTahunRiskMatrixs;
        }

        public virtual void RemoveStageTahun(StageTahunRiskMatrix stageTahunRiskMatrix)
        {
            this.StageTahunRiskMatrix.Remove(stageTahunRiskMatrix);
        }
    }
}
