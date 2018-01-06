using KW.Core;
using System;
using System.Collections.Generic;

namespace KW.Domain
{
    public class CorrelationRiskAntarSektor : Entity
    {
        public int? Status { get; private set; }
        public int? CreateBy { get; private set; }
        public DateTime? CreateDate { get; private set; }
        public int? UpdateBy { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public bool? IsDelete { get; private set; }
        public DateTime? DeleteDate { get; private set; }

        // Foreign key
        public int ScenarioId { get; private set; }
        public int SektorId { get; private set; }
        public int ProjectId { get; private set; }
        public int? RiskMatrixProjectId { get; private set; }

        // Navigation properties
        public virtual Scenario Scenario { get; set; }
        public virtual Sektor Sektor { get; set; }
        public virtual Project Project { get; set; }

        public CorrelationRiskAntarSektor()
        {
            //this.ScenarioDetail = new List<ScenarioDetail>();
        }

        public CorrelationRiskAntarSektor(Scenario scenario, Sektor sektor, Project project, int? createBy, DateTime? createDate)
        {
            this.ScenarioId = scenario.Id;
            this.SektorId = sektor.Id;
            this.ProjectId = project.Id;
            this.CreateBy = createBy;
            this.CreateDate = createDate;
            this.IsDelete = false;
            this.Status = 1; //1 Save, 2 Submit, 3 Approved, 4 Rejected
        }

        public virtual void Update(Scenario scenario, int? updateBy, DateTime? updateDate)
        {
            this.ScenarioId = scenario.Id;
            this.UpdateBy = updateBy;
            this.UpdateDate = updateDate;
        }

        public virtual void Delete(int deleteBy, DateTime deleteDate)
        {
            this.IsDelete = true;
            this.UpdateBy = deleteBy;
            this.DeleteDate = deleteDate;
        }

        public virtual void AddRiskMatrixProject(int riskMatrixProjectId, int? updateBy, DateTime? updateDate)
        {
            this.RiskMatrixProjectId = riskMatrixProjectId;
            this.UpdateBy = updateBy;
            this.UpdateDate = updateDate;
        }

        //public virtual void AddScenarioDetail(IList<ScenarioDetail> scenarioDetails)
        //{
        //    if (!this.Equals(scenarioDetails))
        //        this.ScenarioDetail = scenarioDetails;
        //}

        //public virtual void RemoveScenarioDetail(ScenarioDetail scenarioDetail)
        //{
        //    this.ScenarioDetail.Remove(scenarioDetail);
        //}
    }
}
