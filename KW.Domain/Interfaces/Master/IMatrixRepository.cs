using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public interface IMatrixRepository
    {
        Matrix Get(int? id);
        IEnumerable<Matrix> GetAll();
        void Insert(Matrix model);
        void Update(Matrix model);
        void Delete(int id, int deleteBy, DateTime deleteDate);
        bool IsExist(int id, string namaMatrix);
        bool IsExist(string namaMatrix);
    }
}
