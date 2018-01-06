using KW.Application.Params;
using KW.Domain;
using System;
using System.Collections.Generic;
namespace KW.Application
{
    public interface ILikehoodDetailService
    {
        IEnumerable<LikehoodDetail> GetAll();
        IEnumerable<LikehoodDetail> GetByLikehoodId(int likehoodId);
        LikehoodDetail Get(int id);
        int Add(LikehoodDetailParam param);
        int Update(int id, LikehoodDetailParam param);
    }
}
