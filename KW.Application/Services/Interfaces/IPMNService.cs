using KW.Application.Params;
using KW.Domain;
using System;
using System.Collections.Generic;

namespace KW.Application
{
    public interface IPMNService
    {
        IEnumerable<PMN> GetAll();
        PMN Get(int id);
        int Add(PMNParam param);
        int Update(int id, PMNParam param);
        int Delete(int id, int deleteBy, DateTime deleteDate);
    }
}
