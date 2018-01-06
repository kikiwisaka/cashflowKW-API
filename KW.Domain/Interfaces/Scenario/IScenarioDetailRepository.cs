using System;
using System.Collections.Generic;

namespace KW.Domain
{
    public interface IScenarioDetailRepository
    {
        ScenarioDetail Get(int id);
        IEnumerable<ScenarioDetail> GetAll();
        IEnumerable<ScenarioDetail> GetByScenarioId(int scenarioId);
        void Insert(ScenarioDetail model);
        void Insert(IList<ScenarioDetail> collections);
        void Update(ScenarioDetail model);
        void Delete(int id);
    }
}
