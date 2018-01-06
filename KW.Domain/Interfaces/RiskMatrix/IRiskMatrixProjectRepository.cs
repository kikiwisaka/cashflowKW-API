using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public interface IRiskMatrixProjectRepository
    {
        RiskMatrixProject Get(int id);
        IEnumerable<RiskMatrixProject> GetAllData();
        IEnumerable<RiskMatrixProject> GetAll();
        IEnumerable<RiskMatrixProject> GetByScenarioId(int scenarioId);
        IEnumerable<RiskMatrixProject> GetAllByScenarioId(int scenarioId);
        RiskMatrixProject GetByScenarioIdProjectId(int scenarioId, int projectId);
        RiskMatrixProject GetByProjectId(int projectId);
        void Insert(RiskMatrixProject model);
        void Insert(IList<RiskMatrixProject> collelction);
        void Update(RiskMatrixProject model);
        void Delete(int id);
        bool IsExist(int id, int ProjectId);
        bool IsExist(int ProjectId);
    }
}
