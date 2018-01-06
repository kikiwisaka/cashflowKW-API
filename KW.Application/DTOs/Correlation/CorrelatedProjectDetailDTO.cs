using KW.Domain;
using System;
using System.Collections.Generic;

namespace KW.Application.DTO
{
    public class CorrelatedProjectDetailDTO
    {
        public int Id { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }
        public int CorrelatedProjectId { get; set; }
        public int ProjectIdRow { get; set; }
        public string NamaProjectRow { get; set; }
        public int ProjectiIdCol { get; set; }
        public string NamaProjectCol { get; set; }
        public int CorrelationMatrixId { get; set; }
        public string NamaCorrelationMatrix { get; set; }
        public IList<ProjectDTO> Project { get; set; }

        public CorrelatedProjectDetailDTO(CorrelatedProjectDetail model, IList<Project> project)
        {
            if (model == null) return;

            this.Id = model.Id;
            this.CreateBy = model.CreateBy;
            this.CreateDate = model.CreateDate;
            this.UpdateBy = model.UpdateBy;
            this.UpdateDate = model.UpdateDate;
            this.IsDelete = model.IsDelete;
            this.DeleteDate = model.DeleteDate;
            this.CorrelatedProjectId = model.CorrelatedProjectId;
            this.CorrelationMatrixId = model.CorrelationMatrix.Id;
            this.NamaCorrelationMatrix = model.CorrelationMatrix.NamaCorrelationMatrix;
            this.ProjectIdRow = model.ProjectIdRow;
            this.ProjectiIdCol= model.ProjectIdCol;

            if(project.Count > 0)
            {
                IList<ProjectDTO> dto = ProjectDTO.From(project);
                this.Project = dto;
            }
        }

        public static CorrelatedProjectDetailDTO From(CorrelatedProjectDetail model, IList<Project> project)
        {
            return new CorrelatedProjectDetailDTO(model, project);
        }

        public static IList<CorrelatedProjectDetailDTO> From(IList<CorrelatedProjectDetail> collection, IList<Project> project)
        {
            IList<CorrelatedProjectDetailDTO> colls = new List<CorrelatedProjectDetailDTO>();
            foreach (var item in collection)
            {
                colls.Add(new CorrelatedProjectDetailDTO(item, project));
            }
            return colls;
        }
    }
}
