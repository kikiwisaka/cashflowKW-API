using KW.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public class MaksimumProjectValue : Entity
    {
        public int Tahun { get; private set; }
        public decimal NilaiMaximum { get; private set; }
        public int? CreateBy { get; private set; }
        public DateTime? CreateDate { get; private set; }
        public int? UpdateBy { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public bool? IsDelete { get; private set; }
        public DateTime? DeleteDate { get; private set; }

        //Foreign Key
        public int ScenarioId { get; private set; }
        public int ProjectId { get; private set; }

        // Navigation properties
        public virtual Scenario Scenario { get; set; }
        public virtual Project Project { get; set; }

        public MaksimumProjectValue()
        { }

        public MaksimumProjectValue(Scenario Scenario, Project Project, int tahun, decimal nilaiMaksimum, int? createBy, DateTime? createDate)
        {
            this.ScenarioId = Scenario.Id;
            this.ProjectId = Project.Id;
            this.Tahun = tahun;
            this.NilaiMaximum = nilaiMaksimum;
            this.CreateBy = createBy;
            this.CreateDate = createDate;
            this.IsDelete = false;
        }

        public virtual void Update(Scenario Scenario, Project Project, int tahun, decimal nilaiMaksimum, int? updateBy, DateTime? updateDate)
        {
            this.ScenarioId = Scenario.Id;
            this.ProjectId = Project.Id;
            this.Tahun = tahun;
            this.NilaiMaximum = nilaiMaksimum;
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
