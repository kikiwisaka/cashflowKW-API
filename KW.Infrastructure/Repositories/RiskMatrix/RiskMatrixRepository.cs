using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace KW.Infrastructure.Repositories
{
    public class RiskMatrixRepository : IRiskMatrixRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public RiskMatrixRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public RiskMatrix Get(int id)
        {
            return _databaseContext.RiskMatrixs.SingleOrDefault(x => x.Id == id);
        }

        public RiskMatrix GetByScenarioId(int scenarioId)
        {
            return _databaseContext.RiskMatrixs.SingleOrDefault(x => x.ScenarioId == scenarioId);
        }

        public IEnumerable<RiskMatrix> GetAll()
        {
            return _databaseContext.RiskMatrixs.Where(x => x.IsDelete == false).ToList();
        }

        public void Insert(RiskMatrix model)
        {
            _databaseContext.RiskMatrixs.Add(model);
        }

        public bool IsExist(int scenarioId)
        {
            var results = _databaseContext.RiskMatrixs.Where(x => x.ScenarioId == scenarioId && x.IsDelete == false).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public bool IsExist(int id, int scenarioId)
        {
            var results = _databaseContext.RiskMatrixs.Where(x => x.ScenarioId == scenarioId && x.IsDelete == false && x.Id != id).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public void Update(RiskMatrix model)
        {
            _databaseContext.RiskMatrixs.Attach(model);
            _databaseContext.Entry(model).State = EntityState.Modified;
        }

        public void Delete(int id, int deleteBy, DateTime deleteDate)
        {
            throw new NotImplementedException();
        }
    }
}
