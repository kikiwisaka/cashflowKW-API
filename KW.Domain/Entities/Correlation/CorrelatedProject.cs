using KW.Core;
using System;

namespace KW.Domain
{
    public class CorrelatedProject : Entity
    {
        public int? CreateBy { get; private set; }
        public DateTime? CreateDate { get; private set; }
        public int? UpdateBy { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public bool? IsDelete { get; private set; }
        public DateTime? DeleteDate { get; private set; }

        //Foreign Key
        public int ScenarioId { get; private set; }
        public int ProjectId { get; private set; }
        public int SektorId { get; private set; }

        public CorrelatedProject()
        { }

        public CorrelatedProject(Scenario scenario, int projectId, int sektorId, int? createBy, DateTime? createDate)
        {
            this.ScenarioId = scenario.Id;
            this.ProjectId = projectId;
            this.SektorId = sektorId;
            this.CreateBy = createBy;
            this.CreateDate = createDate;
            this.IsDelete = false;
        }

        public virtual void Update(Scenario scenario, int? updateBy, DateTime? updateDate)
        {
            this.ScenarioId = scenario.Id;
            this.UpdateBy = updateBy;
            this.UpdateDate = updateDate;
        }
    }
}
