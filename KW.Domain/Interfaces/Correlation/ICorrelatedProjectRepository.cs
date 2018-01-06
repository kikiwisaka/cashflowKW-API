using System;
using System.Collections.Generic;

namespace KW.Domain
{
    public interface ICorrelatedProjectRepository
    {
        CorrelatedProject Get(int id);
        IEnumerable<CorrelatedProject> GetAll();
        IEnumerable<CorrelatedProject> GetByScenarioId(int scenarioId);
        void Insert(CorrelatedProject model);
        void Update(CorrelatedProject model);
        void Delete(int id);
    }
}
