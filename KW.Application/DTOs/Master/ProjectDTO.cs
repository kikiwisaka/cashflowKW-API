using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Application.DTO
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string NamaProject { get; set; }
        public DateTime? TahunAwalProject { get; set; }
        public DateTime? TahunAkhirProject { get; set; }
        public int? UserId{ get; set; }
        public int TahapanId { get; set; }
        public string NamaTahapan { get; set; }
        public bool StatusProject { get; set; }
        public decimal Minimum { get; set; }
        public decimal Maximum { get; set; }
        public int SektorId { get; set; }
        public string NamaSektor { get; set; }
        public string Keterangan { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }
        public TahapanDTO Tahapan { get; set; }
        public SektorDTO Sektor{ get; set; }
        public IList<ProjectRiskRegistrasiDTO> ProjectRiskRegistrasi { get; set; }
        public IList<RiskRegistrasiDTO> RiskRegistrasi { get; set; }
        public IList<ProjectRiskStatusDTO> ProjectRiskStatus { get; set; }


        public ProjectDTO(Project model)
        {
            if (model == null) return;

            this.Id = model.Id;
            this.NamaProject = model.NamaProject;
            this.TahunAwalProject = model.TahunAwalProject;
            this.TahunAkhirProject = model.TahunAkhirProject;
            this.UserId = model.UserId;

            this.StatusProject = model.StatusProject;
            this.Minimum = model.Minimum;
            this.Maximum = model.Maximum;
            this.Keterangan = model.Keterangan;
            this.CreateBy = model.CreateBy;
            this.CreateDate = model.CreateDate;
            this.UpdateBy = model.UpdateBy;
            this.UpdateDate = model.UpdateDate;
            this.IsDelete = model.IsDelete;
            this.DeleteDate = model.DeleteDate;

            if(model.Tahapan != null)
            {
                TahapanDTO tahapanDTO = TahapanDTO.From(model.Tahapan);
                this.Tahapan = tahapanDTO;
                this.TahapanId = tahapanDTO.Id;
                this.NamaTahapan = tahapanDTO.NamaTahapan;
            }

            if(model.Sektor != null)
            {
                SektorDTO sektorDTO = SektorDTO.From(model.Sektor);
                this.Sektor = sektorDTO;
                this.SektorId = sektorDTO.Id;
                this.NamaSektor = sektorDTO.NamaSektor;
            }

            if(model.ProjectRiskRegistrasi != null)
            {
                IList<ProjectRiskRegistrasiDTO> projectRiskDTO = ProjectRiskRegistrasiDTO.From(model.ProjectRiskRegistrasi);
                this.ProjectRiskRegistrasi = projectRiskDTO;
            }

            this.RiskRegistrasi = new List<RiskRegistrasiDTO>();

            if(model.ProjectRiskRegistrasi != null)
            {
                foreach (var item in this.ProjectRiskRegistrasi)
                {
                    this.RiskRegistrasi.Add(item.RiskRegistrasi);
                }
            }

            if(model.ProjectRiskStatus != null)
            {
                IList<ProjectRiskStatusDTO> riskStatusDTO = ProjectRiskStatusDTO.From(model.ProjectRiskStatus);
                this.ProjectRiskStatus = riskStatusDTO;
            }
        }

        public static ProjectDTO From(Project model)
        {
            return new ProjectDTO(model);
        }

        public static IList<ProjectDTO> From(IList<Project> collection)
        {
            IList<ProjectDTO> colls = new List<ProjectDTO>();
            foreach (var item in collection)
            {
                colls.Add(new ProjectDTO(item));
            }
            return colls;
        }
    }
}
