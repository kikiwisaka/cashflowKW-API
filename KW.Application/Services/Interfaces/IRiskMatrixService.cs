using KW.Application.Params;
using KW.Domain;
using System;
using System.Collections.Generic;

namespace KW.Application
{
    public interface IRiskMatrixService
    {
        IEnumerable<RiskMatrix> GetAll();
        RiskMatrix Get(int id);
        int Add(RiskMatrixParam param);
        int Update(int id, RiskMatrixParam param);
        int Delete(int id, int deleteBy, DateTime deleteDate);
    }
}
