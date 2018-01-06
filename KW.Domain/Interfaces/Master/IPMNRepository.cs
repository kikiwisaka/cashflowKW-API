using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public interface IPMNRepository
    {
        PMN Get(int id);
        IEnumerable<PMN> GetAll();
        void Insert(PMN model);
        void Update(PMN model);
        void Delete(int id, int deleteBy, DateTime deleteDate);
        bool IsExist(int id, int pmnToModalDasarCap);
        bool IsExist(int pmnToModalDasarCap);
    }
}
