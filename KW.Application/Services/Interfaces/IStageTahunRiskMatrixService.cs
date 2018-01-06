using KW.Application.Params;
using KW.Domain;
using System;
using System.Collections.Generic;

namespace KW.Application
{
    public interface IStageTahunRiskMatrixService
    {
        IEnumerable<StageTahunRiskMatrix> GetAll();
        StageTahunRiskMatrix Get(int id);
        StageTahunRiskMatrixParam GetByRiskMatrixProjectId(int riskMatrixProjectId);
        //IEnumerable<StageTahunRiskMatrix> GetByRiskMatrixProjectId(int riskMatrixProjectId);
        int Add(StageTahunRiskMatrixParam param);
        int Update(int id, StageTahunRiskMatrixParam param);
        int Delete(int id, int deleteBy, DateTime deleteDate);
    }
}
