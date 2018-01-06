using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace KW.Infrastructure.Repositories
{
    public class CorrelationRiskAntarSektorRepository : ICorrelationRiskAntarSektorRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public CorrelationRiskAntarSektorRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public CorrelationRiskAntarSektor Get(int id)
        {
            return _databaseContext.CorrelationRiskAntarSektors.SingleOrDefault(x => x.Id == id);
        }

        public CorrelationRiskAntarSektor GetByProjectIdScenarioId(int projectId, int scenarioId)
        {
            return _databaseContext.CorrelationRiskAntarSektors.SingleOrDefault(x => x.ProjectId == projectId && x.ScenarioId == scenarioId && x.IsDelete == false);
        }

        public IEnumerable<CorrelationRiskAntarSektor> GetByScenarioDefault(int scenarioId)
        {
            return _databaseContext.CorrelationRiskAntarSektors.Where(x => x.ScenarioId == scenarioId && x.IsDelete == false).ToList();
        }

        public CorrelationRiskAntarSektor GetByScenarioId(int scnearioId)
        {
            return _databaseContext.CorrelationRiskAntarSektors.SingleOrDefault(x => x.ScenarioId == scnearioId && x.IsDelete == false);
        }

        public IEnumerable<CorrelationRiskAntarSektor> GetAll()
        {
            return _databaseContext.CorrelationRiskAntarSektors.Where(x => x.IsDelete == false).ToList();
        }

        public void Insert(CorrelationRiskAntarSektor model)
        {
            _databaseContext.CorrelationRiskAntarSektors.Add(model);
        }

        public bool IsExist(int scenarioId)
        {
            var results = _databaseContext.CorrelationRiskAntarSektors.Where(x => x.ScenarioId == scenarioId&& x.IsDelete == false).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }
        public void Update(CorrelationRiskAntarSektor model)
        {
            _databaseContext.CorrelationRiskAntarSektors.Attach(model);
            _databaseContext.Entry(model).State = EntityState.Modified;
        }

        public void Delete(int id, int deleteBy, DateTime deleteDate)
        {
            var model = this.Get(id);
            if (model != null)
                _databaseContext.CorrelationRiskAntarSektors.Remove(model);
        }
    }
}
