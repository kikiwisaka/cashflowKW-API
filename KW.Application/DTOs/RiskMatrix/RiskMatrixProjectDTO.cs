using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Application.DTO
{
    public class RiskMatrixProjectDTO
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int RiskMatrixId { get; set; }
        public int ScenarioId { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }

        public ProjectDTO Project { get; set; }
        public RiskMatrixDTO RiskMatrix { get; set; }
        public ScenarioDTO Scenario { get; set; }
        public IList<RiskRegistrasiDTO> RiskRegistrasi { get; set; }
        public IList<StageTahunRiskMatrixLightDTO> StageTahunRiskMatrix { get; set; }

        public RiskMatrixProjectDTO(RiskMatrixProject model, IList<RiskRegistrasi> riskRegistrasi)
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

            if (model.Scenario != null)
            {
                ScenarioDTO scenarioDTO = ScenarioDTO.From(model.Scenario);
                this.Scenario = scenarioDTO;
                this.ScenarioId = scenarioDTO.Id;
            }

            if(riskRegistrasi != null)
            {
                IList<RiskRegistrasiDTO> riskRegistrasiDTOs = RiskRegistrasiDTO.From(riskRegistrasi);
                this.RiskRegistrasi = riskRegistrasiDTOs;
            }

            if (model.StageTahunRiskMatrix != null)
            {
                IList<StageTahunRiskMatrixLightDTO> stageTahunRiskMatrixLightDTO = StageTahunRiskMatrixLightDTO.From(model.StageTahunRiskMatrix);
                this.StageTahunRiskMatrix = stageTahunRiskMatrixLightDTO;
            }
        }

        public static RiskMatrixProjectDTO From(RiskMatrixProject model, IList<RiskRegistrasi> riskRegistrasi)
        {
            return new RiskMatrixProjectDTO(model, riskRegistrasi);
        }

        public static IList<RiskMatrixProjectDTO> From(IList<RiskMatrixProject> collection, IList<RiskRegistrasi> riskRegistrasi)
        {
            IList<RiskMatrixProjectDTO> colls = new List<RiskMatrixProjectDTO>();
            foreach (var item in collection)
            {
                colls.Add(new RiskMatrixProjectDTO(item, riskRegistrasi));
            }
            return colls;
        }
    }
}
