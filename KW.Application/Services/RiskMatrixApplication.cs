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
    public class RiskMatrixService : IRiskMatrixService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRiskMatrixRepository _riskMatrixRepository;
        private readonly IScenarioRepository _scenarioRepository;

        public RiskMatrixService(IUnitOfWork uow, IRiskMatrixRepository riskMatrixRepository, IScenarioRepository scenarioRepository)
        {
            _unitOfWork = uow;
            _riskMatrixRepository = riskMatrixRepository;
            _scenarioRepository = scenarioRepository;
        }

        #region Query
        public RiskMatrix Get(int id)
        {
            return _riskMatrixRepository.Get(id);
        }

        public IEnumerable<RiskMatrix> GetAll()
        {
            return _riskMatrixRepository.GetAll().ToList().Where(x => x.Scenario.IsDefault == true);
        }

        public void IsExistOnEditing(int id, int scenarioId)
        {
            if (_riskMatrixRepository.IsExist(id, scenarioId))
            {
                throw new ApplicationException(string.Format("Scenario {0} sudah ada.", scenarioId));
            }
        }

        public void isExistOnAdding(int scenarioId)
        {
            if (_riskMatrixRepository.IsExist(scenarioId))
            {
                throw new ApplicationException(string.Format("Scenario {0} sudah ada.", scenarioId));
            }
        }
        #endregion Query

        #region Manipulation
        public int Add(RiskMatrixParam param)
        {
            int id;
            var scenario = _scenarioRepository.Get(param.ScenarioId);
            Validate.NotNull(param.ScenarioId, "Scenario wajib diisi.");

            isExistOnAdding(param.ScenarioId);
            using (_unitOfWork)
            {
                RiskMatrix model = new RiskMatrix(scenario, param.CreateBy, param.CreateDate);
                _riskMatrixRepository.Insert(model);
                _unitOfWork.Commit();
                id = model.Id;
            }

            return id;
        }

        public int Update(int id, RiskMatrixParam param)
        {
            var model = this.Get(id);
            Validate.NotNull(model, "Risk Matrix tidak ditemukan.");

            var scenario = _scenarioRepository.Get(param.ScenarioId);
            Validate.NotNull(param.ScenarioId, "Scenario tidak ditemukan.");

            IsExistOnEditing(id, param.ScenarioId);
            using (_unitOfWork)
            {
                model.Update(scenario, param.UpdateBy, param.UpdateDate);
                _riskMatrixRepository.Update(model);
                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }

        public int Delete(int id, int deleteBy, DateTime deleteDate)
        {
            var model = this.Get(id);
            Validate.NotNull(model, "Scenario tidak ditemukan.");

            using (_unitOfWork)
            {
                model.Delete(deleteBy, deleteDate);
                _riskMatrixRepository.Update(model);
                _unitOfWork.Commit();
                id = model.Id;
            }
            return id;
        }
        #endregion Manipulation
    }
}
