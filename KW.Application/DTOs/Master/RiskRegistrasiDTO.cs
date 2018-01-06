using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Application.DTO
{
    public class RiskRegistrasiDTO
    {
        public int Id { get; set; }
        public string KodeMRisk { get; set; }
        public string NamaCategoryRisk { get; set; }
        public string Definisi { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }
        public IList<SubRiskRegistrasiDTO> SubRiskRegistrasi { get; set; }

        public RiskRegistrasiDTO(RiskRegistrasi model)
        {
            if (model == null) return;
            
            this.Id = model.Id;
            this.KodeMRisk = model.KodeMRisk;
            this.NamaCategoryRisk = model.NamaCategoryRisk;
            this.Definisi = model.Definisi;
            this.CreateBy = model.CreateBy;
            this.CreateDate = model.CreateDate;
            this.UpdateBy = model.UpdateBy;
            this.UpdateDate = model.UpdateDate;
            this.IsDelete = model.IsDelete;
            this.DeleteDate = model.DeleteDate;

            if (model.SubRiskRegistrasi != null)
            {
                IList<SubRiskRegistrasiDTO> riskRegistrasiDetailDTO = SubRiskRegistrasiDTO.From(model.SubRiskRegistrasi);
                this.SubRiskRegistrasi = riskRegistrasiDetailDTO;
            }
        }

        public static RiskRegistrasiDTO From(RiskRegistrasi model)
        {
            return new RiskRegistrasiDTO(model);
        }

        public static IList<RiskRegistrasiDTO> From(IList<RiskRegistrasi> collection)
        {
            IList<RiskRegistrasiDTO> colls = new List<RiskRegistrasiDTO>();
            foreach (var item in collection)
            {
                colls.Add(new RiskRegistrasiDTO(item));
            }
            return colls;
        }
    }
}
