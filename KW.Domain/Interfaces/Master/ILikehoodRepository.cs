using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public interface ILikehoodRepository
    {
        Likehood Get(int id);
        IEnumerable<Likehood> GetAll();
        void Insert(Likehood model);
        void Update(Likehood model);
        void Delete(int id, int deleteBy, DateTime deleteDate);
        bool IsExist(int id, string namaLikehood);
        bool IsExist(string namaLikehood);
        Likehood GetDefault();

    }
}
