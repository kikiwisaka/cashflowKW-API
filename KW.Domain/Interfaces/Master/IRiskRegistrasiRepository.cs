using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public interface IRiskRegistrasiRepository
    {
        RiskRegistrasi Get(int id);
        IEnumerable<RiskRegistrasi> GetAll();
        void Insert(RiskRegistrasi model);
        void Update(RiskRegistrasi model);
        void Delete(int id, int deleteBy, DateTime deleteDate);
        bool IsExist(int id, string namaCategoryRisk);
        bool IsExist(string kodeMRisk);
    }
}
