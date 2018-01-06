using KW.Core;
using System;

namespace KW.Domain
{
    public class CorrelatedSektor : Entity
    {
        public string NamaSektor { get; private set; }
        public int? CreateBy { get; private set; }
        public DateTime? CreateDate { get; private set; }
        public int? UpdateBy { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public bool? IsDelete { get; private set; }
        public DateTime? DeleteDate { get; private set; }

        //Foreign Key
        public int ScenarioId { get; private set; }


        public CorrelatedSektor()
        { }

        public CorrelatedSektor(string namaSektor, Scenario scenario, int? createBy, DateTime? createDate)
        {
            this.NamaSektor = namaSektor;
            this.ScenarioId = scenario.Id;
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

        public virtual void Delete(int deleteBy, DateTime deleteDate)
        {
            this.IsDelete = true;
            this.UpdateBy = deleteBy;
            this.DeleteDate = deleteDate;
        }
    }
}
