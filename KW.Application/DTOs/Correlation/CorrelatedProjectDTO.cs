using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KW.Application.DTO
{
    public class CorrelatedProjectDTO
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string NamaProject { get; set; }
        public int SektorId { get; set; }
        public string NamaSektor { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }
        public IList<CorrelationMatrixDTO> CorrelationMatrix { get; set; }
        public IList<ProjectDTO> Project { get; set; }

        public CorrelatedProjectDTO(CorrelatedProject model, IList<CorrelationMatrix> correlationMatrix, IList<Project> project)
        {
            if (model == null) return;
            var dataProject = project.Where(x => x.Id == model.ProjectId).FirstOrDefault();

            this.Id = model.Id;
            this.ProjectId = model.ProjectId;
            this.NamaProject = dataProject.NamaProject;
            this.SektorId = model.SektorId;
            this.NamaSektor = dataProject.Sektor.NamaSektor;
            this.CreateBy = model.CreateBy;
            this.CreateDate = model.CreateDate;
            this.UpdateBy = model.UpdateBy;
            this.UpdateDate = model.UpdateDate;
            this.IsDelete = model.IsDelete;
            this.DeleteDate = model.DeleteDate;

            if (correlationMatrix.Count > 0)
            {
                IList<CorrelationMatrixDTO> corMatDTO = CorrelationMatrixDTO.From(correlationMatrix);
                this.CorrelationMatrix = corMatDTO;
            }

            if(project.Count > 0)
            {
                IList<ProjectDTO> projectDTO = ProjectDTO.From(project);
                this.Project = projectDTO;
            }
        }

        public static CorrelatedProjectDTO From(CorrelatedProject model, IList<CorrelationMatrix> correlationMatrix, IList<Project> project)
        {
            return new CorrelatedProjectDTO(model, correlationMatrix, project);
        }

        public static IList<CorrelatedProjectDTO> From(IList<CorrelatedProject> collection, IList<CorrelationMatrix> correlationMatrix, IList<Project> project)
        {
            IList<CorrelatedProjectDTO> colls = new List<CorrelatedProjectDTO>();
            foreach (var item in collection)
            {
                colls.Add(new CorrelatedProjectDTO(item, correlationMatrix, project));
            }
            return colls;
        }
    }
}
