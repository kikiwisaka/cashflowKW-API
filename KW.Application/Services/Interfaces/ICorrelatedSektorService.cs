using KW.Application.Params;
using KW.Domain;
using System;
using System.Collections.Generic;

namespace KW.Application
{
    public interface ICorrelatedSektorService
    {
        IEnumerable<CorrelatedSektor> GetAll();
        IEnumerable<CorrelatedSektor> GetByScenarioDefaultId();
        CorrelatedSektor Get(int id);
        int Add(CorrelatedSektorParam param);
        int Update(int id, CorrelatedSektorParam param);
        int Delete(int id, int deleteBy, DateTime deleteDate);
    }
}
