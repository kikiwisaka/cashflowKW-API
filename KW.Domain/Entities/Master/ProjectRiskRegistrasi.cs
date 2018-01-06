using KW.Core;
using System;

namespace KW.Domain
{
    public class ProjectRiskRegistrasi : Entity
    {
        public int? CreateBy { get; private set; }
        public DateTime? CreateDate { get; private set; }
        public int? UpdateBy { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public bool? IsDelete { get; private set; }
        public DateTime? DeleteDate { get; private set; }

        //Foreign Key
        public int ProjectId { get; private set; }
        public int RiskRegistrasiId { get; private set; }

        // Navigation properties
        public virtual Project Project { get; set; }
        public virtual RiskRegistrasi RiskRegistrasi{ get; set; }

        public ProjectRiskRegistrasi()
        {

        }

        public ProjectRiskRegistrasi(Project project, RiskRegistrasi riskRegistrasi, int? createBy, DateTime? createDate)
        {
            this.ProjectId = project.Id;
            this.RiskRegistrasiId = riskRegistrasi.Id;
            this.CreateBy = createBy;
            this.CreateDate = createDate;
            this.IsDelete = false;
        }

        public virtual void Delete(int? deleteBy, DateTime? deleteDate)
        {
            this.IsDelete = true;
            this.UpdateBy = deleteBy;
            this.DeleteDate = deleteDate;
        }
    }
}
