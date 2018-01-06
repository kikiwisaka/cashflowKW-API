using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Application.DTO
{
    public class RiskMatrixDTO
    {
        public int Id { get; set; }
        public int ScenarioId { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }

        public ScenarioDTO Scenario { get; set; }
        public IList<ProjectDTO> Project { get; set; }

        public RiskMatrixDTO(RiskMatrix model)
        {
            if (model == null) return;

            this.Id = model.Id;
            this.CreateBy = model.CreateBy;
            this.CreateDate = model.CreateDate;
            this.UpdateBy = model.UpdateBy;
            this.UpdateDate = model.UpdateDate;
            this.IsDelete = model.IsDelete;
            this.DeleteDate = model.DeleteDate;

            if (model.Scenario != null)
            {
                ScenarioDTO scenarioDTO = ScenarioDTO.From(model.Scenario);
                this.Scenario = scenarioDTO;
                this.ScenarioId = scenarioDTO.Id;
                this.Project = scenarioDTO.Project;
            }
        }

        public static RiskMatrixDTO From(RiskMatrix model)
        {
            return new RiskMatrixDTO(model);
        }

        public static IList<RiskMatrixDTO> From(IList<RiskMatrix> collection)
        {
            IList<RiskMatrixDTO> colls = new List<RiskMatrixDTO>();
            foreach (var item in collection)
            {
                colls.Add(new RiskMatrixDTO(item));
            }
            return colls;
        }
    }
}
