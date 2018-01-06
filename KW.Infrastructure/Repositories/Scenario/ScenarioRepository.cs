using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace KW.Infrastructure.Repositories
{
    public class ScenarioRepository : IScenarioRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public ScenarioRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Scenario Get(int? id)
        {
            return _databaseContext.Scenarios.SingleOrDefault(x => x.Id == id);
        }

        public Scenario GetDefault()
        {
            return _databaseContext.Scenarios.SingleOrDefault(x => x.IsDefault == true && x.IsDelete == false);
        }

        public IEnumerable<Scenario> GetAll()
        {
            return _databaseContext.Scenarios.Where(x => x.IsDelete == false).ToList();
        }

        public void Insert(Scenario model)
        {
            _databaseContext.Scenarios.Add(model);
        }

        public bool IsExist(string namaScenario)
        {
            var results = _databaseContext.Scenarios.Where(x => x.NamaScenario.ToLower() == namaScenario.ToLower() && x.IsDelete == false).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public bool IsExist(int id, string namaScenario)
        {
            var results = _databaseContext.Scenarios.Where(x => x.NamaScenario.ToLower() == namaScenario.ToLower() && x.IsDelete == false && x.Id != id).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public void Update(Scenario model)
        {
            _databaseContext.Scenarios.Attach(model);
            _databaseContext.Entry(model).State = EntityState.Modified;
        }

        public void Delete(int id, int deleteBy, DateTime deleteDate)
        {
            var model = this.Get(id);
            if (model != null)
                _databaseContext.Scenarios.Remove(model);
        }
    }
}
