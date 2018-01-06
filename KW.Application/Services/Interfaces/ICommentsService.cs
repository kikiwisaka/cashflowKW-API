using KW.Application.Params;
using KW.Domain;
using System;
using System.Collections.Generic;

namespace KW.Application
{
    public interface ICommentsService
    {
        IEnumerable<Comments> GetAll();
        IEnumerable<Comments> GetByColorId(int colorId);
        Comments Get(int id);
        int Add(CommentsParam param);
        int Update(int id, CommentsParam param);
        int Delete(int id, int deleteBy, DateTime deleteDate);
    }
}
