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
    public class CorrelatedSektorService : ICorrelatedSektorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICorrelatedSektorRepository _correlatedSektorRepository;
        private readonly IScenarioRepository _scenarioRepository;

        public CorrelatedSektorService(IUnitOfWork unitOfWork, ICorrelatedSektorRepository correlatedSektorRepository, IScenarioRepository scenarioRepository)
        {
            _unitOfWork = unitOfWork;
            _correlatedSektorRepository = correlatedSektorRepository;
            _scenarioRepository = scenarioRepository;
        }

        #region Query
        public IEnumerable<CorrelatedSektor> GetAll()
        {
            return _correlatedSektorRepository.GetAll();
        }

        public IEnumerable<CorrelatedSektor> GetByScenarioDefaultId()
        {
            var scenarioDefault = _scenarioRepository.GetDefault();
            Validate.NotNull(scenarioDefault, "Scenario default tidak ditemukan.");

            return _correlatedSektorRepository.GetByScenarioDefaultId(scenarioDefault.Id);
        }

        public CorrelatedSektor Get(int id)
        {
            return _correlatedSektorRepository.Get(id);
        }
        #endregion Query

        public int Add(CorrelatedSektorParam param)
        {
            throw new NotImplementedException();
        }

        public int Update(int id, CorrelatedSektorParam param)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id, int deleteBy, DateTime deleteDate)
        {
            throw new NotImplementedException();
        }
    }
}
