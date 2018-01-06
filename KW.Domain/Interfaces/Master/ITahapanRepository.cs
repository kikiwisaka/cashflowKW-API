using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public interface ITahapanRepository
    {
        Tahapan Get(int id);
        IEnumerable<Tahapan> GetAll();
        void Insert(Tahapan model);
        void Update(Tahapan model);
        void Delete(int id, int deleteBy, DateTime deleteDate);
        bool IsExist(int id, string namaTahapan);
        bool IsExist(string namaTahapan);
    }
}
