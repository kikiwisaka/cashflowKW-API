using KW.Core;
using System;

namespace KW.Domain
{
    public class SubRiskRegistrasi : Entity
    {
        public string KodeRisk { get; private set; }
        public string RiskEvenClaim { get; private set; }
        public string DescriptionRiskEvenClaim { get; private set; }
        public string SugestionMigration { get; private set; }
        public int? CreateBy { get; private set; }
        public DateTime? CreateDate { get; private set; }
        public int? UpdateBy { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public bool? IsDelete { get; private set; }
        public DateTime? DeleteDate { get; private set; }

        //Foreign  Key
        public int RiskRegistrasiId { get; private set; }

        //Navigation properties
        public virtual RiskRegistrasi RiskRegistrasi { get; private set; }

        public SubRiskRegistrasi()
        { }

        public SubRiskRegistrasi(RiskRegistrasi riskRegistrasi, string kodeRisk, string riskEvenClaim, string descriptionRiskEvenClaim, string sugestionMigration, int? createBy, DateTime? createDate)
        {
            this.RiskRegistrasiId = riskRegistrasi.Id;
            this.KodeRisk = kodeRisk;
            this.RiskEvenClaim = riskEvenClaim;
            this.DescriptionRiskEvenClaim = descriptionRiskEvenClaim;
            this.SugestionMigration = sugestionMigration;
            this.CreateBy = createBy;
            this.CreateDate = createDate;
            this.IsDelete = false;
        }

        public virtual void Update(RiskRegistrasi riskRegistrasi, string kodeRisk, string riskEvenClaim, string descriptionRiskEvenClaim, string sugestionMigration, int? updateBy, DateTime? updateDate)
        {
            this.RiskRegistrasiId = riskRegistrasi.Id;
            this.KodeRisk = kodeRisk;
            this.RiskEvenClaim = riskEvenClaim;
            this.DescriptionRiskEvenClaim = descriptionRiskEvenClaim;
            this.SugestionMigration = sugestionMigration;
            this.UpdateBy = updateBy;
            this.UpdateDate = updateDate;
        }

        public virtual void Delete(int? deleteBy, DateTime? deleteDate)
        {
            this.IsDelete = true;
            this.DeleteDate = deleteDate;
        }
    }
}
