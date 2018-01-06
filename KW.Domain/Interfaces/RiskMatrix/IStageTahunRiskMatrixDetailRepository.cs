using System;
using System.Collections.Generic;

namespace KW.Domain
{
    public interface IStageTahunRiskMatrixDetailRepository
    {
        IEnumerable<StageTahunRiskMatrixDetail> GetByRiskMatrixProjectId(int riskMatrixProjectId);
        IEnumerable<StageTahunRiskMatrixDetail> GetByStageTahunRiskMatrixId(int stageTahunRiskMatrixId);
        void Insert(StageTahunRiskMatrixDetail model);
        void Update(StageTahunRiskMatrixDetail model);
        void Delete(int riskMatrixProjectId);
        bool IsExist(int id, int stageTahunRiskMatrixId);
        bool IsExist(int riskMatrixProjectId);
    }
}
