using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace KW.Infrastructure.Repositories
{
    public class RiskMatrixStageRepository : IRiskMatrixStageRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public RiskMatrixStageRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public RiskMatrixStage Get(int id)
        {
            return _databaseContext.RiskMatrixStages.SingleOrDefault(x => x.Id == id);
        }

        public RiskMatrixStage GetByProjectId(int projectId)
        {
            return _databaseContext.RiskMatrixStages.SingleOrDefault(x => x.ProjectId == projectId);
        }

        public IEnumerable<RiskMatrixStage> GetAll()
        {
            return _databaseContext.RiskMatrixStages.Where(x => x.IsDelete == false).ToList();
        }

        public void Insert(RiskMatrixStage model)
        {
            _databaseContext.RiskMatrixStages.Add(model);
        }

        public bool IsExist(int projectId)
        {
            var results = _databaseContext.RiskMatrixStages.Where(x => x.ProjectId == projectId && x.IsDelete == false).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public bool IsExist(int id, int projectId)
        {
            var results = _databaseContext.RiskMatrixStages.Where(x => x.ProjectId == projectId && x.IsDelete == false && x.Id != id).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public void Update(RiskMatrixStage model)
        {
            _databaseContext.RiskMatrixStages.Attach(model);
            _databaseContext.Entry(model).State = EntityState.Modified;
        }

        public void Delete(int id, int deleteBy, DateTime deleteDate)
        {
            throw new NotImplementedException();
        }
    }
}
