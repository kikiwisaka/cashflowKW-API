using KW.Application.Params;
using KW.Domain;
using System;
using System.Collections.Generic;

namespace KW.Application
{
    public interface IOverAllCommentsService
    {
        IEnumerable<OverAllComments> GetAll();
        IEnumerable<OverAllComments> GetByColorId(int colorId);
        //OverAllComments GetByColorId(int colorId);
        OverAllComments Get(int id);
        int Add(OverAllCommentsParam param);
        int Update(int id, OverAllCommentsParam param);
        int Delete(int id, int deleteBy, DateTime deleteDate);
    }
}
