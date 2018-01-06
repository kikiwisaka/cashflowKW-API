using KW.Application.Params;
using KW.Domain;
using System;
using System.Collections.Generic;

namespace KW.Application
{
    public interface IStageTahunRiskMatrixDetailService
    {
        IEnumerable<StageTahunRiskMatrixDetail> GetByStageTahunRiskMatrixId(int stageTahunRiskMatrixId);
        RiskMatrixCollectionParameter GetByRiskMatrixProjectId(int riskMatrixProjectId);
        //IEnumerable<StageTahunRiskMatrixDetail> GetByRiskMatrixProjectId(int riskMatrixProjectId);
        int Add(RiskMatrixCollectionParameter param);
        //int Add(StageTahunRiskMatrixDetailParam param);
        int Update(int id, RiskMatrixCollectionParameter param);
        int Delete(int id, int deleteBy, DateTime deleteDate);
    }
}
