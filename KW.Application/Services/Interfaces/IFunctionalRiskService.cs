using KW.Application.Params;
using KW.Domain;
using System;
using System.Collections.Generic;

namespace KW.Application
{
    public interface IFunctionalRiskService
    {
        IEnumerable<FunctionalRisk> GetAll();
        FunctionalRisk Get(int id);
        int Add(FunctionalRiskParam param);
        int Update(int id, FunctionalRiskParam param);
        int Delete(int id, int deleteBy, DateTime deleteDate);
    }
}
