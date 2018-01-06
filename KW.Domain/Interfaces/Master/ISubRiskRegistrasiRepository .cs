using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public interface ISubRiskRegistrasiRepository
    {
        SubRiskRegistrasi Get(int id);
        IEnumerable<SubRiskRegistrasi> GetAll();
        IEnumerable<SubRiskRegistrasi> GetByRiskId(int riskId);
        void Insert(SubRiskRegistrasi model);
        void Update(SubRiskRegistrasi model);
        void Delete(int id);
        bool IsExist(int id, string kodeRisk);
        bool IsExist(string kodeRisk);
    }
}
