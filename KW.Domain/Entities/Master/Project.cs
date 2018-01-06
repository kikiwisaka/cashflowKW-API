using KW.Core;
using System;
using System.Collections.Generic;

namespace KW.Domain
{
    public class Project : Entity
    {
        public string NamaProject { get; private set; }
        public DateTime TahunAwalProject { get; private set; }
        public DateTime TahunAkhirProject { get; private set; }
        public bool StatusProject { get; private set; }
        public decimal Minimum { get; private set; }
        public decimal Maximum { get; private set; }
        public string Keterangan { get; private set; }
        public int? CreateBy { get; private set; }
        public DateTime? CreateDate { get; private set; }
        public int? UpdateBy { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public bool? IsDelete { get; private set; }
        public DateTime? DeleteDate { get; private set; }
        
        // Foreign key
        public int TahapanId { get; private set; }
        public int SektorId { get; private set; }

        public int? UserId { get; private set; }

        // Navigation properties
        public virtual Tahapan Tahapan { get; set; }
        public virtual Sektor Sektor { get; set; }
        public virtual IList<ProjectRiskRegistrasi> ProjectRiskRegistrasi { get; set; }
        public virtual IList<ProjectRiskStatus> ProjectRiskStatus { get; set; }


        public Project()
        {
            this.ProjectRiskRegistrasi = new List<ProjectRiskRegistrasi>();
            this.ProjectRiskStatus = new List<ProjectRiskStatus>();
        }

        public Project(Tahapan tahapan, Sektor sektor, string namaProject, DateTime tahunAwalProject, DateTime tahunAkhirProject, int? userId, int tahapanId, 
            decimal minimum, decimal maximum, int sektorId, string keterangan, int? createBy, DateTime? createDate)
        {
            this.NamaProject = namaProject;
            this.TahunAwalProject = tahunAwalProject;
            this.TahunAkhirProject = tahunAkhirProject;
            this.UserId = userId;
            this.TahapanId = tahapan.Id;
            this.StatusProject = true;
            this.Minimum = minimum;
            this.Maximum = maximum;
            this.SektorId = sektorId;
            this.Keterangan = keterangan;
            this.CreateBy = createBy;
            this.CreateDate = createDate;
            this.IsDelete = false;
        }

        public virtual void Update(Tahapan tahapan, Sektor sektor, string namaProject, DateTime tahunAwalProject, DateTime tahunAkhirProject, int? userId, int tahapanId, decimal minimum, decimal maximum, int sektorId, string keterangan, int? updateBy, DateTime? updateDate)
        {
            this.NamaProject = namaProject;
            this.TahunAwalProject = tahunAwalProject;
            this.TahunAkhirProject = tahunAkhirProject;
            this.UserId = userId;
            this.TahapanId = tahapan.Id;
            this.StatusProject = true;
            this.Minimum = minimum;
            this.Maximum = maximum;
            this.SektorId = sektor.Id;
            this.Keterangan = keterangan;
            this.UpdateBy = updateBy;
            this.UpdateDate = updateDate;
        }

        public virtual void Delete(int deleteBy, DateTime deleteDate)
        {
            this.IsDelete = true;
            this.DeleteDate = deleteDate;
        }

        public virtual void AddProjectRisk(IList<ProjectRiskRegistrasi> projectRisks)
        {
            if (!this.Equals(projectRisks))
                this.ProjectRiskRegistrasi = projectRisks;
        }

        public virtual void RemoveProjecRisk(ProjectRiskRegistrasi projectRisks)
        {
            this.ProjectRiskRegistrasi.Remove(projectRisks);
        }

        public virtual void AddProjectRiskStatus(IList<ProjectRiskStatus> status)
        {
            if (!this.Equals(status))
                this.ProjectRiskStatus = status;
        }

        public virtual void RemoveProjecRiskStatus(ProjectRiskStatus status)
        {
            this.ProjectRiskStatus.Remove(status);
        }
    }
}
