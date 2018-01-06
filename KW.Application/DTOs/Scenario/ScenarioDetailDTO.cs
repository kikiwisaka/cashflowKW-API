using KW.Domain;
using System;
using System.Collections.Generic;

namespace KW.Application.DTO
{
    public class ScenarioDetailDTO
    {
        public int Id { get; set; }
        public int ScenarioId { get; set; }
        public string NamaScenario{ get; set; }
        public int ProjectId{ get; set; }
        public string NamaProject { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }
        public ProjectDTO Project { get; set; }

        public ScenarioDetailDTO(ScenarioDetail model)
        {
            if (model == null) return;

            this.Id = model.Id;
            this.ScenarioId = model.ScenarioId;
            this.ProjectId = model.ProjectId;
            this.CreateBy = model.CreateBy;
            this.CreateDate = model.CreateDate;
            this.UpdateBy = model.UpdateBy;
            this.UpdateDate = model.UpdateDate;
            this.IsDelete = model.IsDelete;
            this.DeleteDate = model.DeleteDate;

            if(model.Project != null)
            {
                this.Project = ProjectDTO.From(model.Project);
            }
        }

        public static ScenarioDetailDTO From(ScenarioDetail model)
        {
            return new ScenarioDetailDTO(model);
        }

        public static IList<ScenarioDetailDTO> From(IList<ScenarioDetail> collection)
        {
            IList<ScenarioDetailDTO> colls = new List<ScenarioDetailDTO>();
            foreach (var item in collection)
            {
                colls.Add(new ScenarioDetailDTO(item));
            }
            return colls;
        }
    }
}
