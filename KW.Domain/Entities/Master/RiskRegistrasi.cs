using KW.Core;
using System;
using System.Collections.Generic;

namespace KW.Domain
{
    public class RiskRegistrasi : Entity
    {
        public string KodeMRisk { get; private set; }
        public string NamaCategoryRisk { get; private set; }
        public string Definisi { get; private set; }
        public int? CreateBy { get; private set; }
        public DateTime? CreateDate { get; private set; }
        public int? UpdateBy { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public bool? IsDelete { get; private set; }
        public DateTime? DeleteDate { get; private set; }

        //Navigation properties
        public virtual IList<SubRiskRegistrasi> SubRiskRegistrasi { get; set; }

        public RiskRegistrasi()
        {
            this.SubRiskRegistrasi = new List<SubRiskRegistrasi>();
        }

        public RiskRegistrasi(string kodeMRisk, string namaCategoryRisk, string definisi, int? createBy, DateTime? createDate)
        {
            this.KodeMRisk = kodeMRisk;
            this.NamaCategoryRisk = namaCategoryRisk;
            this.Definisi = definisi;
            this.CreateBy = createBy;
            this.CreateDate = createDate;
            this.IsDelete = false;
        }

        public virtual void Update(string kodeMRisk, string namaCategoryRisk, string definisi, int? updateBy, DateTime? updateDate)
        {
            this.KodeMRisk = kodeMRisk;
            this.NamaCategoryRisk = namaCategoryRisk;
            this.Definisi = definisi;
            this.UpdateBy = updateBy;
            this.UpdateDate = updateDate;
        }

        public virtual void Delete(int? deleteBy, DateTime? deleteDate)
        {
            this.IsDelete = true;
            this.DeleteDate = deleteDate;
        }

        public virtual void AddRiskRegistrasiDetail(SubRiskRegistrasi riskRegistrasiDetail)
        {
            this.SubRiskRegistrasi.Add(riskRegistrasiDetail);
        }

        public virtual void RemoveRiskRegistrasiDetail(SubRiskRegistrasi riskRegistrasiDetail)
        {
            this.SubRiskRegistrasi.Remove(riskRegistrasiDetail);
        }
    }
}
