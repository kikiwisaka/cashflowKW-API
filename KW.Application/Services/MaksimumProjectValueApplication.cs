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
    public class MaksimumProjectValueService : IMaksimumProjectValueService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMaksimumProjectValueRepository _maksimumProjectValueRepository;
        private readonly IScenarioRepository _scenarioRepository;
        private readonly IProjectRepository _projectRepository;

        public MaksimumProjectValueService(IUnitOfWork uow, IMaksimumProjectValueRepository maksimumProjectValueRepository, IScenarioRepository scenarioRepository, IProjectRepository projectRepository)
        {
            _unitOfWork = uow;
            _maksimumProjectValueRepository = maksimumProjectValueRepository;
            _scenarioRepository = scenarioRepository;
            _projectRepository = projectRepository;
        }

        #region Query
        public IEnumerable<MaksimumProjectValue> GetAll()
        {
            return _maksimumProjectValueRepository.GetAll();
        }

        public MaksimumProjectValue Get(int id)
        {
            return _maksimumProjectValueRepository.Get(id);
        }

        public void IsExistOnEditing(int id, int scenarioId)
        {
            if (_maksimumProjectValueRepository.IsExist(id, scenarioId))
            {
                throw new ApplicationException(string.Format("Scenario {0} already exist.", scenarioId));
            }
        }

        public void isExistOnAdding(int scenarioId)
        {
            if (_maksimumProjectValueRepository.IsExist(scenarioId))
            {
                throw new ApplicationException(string.Format("ScenarioId {0} already exist.", scenarioId));
            }
        }
        #endregion Query

        #region Manipulation 
        public int Add(MaksimumProjectValueParam param)
        {
            int id;
            var project = _projectRepository.Get(param.ProjectId);
            var scenario = _scenarioRepository.Get(param.ScenarioId);
            Validate.NotNull(param.ScenarioId, "Scenario wajib diisi.");

            isExistOnAdding(param.ScenarioId);
            using (_unitOfWork)
            {
                MaksimumProjectValue model = new MaksimumProjectValue(scenario, project, param.Tahun, param.NilaiMaximum, param.CreateBy, param.CreateDate);
                _maksimumProjectValueRepository.Insert(model);
                _unitOfWork.Commit();
                id = model.Id;
            }

            return id;
        }

        public int Update(int id, MaksimumProjectValueParam param)
        {
            var model = this.Get(id);
            var project = _projectRepository.Get(param.ProjectId);
            var scenario = _scenarioRepository.Get(param.ScenarioId);
            Validate.NotNull(model, "Maksimum Project Value tidak ditemukan.");

            IsExistOnEditing(id, param.ScenarioId);
            using (_unitOfWork)
            {
                model.Update(scenario, project, param.Tahun, param.NilaiMaximum, param.UpdateBy, param.UpdateDate);
                _maksimumProjectValueRepository.Update(model);
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
                _maksimumProjectValueRepository.Update(model);
                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }
        #endregion Manipulation
    }
}
