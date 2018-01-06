using KW.Application.Params;
using KW.Domain;
using System;
using System.Collections.Generic;

namespace KW.Application
{
    public interface IMaksimumProjectValueService
    {
        IEnumerable<MaksimumProjectValue> GetAll();
        MaksimumProjectValue Get(int id);
        int Add(MaksimumProjectValueParam param);
        int Update(int id, MaksimumProjectValueParam param);
        int Delete(int id, int deleteBy, DateTime deleteDate);
    }
}
