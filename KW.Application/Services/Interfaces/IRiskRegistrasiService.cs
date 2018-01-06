using KW.Application.Params;
using KW.Domain;
using System;
using System.Collections.Generic;

namespace KW.Application
{
    public interface IRiskRegistrasiService
    {
        IEnumerable<RiskRegistrasi> GetAll();
        RiskRegistrasi Get(int id);
        int Add(RiskRegistrasiParam param);
        int Update(int id, RiskRegistrasiParam param);
        int Delete(int id, int deleteBy, DateTime deleteDate);
    }
}
