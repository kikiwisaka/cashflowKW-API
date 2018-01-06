using KW.Core;
using System;
using System.Collections.Generic;

namespace KW.Domain
{
    public class Scenario : Entity
    {
        public string NamaScenario{ get; private set; }
        public bool IsDefault { get; private set; }
        public int? Status { get; private set; }
        public int? CreateBy { get; private set; }
        public DateTime? CreateDate { get; private set; }
        public int? UpdateBy { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public bool? IsDelete { get; private set; }
        public DateTime? DeleteDate { get; private set; }

        // Foreign key
        public int LikehoodId { get; private set; }

        // Navigation properties
        public virtual Likehood Likehood { get; set; }
        public virtual IList<ScenarioDetail> ScenarioDetail { get; set; }

        public Scenario()
        {
            this.ScenarioDetail = new List<ScenarioDetail>();
        }

        public Scenario(Likehood Likehood, string namaScenario, int? createBy, DateTime? createDate)
        {
            this.NamaScenario = namaScenario;
            this.LikehoodId = Likehood.Id;
            this.CreateBy = createBy;
            this.CreateDate = createDate;
            this.IsDelete = false;
        }

        public virtual void Update(Likehood Likehood, string namaScenario, int? updateBy, DateTime? updateDate)
        {
            this.NamaScenario = namaScenario;
            this.LikehoodId = Likehood.Id;
            this.UpdateBy = updateBy;
            this.UpdateDate = updateDate;
        }

        public virtual void Delete(int deleteBy, DateTime deleteDate)
        {
            this.IsDelete = true;
            this.UpdateBy = deleteBy;
            this.DeleteDate = deleteDate;
        }

        public virtual void AddScenarioDetail(IList<ScenarioDetail> scenarioDetails)
        {
            if (!this.Equals(scenarioDetails))
                this.ScenarioDetail = scenarioDetails;
        }

        public virtual void RemoveScenarioDetail(ScenarioDetail scenarioDetail)
        {
            this.ScenarioDetail.Remove(scenarioDetail);
        }

        public virtual void SetDefault(int? updateBy, DateTime? updateDate)
        {
            this.IsDefault = true;
            this.UpdateBy = updateBy;
            this.DeleteDate = updateDate;
        }

        public virtual void RemoveDefault(int? updateBy, DateTime? updateDate)
        {
            this.IsDefault = false;
            this.UpdateBy = updateBy;
            this.DeleteDate = updateDate;
        }
    }
}
