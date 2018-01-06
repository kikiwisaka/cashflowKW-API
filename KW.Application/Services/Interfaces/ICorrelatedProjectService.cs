using KW.Application.Params;
using KW.Domain;
using System.Collections.Generic;

namespace KW.Application
{
    public interface ICorrelatedProjectService
    {
        IEnumerable<CorrelatedProject> GetAll();
        IEnumerable<CorrelatedProject> GetByScenarioDefaultId();
        CorrelatedProject Get(int id);
    }
}
