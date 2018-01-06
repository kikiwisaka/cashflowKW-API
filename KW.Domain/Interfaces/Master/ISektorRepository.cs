using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public interface ISektorRepository
    {
        Sektor Get(int id);
        IEnumerable<Sektor> GetAll();
        void Insert(Sektor model);
        void Update(Sektor model);
        void Delete(int id, int deleteBy, DateTime deleteDate);
        bool IsExist(int id, string namaSektor);
        bool IsExist(string namaSektor);
    }
}
