using KW.Domain;
using System;
using System.Collections.Generic;

namespace KW.Application.DTO
{
    public class CorrelatedSektorDetailDTO
    {
        public int Id { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }
        public int CorrelatedSektorId{ get; set; }
        public string NamaSektor { get; set; }
        public int RiskRegistrasiIdRow { get; set; }
        public string KodeMRiskRow { get; set; }
        public int RiskRegistrasiIdCol { get; set; }
        public string KodeMRiskCol { get; set; }
        public int CorrelationMatrixId { get; set; }
        public string NamaCorrelationMatrix { get; set; }
        public IList<RiskRegistrasiDTO> RiskRegistrasi { get; set; }

        public CorrelatedSektorDetailDTO(CorrelatedSektorDetail model, IList<RiskRegistrasi> riskRegistrasi)
        {
            if (model == null) return;

            this.Id = model.Id;
            this.CreateBy = model.CreateBy;
            this.CreateDate = model.CreateDate;
            this.UpdateBy = model.UpdateBy;
            this.UpdateDate = model.UpdateDate;
            this.IsDelete = model.IsDelete;
            this.DeleteDate = model.DeleteDate;
            this.CorrelatedSektorId = model.CorrelatedSektorId;
            this.NamaSektor = model.CorrelatedSektor.NamaSektor;
            this.CorrelationMatrixId = model.CorrelationMatrix.Id;
            this.NamaCorrelationMatrix = model.CorrelationMatrix.NamaCorrelationMatrix;
            this.RiskRegistrasiIdRow = model.RiskRegistrasiIdRow;
            //this.KodeMRiskRow = model.RiskRegistrasi.KodeMRisk;
            this.RiskRegistrasiIdCol = model.RiskRegistrasiIdCol;
            //this.KodeMRiskCol = model.RiskRegistrasi.KodeMRisk;

            if(riskRegistrasi.Count > 0)
            {
                IList<RiskRegistrasiDTO> riskDTO = RiskRegistrasiDTO.From(riskRegistrasi);
                this.RiskRegistrasi = riskDTO;
            }
        }

        public static CorrelatedSektorDetailDTO From(CorrelatedSektorDetail model, IList<RiskRegistrasi> riskRegistrasi)
        {
            return new CorrelatedSektorDetailDTO(model, riskRegistrasi);
        }

        public static IList<CorrelatedSektorDetailDTO> From(IList<CorrelatedSektorDetail> collection, IList<RiskRegistrasi> riskRegistrasi)
        {
            IList<CorrelatedSektorDetailDTO> colls = new List<CorrelatedSektorDetailDTO>();
            foreach (var item in collection)
            {
                colls.Add(new CorrelatedSektorDetailDTO(item, riskRegistrasi));
            }
            return colls;
        }
    }
}
