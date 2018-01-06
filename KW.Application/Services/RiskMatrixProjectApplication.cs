using KW.Common.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KW.Application.Params;
using KW.Core;
using KW.Domain;

namespace KW.Application
{
    public class RiskMatrixProjectService : IRiskMatrixProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRiskMatrixProjectRepository _riskMatrixProjectRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IRiskMatrixRepository _riskMatrixRepository;
        private readonly IScenarioRepository _scenarioRepository;

        public RiskMatrixProjectService(IUnitOfWork uow, IRiskMatrixProjectRepository riskMatrixProjectRepository, IProjectRepository projectRepository, IRiskMatrixRepository riskMatrixRepository, IScenarioRepository scenarioRepository)
        {
            _unitOfWork = uow;
            _riskMatrixProjectRepository = riskMatrixProjectRepository;
            _projectRepository = projectRepository;
            _riskMatrixRepository = riskMatrixRepository;
            _scenarioRepository = scenarioRepository;

        }

        #region Query
        public IEnumerable<RiskMatrixProject> GetAll()
        {
            return _riskMatrixProjectRepository.GetAll().ToList().Where(x => x.Scenario.IsDefault == true);
        }
        
        public IEnumerable<RiskMatrixProject> GetAllData()
        {
            return _riskMatrixProjectRepository.GetAllData();
        }

        public RiskMatrixProject Get(int id)
        {
            return _riskMatrixProjectRepository.Get(id);
        }

        public void IsExistOnEditing(int id, int projectId)
        {
            if (_riskMatrixProjectRepository.IsExist(id, projectId))
            {
                throw new ApplicationException(string.Format("Project already exist.", projectId));
            }
        }

        public void isExistOnAdding(int projectId)
        {
            if (_riskMatrixProjectRepository.IsExist(projectId))
            {
                throw new ApplicationException(string.Format("Project {0} already exist.", projectId));
            }
        }
        #endregion Query

        #region Manipulation 
        public int Add(RiskMatrixProjectParam param)
        {
            int id;
            var project = _projectRepository.Get(param.ProjectId);
            var riskMatrix = _riskMatrixRepository.Get(param.RiskMatrixId);
            var scenario = _scenarioRepository.Get(param.ScenarioId);
            Validate.NotNull(param.ProjectId, "Project wajib diisi.");

            isExistOnAdding(param.ProjectId);
            using (_unitOfWork)
            {
                RiskMatrixProject model = new RiskMatrixProject(project, riskMatrix, scenario, param.CreateBy, param.CreateDate);
                _riskMatrixProjectRepository.Insert(model);
                _unitOfWork.Commit();
                id = model.Id;
            }

            return id;
        }

        public int Update(int id, RiskMatrixProjectParam param)
        {
            var model = this.Get(id);
            var project = _projectRepository.Get(param.ProjectId);
            var riskMatrix = _riskMatrixRepository.Get(param.RiskMatrixId);
            var scenario = _scenarioRepository.Get(param.ScenarioId);
            Validate.NotNull(model, "Project tidak ditemukan.");

            IsExistOnEditing(id, param.ProjectId);
            using (_unitOfWork)
            {
                model.Update(project, riskMatrix, scenario, param.UpdateBy, param.UpdateDate);
                _riskMatrixProjectRepository.Update(model);
                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }
        
        public int Delete(int id, int deleteBy, DateTime deleteDate)
        {
            var model = this.Get(id);
            Validate.NotNull(model, "Nama Category Risk tidak ditemukan.");

            using (_unitOfWork)
            {
                model.Delete(deleteBy, deleteDate);
                _riskMatrixProjectRepository.Update(model);
                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }
        #endregion Manipulation
    }
}
