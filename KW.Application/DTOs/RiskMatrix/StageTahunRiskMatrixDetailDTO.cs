using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Application.DTO
{
    public class StageTahunRiskMatrixDetailDTO
    {
        public int Id { get; set; }
        public int StageTahunRiskMatrixId { get; set; }
        public int RiskMatrixId { get; set; }
        public int LikehoodDetailId { get; set; }
        public int RiskRegistrasiId { get; set; }
        public string KodeMRisk { get; set; }
        public string NamaCategoryRisk { get; set; }
        public decimal? NilaiExpose { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }

        public LikehoodDetailDTO LikehoodDetail { get; set; }
        public RiskRegistrasiDTO RiskRegistrasi { get; set; }

        public StageTahunRiskMatrixDetailDTO(StageTahunRiskMatrixDetail model)
        {
            if (model == null) return;

            this.Id = model.Id;
            this.StageTahunRiskMatrixId = model.StageTahunRiskMatrixId;
            this.CreateBy = model.CreateBy;
            this.CreateDate = model.CreateDate;
            this.UpdateBy = model.UpdateBy;
            this.UpdateDate = model.UpdateDate;
            this.IsDelete = model.IsDelete;
            this.DeleteDate = model.DeleteDate;
            this.LikehoodDetailId = model.LikehoodDetailId;

            if (model.RiskRegistrasi != null)
            {
                RiskRegistrasiDTO riskRegistrasiDTO = RiskRegistrasiDTO.From(model.RiskRegistrasi);
                this.RiskRegistrasi = riskRegistrasiDTO;
                this.RiskRegistrasiId = riskRegistrasiDTO.Id;
                this.KodeMRisk = riskRegistrasiDTO.KodeMRisk;
                this.NamaCategoryRisk = riskRegistrasiDTO.NamaCategoryRisk;
            }

            //if(model.LikehoodDetail != null)
            //{
            //    LikehoodDetailDTO likehoodDetailDTO = LikehoodDetailDTO.From(model)
            //}
        }

        public static StageTahunRiskMatrixDetailDTO From(StageTahunRiskMatrixDetail model)
        {
            return new StageTahunRiskMatrixDetailDTO(model);
        }

        public static IList<StageTahunRiskMatrixDetailDTO> From(IList<StageTahunRiskMatrixDetail> collection)
        {
            IList<StageTahunRiskMatrixDetailDTO> colls = new List<StageTahunRiskMatrixDetailDTO>();
            foreach (var item in collection)
            {
                colls.Add(new StageTahunRiskMatrixDetailDTO(item));
            }
            return colls;
        }
    }
}
