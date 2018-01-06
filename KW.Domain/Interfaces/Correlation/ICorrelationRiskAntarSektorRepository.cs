using System;
using System.Collections.Generic;

namespace KW.Domain
{
    public interface ICorrelationRiskAntarSektorRepository
    {
        CorrelationRiskAntarSektor Get(int id);
        CorrelationRiskAntarSektor GetByScenarioId(int scenarioId);
        CorrelationRiskAntarSektor GetByProjectIdScenarioId(int projectId, int scenarioId);
        IEnumerable<CorrelationRiskAntarSektor> GetAll();
        IEnumerable<CorrelationRiskAntarSektor> GetByScenarioDefault(int scenarioId);
        void Insert(CorrelationRiskAntarSektor model);
        void Update(CorrelationRiskAntarSektor model);
        void Delete(int id, int deleteBy, DateTime deleteDate);
        bool IsExist(int scenarioId);

    }
}
