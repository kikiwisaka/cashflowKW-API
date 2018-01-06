using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public interface IMaksimumProjectValueRepository
    {
        MaksimumProjectValue Get(int id);
        MaksimumProjectValue GetByScenarioId(int scenarioId);
        IEnumerable<MaksimumProjectValue> GetAll();
        void Insert(MaksimumProjectValue model);
        void Update(MaksimumProjectValue model);
        void Delete(int id, int deleteBy, DateTime deleteDate);
        bool IsExist(int id, int scenarioId);
        bool IsExist(int scenarioId);
    }
}
