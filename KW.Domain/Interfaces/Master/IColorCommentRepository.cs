using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public interface IColorCommentRepository
    {
        ColorComment Get(int? id);
        IEnumerable<ColorComment> GetAll();
        void Insert(ColorComment model);
        void Update(ColorComment model);
        void Delete(int id, int deleteBy, DateTime deleteDate);
        bool IsExist(int id, string warna);
        bool IsExist(string warna);
    }
}
