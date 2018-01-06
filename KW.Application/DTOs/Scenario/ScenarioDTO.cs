using KW.Domain;
using System;
using System.Collections.Generic;

namespace KW.Application.DTO
{
    public class ScenarioDTO
    {
        public int Id { get; set; }
        public string NamaScenario { get; set; }
        public int LikehoodId { get; set; }
        public string NamaLikehood { get; set; }
        public bool IsDefault { get; set; }
        public int? Status { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }
        public LikehoodDTO Likehood { get; set; }
        public IList<ScenarioDetailDTO> ScenarioDetail { get; set; }
        public IList<ProjectDTO> Project { get; set; }

        public ScenarioDTO(Scenario model)
        {
            if (model == null) return;

            this.Id = model.Id;
            this.NamaScenario = model.NamaScenario;
            this.CreateBy = model.CreateBy;
            this.CreateDate = model.CreateDate;
            this.UpdateBy = model.UpdateBy;
            this.UpdateDate = model.UpdateDate;
            this.IsDelete = model.IsDelete;
            this.DeleteDate = model.DeleteDate;
            this.Status = model.Status;
            this.IsDefault = model.IsDefault;

            if(model.Likehood != null)
            {
                LikehoodDTO likehoodDTO = LikehoodDTO.From(model.Likehood);
                this.Likehood = likehoodDTO;
                this.LikehoodId = likehoodDTO.Id;
                this.NamaLikehood = likehoodDTO.NamaLikehood;
            }
            
            if(model.ScenarioDetail != null)
            {
                IList<ScenarioDetailDTO> scenarioDetailDTO = ScenarioDetailDTO.From(model.ScenarioDetail);
                this.ScenarioDetail = scenarioDetailDTO;
            }

            this.Project = new List<ProjectDTO>();
            if (model.ScenarioDetail != null)
            {
                foreach (var item in this.ScenarioDetail)
                {
                    this.Project.Add(item.Project);
                }
            }
        }

        public static ScenarioDTO From(Scenario model)
        {
            return new ScenarioDTO(model);
        }

        public static IList<ScenarioDTO> From(IList<Scenario> collection)
        {
            IList<ScenarioDTO> colls = new List<ScenarioDTO>();
            foreach (var item in collection)
            {
                colls.Add(new ScenarioDTO(item));
            }
            return colls;
        }
    }
}
