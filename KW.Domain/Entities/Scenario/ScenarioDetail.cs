using KW.Core;
using System;
using System.Collections.Generic;

namespace KW.Domain
{
    public class ScenarioDetail : Entity
    {
        public int ScenarioId { get; private set; }
        public int? Status{ get; private set; }
        public int? CreateBy { get; private set; }
        public DateTime? CreateDate { get; private set; }
        public int? UpdateBy { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public bool? IsDelete { get; private set; }
        public DateTime? DeleteDate { get; private set; }

        // Foreign key
        public int ProjectId { get; private set; }

        // Navigation properties
        public virtual Project Project { get; set; }


        public ScenarioDetail()
        {
        }

        public ScenarioDetail(Project project, int scenarioId, int? createBy, DateTime? createDate)
        {
            this.ScenarioId = scenarioId;
            this.ProjectId = project.Id;
            this.IsDelete = false;
            this.CreateBy = createBy;
            this.CreateDate = createDate;
        }

        public virtual void Update(Project project, int scenarioId, int? updateBy, DateTime? updateDate)
        {
            this.ScenarioId = scenarioId;
            this.ProjectId = project.Id;
            this.UpdateBy= updateBy;
            this.UpdateDate= updateDate;
        }

        public virtual void Delete(int? deleteBy, DateTime? deleteDate)
        {
            this.IsDelete = true;
            this.UpdateBy = deleteBy;
            this.DeleteDate = deleteDate;
        }
    }
}
