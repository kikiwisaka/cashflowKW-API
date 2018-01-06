using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace KW.Infrastructure.Repositories
{
    public class StageTahunRiskMatrixDetailRepository : IStageTahunRiskMatrixDetailRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public StageTahunRiskMatrixDetailRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IEnumerable<StageTahunRiskMatrixDetail> GetByRiskMatrixProjectId(int riskMatrixProjectId)
        {
            return _databaseContext.StageTahunRiskMatrixDetails.Where(x => x.RiskMatrixProjectId == riskMatrixProjectId).ToList();
        }

        public IEnumerable<StageTahunRiskMatrixDetail> GetByStageTahunRiskMatrixId(int stageTahunRiskMatrixId)
        {
            return _databaseContext.StageTahunRiskMatrixDetails.Where(x => x.StageTahunRiskMatrixId == stageTahunRiskMatrixId && x.IsDelete == false).ToList();
        }

        public void Insert(StageTahunRiskMatrixDetail model)
        {
            _databaseContext.StageTahunRiskMatrixDetails.Add(model);
        }

        public void Update(StageTahunRiskMatrixDetail model)
        {
            _databaseContext.StageTahunRiskMatrixDetails.Attach(model);
            _databaseContext.Entry(model).State = EntityState.Modified;
        }

        public bool IsExist(int riskMatrixProjectId)
        {
            var result = _databaseContext.StageTahunRiskMatrixDetails.Where(x => x.RiskMatrixProjectId == riskMatrixProjectId).ToList();
            if (result.Count > 0)
                return true;

            return false;
        }

        public bool IsExist(int id, int stageTahunRiskMatrixId)
        {
            var results = _databaseContext.StageTahunRiskMatrixDetails.Where(x => x.StageTahunRiskMatrixId == stageTahunRiskMatrixId && x.IsDelete == false && x.Id != id).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }
        
        public void Delete(int riskMatrixProjectId)
        {
            var result = _databaseContext.StageTahunRiskMatrixDetails.Where(x => x.RiskMatrixProjectId == riskMatrixProjectId).ToList();
            if(result.Count > 0)
            {
                foreach (var item in result)
                {
                    _databaseContext.StageTahunRiskMatrixDetails.Remove(item);
                }
            }
        }
    }
}
