using KW.Application.Params;
using KW.Domain;
using System;
using System.Collections.Generic;

namespace KW.Application
{
    public interface ISubRiskRegistrasiService
    {
        IEnumerable<SubRiskRegistrasi> GetAll();
        IEnumerable<SubRiskRegistrasi> GetByRiskId(int riskId);

        SubRiskRegistrasi Get(int id);
        int Add(SubRiskRegistrasiParam param);
        int Update(int id, SubRiskRegistrasiParam param);
        int Delete(int id, int deleteBy, DateTime deleteDate);
    }
}
