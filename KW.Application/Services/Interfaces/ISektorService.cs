using KW.Application.Params;
using KW.Domain;
using System;
using System.Collections.Generic;

namespace KW.Application
{
    public interface ISektorService
    {
        IEnumerable<Sektor> GetAll();
        Sektor Get(int id);
        int Add(SektorParam param);
        int Update(int id, SektorParam param);
        int Delete(int id, int deleteBy, DateTime deleteDate);
    }
}
