using KW.Application.Params;
using KW.Domain;
using System;
using System.Collections.Generic;

namespace KW.Application
{
    public interface ILikehoodService
    {
        IEnumerable<Likehood> GetAll();
        Likehood Get(int id);
        int Add(LikehoodParam param);
        int Update(int id, LikehoodParam param);
        int Delete(int id, int deleteBy, DateTime deleteDate);
        int Default(int id, LikehoodParam param);
        Likehood GetDefault();
    }
}
