using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Domain
{
    public interface ICommentsRepository
    {
        Comments Get(int id);
        IEnumerable<Comments> GetAll();
        IEnumerable<Comments> GetByColorId(int colorId);
        void Insert(Comments model);
        void Update(Comments model);
        void Delete(int id, int deleteBy, DateTime deleteDate);
        bool IsExist(int id, string comment);
        bool IsExist(string comment);
    }
}
