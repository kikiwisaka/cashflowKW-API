using System.Collections.Generic;

namespace KW.Domain
{
    public interface ICorrelationRiskAntarProjectRepository
    {
        CorrelationRiskAntarProject Get(int id);
        IEnumerable<CorrelationRiskAntarProject> GetAll();
        IEnumerable<CorrelationRiskAntarProject> GetByScenarioDefaultId(int scenarioId);
        IEnumerable<CorrelationRiskAntarProject> GetByCorrelationRiskAntarSektorId(int correlationRiskAntarSektorId);
        void Insert(CorrelationRiskAntarProject model);
        void Insert(IList<CorrelationRiskAntarProject> collections);
        void Update(CorrelationRiskAntarProject model);
        void Delete(int id);
        CorrelationRiskAntarProject isExistOnAdding(int correlationRiskAntarSektorId, int projectIdRow, int projectIdCol);
    }
}
