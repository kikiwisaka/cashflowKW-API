using KW.Common.Validation;
using System;
using System.Collections.Generic;
using KW.Application.Params;
using KW.Core;
using KW.Domain;

namespace KW.Application
{
    public class ScenarioDetailService : IScenarioDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IScenarioRepository _scenarioRepository;
        private readonly IScenarioDetailRepository _scenarioDetailRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IRiskRegistrasiRepository _riskRegistrasiRepository;

        public ScenarioDetailService(IUnitOfWork unitOfWork, IScenarioRepository scenarioRepository, IScenarioDetailRepository scenarioDetailRepository, IProjectRepository projectRepository, IRiskRegistrasiRepository riskRegistrasiRepository)
        {
            _unitOfWork = unitOfWork;
            _scenarioRepository = scenarioRepository;
            _scenarioDetailRepository = scenarioDetailRepository;
            _projectRepository = projectRepository;
            _riskRegistrasiRepository = riskRegistrasiRepository;
        }

        #region Query
        public IEnumerable<ScenarioDetail> GetAll()
        {
            return _scenarioDetailRepository.GetAll();
        }

        public IEnumerable<ScenarioDetail> GetByScenarioId(int scenarioId)
        {
            return _scenarioDetailRepository.GetByScenarioId(scenarioId);
        }

        public ScenarioDetail Get(int id)
        {
            return _scenarioDetailRepository.Get(id);
        }

        #endregion Query

        #region Manipulation
        public int Add(ScenarioDetailParam param)
        {
            int id;
            Validate.NotNull(param.ScenarioId, "Nama Scenario is required.");
            Validate.NotNull(param.ProjectId, "Project is required.");

            //isExistOnAdding(param.NamaScenario);
            using (_unitOfWork)
            {
                ScenarioDetail model = new ScenarioDetail();
                _scenarioDetailRepository.Insert(model);
                _unitOfWork.Commit();
                id = model.Id;
            }

            return id;
        }

        public int Update(int id, ScenarioDetailParam param)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id, int deleteBy, DateTime deleteDate)
        {
            throw new NotImplementedException();
        }
        #endregion Manipulation
    }
}
