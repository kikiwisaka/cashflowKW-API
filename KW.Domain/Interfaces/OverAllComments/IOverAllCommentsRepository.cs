using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public interface IOverAllCommentsRepository
    {
        OverAllComments Get(int id);
        IEnumerable<OverAllComments> GetAll();
        IEnumerable<OverAllComments> GetByColorId(int colorId);
        //OverAllComments GetByColorId(int colorId);
        void Insert(OverAllComments model);
        void Update(OverAllComments model);
        void Delete(int id, int deleteBy, DateTime deleteDate);
        bool IsExist(int id, string overAllComment);
        bool IsExist(string overAllComment);
    }
}
