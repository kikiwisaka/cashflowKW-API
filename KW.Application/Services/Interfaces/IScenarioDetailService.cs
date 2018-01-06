using KW.Application.Params;
using KW.Domain;
using System;
using System.Collections.Generic;

namespace KW.Application
{
    public interface IScenarioDetailService
    {
        IEnumerable<ScenarioDetail> GetAll();
        IEnumerable<ScenarioDetail> GetByScenarioId(int scenarioId);
        ScenarioDetail Get(int id);
        int Add(ScenarioDetailParam param);
        int Update(int id, ScenarioDetailParam param);
        int Delete(int id, int deleteBy, DateTime deleteDate);
    }
}
