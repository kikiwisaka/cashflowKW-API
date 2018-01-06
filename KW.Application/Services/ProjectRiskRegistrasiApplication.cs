using KW.Common.Validation;
using System;
using System.Collections.Generic;
using KW.Application.Params;
using KW.Core;
using KW.Domain;

namespace KW.Application
{
    public class ProjectRiskRegistrasiService : IProjectRiskRegistrasiService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProjectRiskRegistrasiRepository _projectRiskRegistrasiRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IRiskRegistrasiRepository _riskRegistrasiRepository;

        public ProjectRiskRegistrasiService(IUnitOfWork unitOfWork, IProjectRiskRegistrasiRepository projectRiskRegistrasiRepository, IProjectRepository projectRepository, IRiskRegistrasiRepository riskRegistrasiRepository)
        {
            _unitOfWork = unitOfWork;
            _projectRiskRegistrasiRepository = projectRiskRegistrasiRepository;
            _projectRepository = projectRepository;
            _riskRegistrasiRepository = riskRegistrasiRepository;
        }

        #region Query
        public IEnumerable<ProjectRiskRegistrasi> GetAll()
        {
            return _projectRiskRegistrasiRepository.GetAll();
        }

        public IEnumerable<ProjectRiskRegistrasi> GetByProjectId(int projectId)
        {
            return _projectRiskRegistrasiRepository.GetByProjectId(projectId);
        }

        public ProjectRiskRegistrasi Get(int id)
        {
            return _projectRiskRegistrasiRepository.Get(id);
        }

        public void isExist(int projectId, int riskRegistrasiId)
        {
            if(_projectRiskRegistrasiRepository.IsExist(projectId, riskRegistrasiId))
            {
                throw new ApplicationException(string.Format("Projek dan Katagori {0} Sudah ada."));
            }
        }
        #endregion Query

        #region Manipulation
        public void Add(IList<ProjectRiskRegistrasiParam> collections)
        {
            Project project = _projectRepository.Get(collections[0].ProjectId);
            Validate.NotNull(project, "Project is not found");

            RiskRegistrasi risk = new RiskRegistrasi();

            using (_unitOfWork)
            {
                IList<ProjectRiskRegistrasi> projectRisks = new List<ProjectRiskRegistrasi>();
                foreach (var item in collections)
                {
                    risk = _riskRegistrasiRepository.Get(item.RiskRegistrasiId);
                    Validate.NotNull(risk, "Risk Category " + item.RiskRegistrasiId + "is not found");

                    projectRisks.Add(new ProjectRiskRegistrasi(project, risk, item.CreateBy, item.CreateDate));
                }

                _projectRiskRegistrasiRepository.Insert(projectRisks);
                _unitOfWork.Commit();
            }
        }

        public void Delete(int id, int deleteBy, DateTime deleteDate)
        {
            throw new NotImplementedException();
        }
        #endregion Manipulation
    }
}
