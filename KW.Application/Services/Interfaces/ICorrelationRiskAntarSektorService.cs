using KW.Application.Params;
using KW.Domain;
using System;
using System.Collections.Generic;

namespace KW.Application
{
    public interface ICorrelationRiskAntarSektorService
    {
        IEnumerable<CorrelationRiskAntarSektor> GetAll();
        IList<Sektor> GetSektorList();
        CorrelationRiskAntarSektor Get(int id);
        CorrelationRiskAntarSektor GetByScenarioId(int scenarioId);
        IEnumerable<CorrelationRiskAntarSektor> GetByScenarioDefault(int scenarioId);
        IEnumerable<Project> GetProjectByScenarioDefault(int scenarioId);
        int Add(CorrelationRiskAntarSektorParam param);
        int Update(int id, CorrelationRiskAntarSektorParam param);
        int Delete(int id, int deleteBy, DateTime deleteDate);
    }
}
