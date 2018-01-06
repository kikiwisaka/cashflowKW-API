using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Application.DTO
{
    public class MaksimumProjectValueDTO
    {
        public int Id { get; set; }
        public int ScenarioId { get; set; }
        public int ProjectId { get; set; }
        public int Tahun { get; set; }
        public decimal NilaiMaximum { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }

        public ScenarioDTO Scenario { get; set; }
        public ProjectDTO Project { get; set; }

        public MaksimumProjectValueDTO(MaksimumProjectValue model)
        {
            if (model == null) return;

            this.Id = model.Id;
            this.Tahun = model.Tahun;
            this.NilaiMaximum = model.NilaiMaximum;
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
            }

            {
                ProjectDTO projectDTO = ProjectDTO.From(model.Project);
                this.Project = projectDTO;
                this.ProjectId = projectDTO.Id;
            }
        }

        public static MaksimumProjectValueDTO From(MaksimumProjectValue model)
        {
            return new MaksimumProjectValueDTO(model);
        }

        public static IList<MaksimumProjectValueDTO> From(IList<MaksimumProjectValue> collection)
        {
            IList<MaksimumProjectValueDTO> colls = new List<MaksimumProjectValueDTO>();
            foreach (var item in collection)
            {
                colls.Add(new MaksimumProjectValueDTO(item));
            }
            return colls;
        }
    }
}
