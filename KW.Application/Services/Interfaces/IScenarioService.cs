using KW.Application.Params;
using KW.Domain;
using System;
using System.Collections.Generic;

namespace KW.Application
{
    public interface IScenarioService
    {
        IEnumerable<Scenario> GetAll();
        Scenario Get(int id);
        int Add(ScenarioParam param);
        int Update(int id, ScenarioParam param);
        int Delete(int id, int deleteBy, DateTime deleteDate);
        int SetDefault(int id, int? updateBy, DateTime? updateDate);
        Scenario GetDefault();
        void RemoveDefault(int updateBy, DateTime updateDate);
    }
}
