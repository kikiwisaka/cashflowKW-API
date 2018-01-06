using System.Collections.Generic;
using KW.Core;
using KW.Domain;

namespace KW.Application
{
    public class CorrelatedProjectService : ICorrelatedProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICorrelatedProjectRepository _correlatedProjectRepository;
        private readonly IScenarioRepository _scenarioRepository;

        public CorrelatedProjectService(IUnitOfWork unitOfWork, ICorrelatedProjectRepository correlatedProjectRepository, IScenarioRepository scenarioRepository)
        {
            _unitOfWork = unitOfWork;
            _correlatedProjectRepository = correlatedProjectRepository;
            _scenarioRepository = scenarioRepository;
        }

        public IEnumerable<CorrelatedProject> GetAll()
        {
            return _correlatedProjectRepository.GetAll();
        }

        public IEnumerable<CorrelatedProject> GetByScenarioDefaultId()
        {
            var scenarioDefault = _scenarioRepository.GetDefault();
            return _correlatedProjectRepository.GetByScenarioId(scenarioDefault.Id);
        }

        public CorrelatedProject Get(int id)
        {
            return _correlatedProjectRepository.Get(id);
        }
    }
}
