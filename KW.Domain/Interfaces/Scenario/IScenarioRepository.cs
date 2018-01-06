using System;
using System.Collections.Generic;

namespace KW.Domain
{
    public interface IScenarioRepository
    {
        Scenario Get(int? id);
        IEnumerable<Scenario> GetAll();
        void Insert(Scenario model);
        void Update(Scenario model);
        void Delete(int id, int deleteBy, DateTime deleteDate);
        bool IsExist(int id, string namaScenario);
        bool IsExist(string namaScenario);
        Scenario GetDefault();
        //void SetDefault(Scenario model);

    }
}
