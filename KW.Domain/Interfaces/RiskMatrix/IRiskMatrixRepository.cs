using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public interface IRiskMatrixRepository
    {
        RiskMatrix Get(int id);
        RiskMatrix GetByScenarioId(int scenarioId);
        IEnumerable<RiskMatrix> GetAll();
        void Insert(RiskMatrix model);
        void Update(RiskMatrix model);
        void Delete(int id, int deleteBy, DateTime deleteDate);
        bool IsExist(int id, int scenarioId);
        bool IsExist(int scenarioId);
    }
}
