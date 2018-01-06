using KW.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public class RiskMatrix : Entity
    {
        public int? CreateBy { get; private set; }
        public DateTime? CreateDate { get; private set; }
        public int? UpdateBy { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public bool? IsDelete { get; private set; }
        public DateTime? DeleteDate { get; private set; }

        //Foreign Key
        public int ScenarioId { get; private set; }

        // Navigation properties
        public virtual Scenario Scenario { get; set; }

        public RiskMatrix()
        { }

        public RiskMatrix(Scenario Scenario, int? createBy, DateTime? createDate)
        {
            this.ScenarioId = Scenario.Id;
            this.CreateBy = createBy;
            this.CreateDate = createDate;
            this.IsDelete = false;
        }

        public virtual void Update(Scenario Scenario, int? updateBy, DateTime? updateDate)
        {
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
    }
}
