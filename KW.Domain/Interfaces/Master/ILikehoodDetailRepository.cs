using System;
using System.Collections.Generic;

namespace KW.Domain
{
    public interface ILikehoodDetailRepository
    {
        LikehoodDetail Get(int id);
        IEnumerable<LikehoodDetail> GetAll();
        IEnumerable<LikehoodDetail> GetByLikehoodId(int likehoodId);
        void Insert(LikehoodDetail model);
        void Insert(IList<LikehoodDetail> collections);
        void Update(LikehoodDetail model);
        void Delete(int id, int deleteBy, DateTime deleteDate);
        bool IsExist(int id, string definisiLikehood);
        bool IsExist(string definisiLikehood);
    }
}
