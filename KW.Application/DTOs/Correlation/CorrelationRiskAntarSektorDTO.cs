using KW.Domain;
using System;
using System.Collections.Generic;

namespace KW.Application.DTO
{
    public class CorrelationRiskAntarSektorDTO
    {
        public int Id { get; set; }
        public int ScenarioId { get; set; }
        public string NamaScenario { get; set; }
        public int? Status { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }
        public ScenarioDTO Scenario { get; set; }
        public int SektorId { get; set; }
        public int ProjectId { get; set; }
        public IList<CorrelationMatrixDTO> CorrelationMatrix{ get; set; }
        public ProjectDTO Project{ get; set; }


        public CorrelationRiskAntarSektorDTO(CorrelationRiskAntarSektor model, IList<CorrelationMatrix> correlationMatrix)
        {
            if (model == null) return;

            this.Id = model.Id;
            this.CreateBy = model.CreateBy;
            this.CreateDate = model.CreateDate;
            this.UpdateBy = model.UpdateBy;
            this.UpdateDate = model.UpdateDate;
            this.IsDelete = model.IsDelete;
            this.DeleteDate = model.DeleteDate;
            this.Status = model.Status;
            this.SektorId = model.SektorId;
            this.ProjectId = model.ProjectId;

            if(model.Scenario != null)
            {
                ScenarioDTO scenarioDTO = ScenarioDTO.From(model.Scenario);
                this.Scenario = scenarioDTO;
                this.ScenarioId = scenarioDTO.Id;
                this.NamaScenario = scenarioDTO.NamaScenario;
            }
            if(correlationMatrix != null)
            {
                IList<CorrelationMatrixDTO> corMatDTOs = CorrelationMatrixDTO.From(correlationMatrix);
                this.CorrelationMatrix = corMatDTOs;
            }
            if(model.Project != null)
            {
                ProjectDTO projectDTO = ProjectDTO.From(model.Project);
                this.Project = projectDTO;
            }
        }

        public static CorrelationRiskAntarSektorDTO From(CorrelationRiskAntarSektor model, IList<CorrelationMatrix> correlationMatrix)
        {
            return new CorrelationRiskAntarSektorDTO(model, correlationMatrix);
        }

        public static IList<CorrelationRiskAntarSektorDTO> From(IList<CorrelationRiskAntarSektor> collection, IList<CorrelationMatrix> correlationMatrix)
        {
            IList<CorrelationRiskAntarSektorDTO> colls = new List<CorrelationRiskAntarSektorDTO>();
            foreach (var item in collection)
            {
                colls.Add(new CorrelationRiskAntarSektorDTO(item, correlationMatrix));
            }
            return colls;
        }
    }
}
