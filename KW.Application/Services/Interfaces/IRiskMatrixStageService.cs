using KW.Application.Params;
using KW.Domain;
using System;
using System.Collections.Generic;

namespace KW.Application
{
    public interface IRiskMatrixStageService
    {
        IEnumerable<RiskMatrixStage> GetAll();
        RiskMatrixStage Get(int id);
        int Add(RiskMatrixStageParam param);
        int Update(int id, RiskMatrixStageParam param);
        int Delete(int id, int deleteBy, DateTime deleteDate);
    }
}
