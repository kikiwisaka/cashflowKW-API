using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Application.DTO
{
    public class RiskMatrixStageDTO
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int RiskMatrixId { get; set; }
        public int StageId { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }

        public ProjectDTO Project { get; set; }
        public RiskMatrixDTO RiskMatrix { get; set; }
        public StageDTO Stage { get; set; }

        public RiskMatrixStageDTO(RiskMatrixStage model)
        {
            if (model == null) return;

            this.Id = model.Id;
            this.CreateBy = model.CreateBy;
            this.CreateDate = model.CreateDate;
            this.UpdateBy = model.UpdateBy;
            this.UpdateDate = model.UpdateDate;
            this.IsDelete = model.IsDelete;
            this.DeleteDate = model.DeleteDate;

            if (model.Project != null)
            {
                ProjectDTO projectDTO = ProjectDTO.From(model.Project);
                this.Project = projectDTO;
                this.ProjectId = projectDTO.Id;
            }

            if (model.RiskMatrix != null)
            {
                RiskMatrixDTO riskMatrixDTO = RiskMatrixDTO.From(model.RiskMatrix);
                this.RiskMatrix = riskMatrixDTO;
                this.RiskMatrixId = riskMatrixDTO.Id;
            }

            if (model.Stage != null)
            {
                StageDTO stageDTO = StageDTO.From(model.Stage);
                this.Stage = stageDTO;
                this.StageId = stageDTO.Id;
            }
        }

        public static RiskMatrixStageDTO From(RiskMatrixStage model)
        {
            return new RiskMatrixStageDTO(model);
        }

        public static IList<RiskMatrixStageDTO> From(IList<RiskMatrixStage> collection)
        {
            IList<RiskMatrixStageDTO> colls = new List<RiskMatrixStageDTO>();
            foreach (var item in collection)
            {
                colls.Add(new RiskMatrixStageDTO(item));
            }
            return colls;
        }
    }
}
