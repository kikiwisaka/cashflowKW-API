using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public interface ICorrelationMatrixRepository
    {
        CorrelationMatrix Get(int id);
        IEnumerable<CorrelationMatrix> GetAll();
        void Insert(CorrelationMatrix model);
        void Update(CorrelationMatrix model);
        bool IsExist(int id, string namaCorrelationMatrix);
        bool IsExist(string namaCorrelationMatrix);
    }
}
