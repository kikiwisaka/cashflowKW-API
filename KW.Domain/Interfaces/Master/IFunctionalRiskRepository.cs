using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public interface IFunctionalRiskRepository
    {
        FunctionalRisk Get(int id);
        IEnumerable<FunctionalRisk> GetAll();
        void Insert(FunctionalRisk model);
        void Update(FunctionalRisk model);
        void Delete(int id, int deleteBy, DateTime deleteDate);
        bool IsExist(int id, string definisi);
        bool IsExist(string definisi);
    }
}
