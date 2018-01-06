using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Application.DTO
{
    public class StageTahunRiskMatrixDTO
    {
        public int Id { get; set; }
        public int StageId { get; set; }
        public int RiskMatrixProjectId { get; set; }
        public int? Tahun { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public bool? IsUpdate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }

        public RiskMatrixProjectDTO RiskMatrixProject { get; set; }
        public StageDTO Stage { get; set; }

        public StageTahunRiskMatrixDTO(StageTahunRiskMatrix model, IList<RiskRegistrasi> riskRegistrasi)
        {
            if (model == null) return;

            this.Id = model.Id;
            this.Tahun = model.Tahun;
            this.CreateBy = model.CreateBy;
            this.CreateDate = model.CreateDate;
            this.UpdateBy = model.UpdateBy;
            this.IsUpdate = model.IsUpdate;
            this.UpdateDate = model.UpdateDate;
            this.IsDelete = model.IsDelete;
            this.DeleteDate = model.DeleteDate;

            if (model.RiskMatrixProject != null)
            {
                RiskMatrixProjectDTO riskMatrixProjectDTO = RiskMatrixProjectDTO.From(model.RiskMatrixProject, riskRegistrasi);
                this.RiskMatrixProject = riskMatrixProjectDTO;
                this.RiskMatrixProjectId = riskMatrixProjectDTO.Id;
            }

            if (model.Stage != null)
            {
                StageDTO stageDTO = StageDTO.From(model.Stage);
                this.Stage = stageDTO;
                this.StageId = stageDTO.Id;
            }
        }

        public static StageTahunRiskMatrixDTO From(StageTahunRiskMatrix model, IList<RiskRegistrasi> riskRegistrasi)
        {
            return new StageTahunRiskMatrixDTO(model, riskRegistrasi);
        }

        public static IList<StageTahunRiskMatrixDTO> From(IList<StageTahunRiskMatrix> collection, IList<RiskRegistrasi> riskRegistrasi)
        {
            IList<StageTahunRiskMatrixDTO> colls = new List<StageTahunRiskMatrixDTO>();
            foreach (var item in collection)
            {
                colls.Add(new StageTahunRiskMatrixDTO(item, riskRegistrasi));
            }
            return colls;
        }
    }

    public class StageTahunRiskMatrixLightDTO
    {
        public int Id { get; set; }
        public int StageId { get; set; }
        //public int RiskMatrixProjectId { get; set; }
        public int? Tahun { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public bool? IsUpdate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }

        //public RiskMatrixProjectDTO RiskMatrixProject { get; set; }
        public StageDTO Stage { get; set; }

        public StageTahunRiskMatrixLightDTO(StageTahunRiskMatrix model)
        {
            if (model == null) return;

            this.Id = model.Id;
            this.Tahun = model.Tahun;
            this.CreateBy = model.CreateBy;
            this.CreateDate = model.CreateDate;
            this.UpdateBy = model.UpdateBy;
            this.IsUpdate = model.IsUpdate;
            this.UpdateDate = model.UpdateDate;
            this.IsDelete = model.IsDelete;
            this.DeleteDate = model.DeleteDate;

           
            if (model.Stage != null)
            {
                StageDTO stageDTO = StageDTO.From(model.Stage);
                this.Stage = stageDTO;
                this.StageId = stageDTO.Id;
            }
        }

        public static StageTahunRiskMatrixLightDTO From(StageTahunRiskMatrix model)
        {
            return new StageTahunRiskMatrixLightDTO(model);
        }

        public static IList<StageTahunRiskMatrixLightDTO> From(IList<StageTahunRiskMatrix> collection)
        {
            IList<StageTahunRiskMatrixLightDTO> colls = new List<StageTahunRiskMatrixLightDTO>();
            foreach (var item in collection)
            {
                colls.Add(new StageTahunRiskMatrixLightDTO(item));
            }
            return colls;
        }
    }
}
