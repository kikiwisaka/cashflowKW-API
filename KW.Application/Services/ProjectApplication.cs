using KW.Common.Validation;
using System;
using System.Collections.Generic;
using KW.Application.Params;
using KW.Core;
using KW.Domain;
using System.Linq;

namespace KW.Application
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProjectRepository _projectRepository;
        private readonly ITahapanRepository _tahapanRepository;
        private readonly ISektorRepository _sektorRepository;
        private readonly IRiskRegistrasiRepository _riskRegistrasiRepository;
        private readonly IProjectRiskRegistrasiRepository _projectRiskRegistrasiRepository;
        private readonly IProjectRiskStatusRepository _projectRiskStatusRepository;

        public ProjectService(IUnitOfWork uow, IProjectRepository projectRepository, ITahapanRepository tahapanRepository, ISektorRepository sektorRepository, IRiskRegistrasiRepository riskRegistrasiRepository, 
            IProjectRiskRegistrasiRepository projectRiskRegistrasiRepository, IProjectRiskStatusRepository projectRiskStatusRepository)
        {
            _unitOfWork = uow;
            _projectRepository = projectRepository;
            _tahapanRepository = tahapanRepository;
            _sektorRepository = sektorRepository;
            _riskRegistrasiRepository = riskRegistrasiRepository;
            _projectRiskRegistrasiRepository = projectRiskRegistrasiRepository;
            _projectRiskStatusRepository = projectRiskStatusRepository;
        }

        #region Query
        public IEnumerable<Project> GetAll()
        {
            return _projectRepository.GetAll();
        }

        public Project Get(int id)
        {
            return _projectRepository.Get(id);
        }

        public void IsExistOnEditing(int id, string namaProject, string keterangan)
        {
            if (_projectRepository.IsExist(id, namaProject))
            {
                throw new ApplicationException(string.Format("Nama Project {0} sudah ada.", namaProject));
            }
        }

        public void isExistOnAdding(string namaProject)
        {
            if (_projectRepository.IsExist(namaProject))
            {
                throw new ApplicationException(string.Format("Nama Project {0} sudah ada.", namaProject));
            }
        }
        #endregion Query

        #region Manipulation
        public int Add(ProjectParam param)
        {
            int id;
            Validate.NotNull(param.NamaProject, "Nama Project is required.");
            Validate.NotNull(param.TahunAwalProject, "Awal Project is required.");
            Validate.NotNull(param.TahunAkhirProject, "Akhir Project is required.");
            Validate.NotNull(param.Minimum, "Minimum is required.");
            Validate.NotNull(param.Maximum, "Maximum is required.");
            Validate.NotNull(param.RiskRegistrasiId, "Risk Category is required.");

            Tahapan tahapan = _tahapanRepository.Get(param.TahapanId);
            Validate.NotNull(tahapan, "Tahapan is required but not found in database.");

            Sektor sektor = _sektorRepository.Get(param.SektorId);
            Validate.NotNull(sektor, "Sektor is required but not found in database.");

            isExistOnAdding(param.NamaProject);
            using (_unitOfWork)
            {
                Project model = new Project(tahapan, sektor, param.NamaProject, param.TahunAwalProject, param.TahunAkhirProject, param.CreateBy, param.TahapanId, param.Minimum,
                    param.Maximum, param.SektorId, param.Keterangan, param.CreateBy, param.CreateDate);
                _projectRepository.Insert(model);

                IList<ProjectRiskRegistrasi> projectRisks = GenereateProjectRiskRegistrasi(param, model);
                _projectRiskRegistrasiRepository.Insert(projectRisks);

                _unitOfWork.Commit();
                id = model.Id;
            }

            return id;
        }

        public int Update(int id, ProjectParam param)
        {
            int modelId;

            Project model = Get(id);

            Validate.NotNull(param.NamaProject, "Nama Project is required.");
            Validate.NotNull(param.TahunAwalProject, "Awal Project is required.");
            Validate.NotNull(param.TahunAkhirProject, "Akhir Project is required.");
            Validate.NotNull(param.Minimum, "Minimum is required.");
            Validate.NotNull(param.Maximum, "Maximum is required.");
            Validate.NotNull(param.RiskRegistrasiId, "Risk Category is required.");

            Tahapan tahapan = _tahapanRepository.Get(param.TahapanId);
            Validate.NotNull(tahapan, "Tahapan is required but not found in database.");

            Sektor sektor = _sektorRepository.Get(param.SektorId);
            Validate.NotNull(sektor, "Sektor is required but not found in database.");

            var projectRisk = _projectRiskRegistrasiRepository.GetByProjectId(id);
            Validate.NotNull(projectRisk, "Risk Category is required but not found in database.");

            IsExistOnEditing(id, param.NamaProject, param.Keterangan);

            IList<ProjectRiskStatus> riskStatus = new List<ProjectRiskStatus>();
            using (_unitOfWork)
            {
                model.Update(tahapan, sektor, param.NamaProject, param.TahunAwalProject, param.TahunAkhirProject, param.UpdateBy, param.TahapanId, param.Minimum, param.Maximum, param.SektorId, param.Keterangan, param.UpdateBy, param.UpdateDate);
                _projectRepository.Update(model);

                //delete old project risk 
                RemoveOldProjectRisk(id, model);

                //delete current risk status
                RemoveProjectRiskStatus(model);

                //insert new project risk
                IList<ProjectRiskRegistrasi> projectRisks = GenereateProjectRiskRegistrasi(param, model);
                _projectRiskRegistrasiRepository.Insert(projectRisks);

                _unitOfWork.Commit();
                modelId = model.Id;
            }

            return modelId;
        }

        public int Delete(int id, int deleteBy, DateTime deleteDate)
        {
            int modelId;
            Project model = Get(id);
            Validate.NotNull(model, "Project is not found.");

            using (_unitOfWork)
            {
                model.Delete(deleteBy, deleteDate);
                _projectRepository.Update(model);

                //delete old project risk 
                RemoveOldProjectRisk(id, model);

                //delete project risk status
                RemoveProjectRiskStatus(model);

                _unitOfWork.Commit();
                modelId = model.Id;
            }

            return modelId;
        }
        #endregion Manipulation

        #region Private
        private IList<ProjectRiskRegistrasi> GenereateProjectRiskRegistrasi(ProjectParam param, Project model)
        {
            IList<ProjectRiskRegistrasi> projectRisks = new List<ProjectRiskRegistrasi>();
            IList<ProjectRiskStatus> riskStatus = new List<ProjectRiskStatus>();
            List<int> riskRegistrasiIds = new List<int>();

            if(param.RiskRegistrasiId != null)
            {
                for (int i = 0; i < param.RiskRegistrasiId.Length; i++)
                {
                    int n;
                    if (Int32.TryParse(param.RiskRegistrasiId[i], out n))
                    {
                        if (!riskRegistrasiIds.Contains(n))
                            riskRegistrasiIds.Add(n);
                    }
                }

                foreach (var item in riskRegistrasiIds)
                {
                    var risk = _riskRegistrasiRepository.Get(item);
                    Validate.NotNull(risk, "Risk Category id " + item + " is not found.");

                    ProjectRiskRegistrasi projectRisk = new ProjectRiskRegistrasi(model, risk, param.CreateBy, param.CreateDate);
                    projectRisks.Add(projectRisk);

                    ProjectRiskStatus status = new ProjectRiskStatus(model, risk.Id, risk.KodeMRisk, risk.NamaCategoryRisk, risk.Definisi, true);
                    riskStatus.Add(status);
                }

                //Set Project Risk Status for this project
                var dataRiskStatus = GenerateRiskStatus(riskStatus, model);
                model.AddProjectRiskStatus(dataRiskStatus);

                model.AddProjectRisk(projectRisks);
            }
            return projectRisks;
        }

        private void RemoveOldProjectRisk(int id, Project model)
        {
            var projectRisk = _projectRiskRegistrasiRepository.GetByProjectId(id);
            if(projectRisk != null)
            {
                foreach (var item in projectRisk)
                {
                    model.RemoveProjecRisk(item);
                    _projectRiskRegistrasiRepository.DeleteByProjectId(item.Id);
                }
            }
        }

        private IList<ProjectRiskStatus> GenerateRiskStatus(IList<ProjectRiskStatus> status, Project project)
        {
            var allRisks = _riskRegistrasiRepository.GetAll().ToList();
            IList<ProjectRiskStatus> riskStatus = new List<ProjectRiskStatus>();

            for (int i = 0; i < allRisks.Count; i++)
            {
                var filtered = status.Where(x => x.KodeMRisk == allRisks[i].KodeMRisk).FirstOrDefault();
                if(filtered == null)
                {
                    ProjectRiskStatus model = new ProjectRiskStatus(project, allRisks[i].Id, allRisks[i].KodeMRisk, allRisks[i].NamaCategoryRisk, allRisks[i].Definisi, false);
                    status.Add(model);
                }
            }

            return status.OrderBy(x => x.KodeMRisk).ToList();
        }

        private void RemoveProjectRiskStatus(Project model)
        {
            var riskStatus = _projectRiskStatusRepository.GetByProjectId(model.Id);
            if (riskStatus != null)
            {
                foreach (var item in riskStatus)
                {
                    model.RemoveProjecRiskStatus(item);
                    _projectRiskStatusRepository.Delete(item.Id);
                }
            }
        }

        #endregion Private
    }
}
