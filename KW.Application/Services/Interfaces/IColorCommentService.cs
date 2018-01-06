using KW.Application.Params;
using KW.Domain;
using System;
using System.Collections.Generic;

namespace KW.Application
{
    public interface IColorCommentService
    {
        IEnumerable<ColorComment> GetAll();
        ColorComment Get(int id);
        int Add(ColorCommentParam param);
        int Update(int id, ColorCommentParam param);
        int Delete(int id, int deleteBy, DateTime deleteDate);
    }
}
