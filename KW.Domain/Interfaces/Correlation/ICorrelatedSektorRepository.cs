using System;
using System.Collections.Generic;

namespace KW.Domain
{
    public interface ICorrelatedSektorRepository
    {
        CorrelatedSektor Get(int id);
        IEnumerable<CorrelatedSektor> GetAll();
        IEnumerable<CorrelatedSektor> GetByScenarioDefaultId(int scenarioId);
        IEnumerable<CorrelatedSektor> GetByScenarioIdIsZero();
        void Insert(CorrelatedSektor model);
        void Insert(IList<CorrelatedSektor> collection);
        void Update(CorrelatedSektor model);
        void Delete(int id, int deleteBy, DateTime deleteDate);
    }
}
