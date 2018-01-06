using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Application.DTO
{
    public class SubRiskRegistrasiDTO
    {
        public int Id { get; set; }
        public int RiskRegistrasiId { get; set; }
        public string KodeRisk { get; set; }
        public string RiskEvenClaim { get; set; }
        public string DescriptionRiskEvenClaim { get; set; }
        public string SugestionMigration { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }

        public SubRiskRegistrasiDTO(SubRiskRegistrasi model)
        {
            if (model == null) return;
            
            this.Id = model.Id;
            this.RiskRegistrasiId = model.RiskRegistrasiId;
            this.KodeRisk = model.KodeRisk;
            this.RiskEvenClaim = model.RiskEvenClaim;
            this.DescriptionRiskEvenClaim = model.DescriptionRiskEvenClaim;
            this.SugestionMigration = model.SugestionMigration;
            this.CreateBy = model.CreateBy;
            this.CreateDate = model.CreateDate;
            this.UpdateBy = model.UpdateBy;
            this.UpdateDate = model.UpdateDate;
            this.IsDelete = model.IsDelete;
            this.DeleteDate = model.DeleteDate;
        }

        public static SubRiskRegistrasiDTO From(SubRiskRegistrasi model)
        {
            return new SubRiskRegistrasiDTO(model);
        }

        public static IList<SubRiskRegistrasiDTO> From(IList<SubRiskRegistrasi> collection)
        {
            IList<SubRiskRegistrasiDTO> colls = new List<SubRiskRegistrasiDTO>();
            foreach (var item in collection)
            {
                colls.Add(new SubRiskRegistrasiDTO(item));
            }
            return colls;
        }


    }
}
