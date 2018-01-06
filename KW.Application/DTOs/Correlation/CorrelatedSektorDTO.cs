using KW.Domain;
using System;
using System.Collections.Generic;

namespace KW.Application.DTO
{
    public class CorrelatedSektorDTO
    {
        public int Id { get; set; }
        public string NamaSektor { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }
        public IList<RiskRegistrasiDTO> RiskRegistrasi { get; set; }
        public IList<CorrelationMatrixDTO> CorrelationMatrix { get; set; }


        public CorrelatedSektorDTO(CorrelatedSektor model, IList<RiskRegistrasi> riskRegistrasi, IList<CorrelationMatrix> correlationMatrix)
        {
            if (model == null) return;

            this.Id = model.Id;
            this.NamaSektor = model.NamaSektor;
            this.CreateBy = model.CreateBy;
            this.CreateDate = model.CreateDate;
            this.UpdateBy = model.UpdateBy;
            this.UpdateDate = model.UpdateDate;
            this.IsDelete = model.IsDelete;
            this.DeleteDate = model.DeleteDate;

            if(riskRegistrasi.Count > 0)
            {
                IList<RiskRegistrasiDTO> riskDTO = RiskRegistrasiDTO.From(riskRegistrasi);
                this.RiskRegistrasi = riskDTO;
            }

            if(correlationMatrix.Count > 0)
            {
                IList<CorrelationMatrixDTO> corMatDTO = CorrelationMatrixDTO.From(correlationMatrix);
                this.CorrelationMatrix = corMatDTO;
            }
        }

        public static CorrelatedSektorDTO From(CorrelatedSektor model, IList<RiskRegistrasi> riskRegistrasi, IList<CorrelationMatrix> correlationMatrix)
        {
            return new CorrelatedSektorDTO(model, riskRegistrasi, correlationMatrix);
        }

        public static IList<CorrelatedSektorDTO> From(IList<CorrelatedSektor> collection, IList<RiskRegistrasi> riskRegistrasi, IList<CorrelationMatrix> correlationMatrix)
        {
            IList<CorrelatedSektorDTO> colls = new List<CorrelatedSektorDTO>();
            foreach (var item in collection)
            {
                colls.Add(new CorrelatedSektorDTO(item, riskRegistrasi, correlationMatrix));
            }
            return colls;
        }
    }
}
