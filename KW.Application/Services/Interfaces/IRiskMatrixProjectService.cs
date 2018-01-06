using KW.Application.Params;
using KW.Domain;
using System;
using System.Collections.Generic;

namespace KW.Application
{
    public interface IRiskMatrixProjectService
    {
        IEnumerable<RiskMatrixProject> GetAllData();
        IEnumerable<RiskMatrixProject> GetAll();
        RiskMatrixProject Get(int id);
        int Add(RiskMatrixProjectParam param);
        int Update(int id, RiskMatrixProjectParam param);
        int Delete(int id, int deleteBy, DateTime deleteDate);
    }
}
