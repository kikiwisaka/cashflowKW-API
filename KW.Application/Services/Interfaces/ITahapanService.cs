using KW.Application.Params;
using KW.Domain;
using System;
using System.Collections.Generic;

namespace KW.Application
{
    public interface ITahapanService
    {
        IEnumerable<Tahapan> GetAll();
        Tahapan Get(int id);
        int Add(TahapanParam param);
        int Update(int id, TahapanParam param);
        int Delete(int id, int deleteBy, DateTime deleteDate);
    }
}
