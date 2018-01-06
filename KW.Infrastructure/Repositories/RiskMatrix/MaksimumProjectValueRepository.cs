using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace KW.Infrastructure.Repositories
{
    public class MaksimumProjectValueRepository : IMaksimumProjectValueRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public MaksimumProjectValueRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public MaksimumProjectValue Get(int id)
        {
            return _databaseContext.MaksimumProjectValues.SingleOrDefault(x => x.Id == id);
        }

        public MaksimumProjectValue GetByScenarioId(int scenarioId)
        {
            return _databaseContext.MaksimumProjectValues.SingleOrDefault(x => x.ScenarioId == scenarioId);
        }

        public IEnumerable<MaksimumProjectValue> GetAll()
        {
            return _databaseContext.MaksimumProjectValues.Where(x => x.IsDelete == false).ToList();
        }

        public void Insert(MaksimumProjectValue model)
        {
            _databaseContext.MaksimumProjectValues.Add(model);
        }

        public bool IsExist(int scenarioId)
        {
            var results = _databaseContext.MaksimumProjectValues.Where(x => x.ScenarioId == scenarioId && x.IsDelete == false).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public bool IsExist(int id, int scenarioId)
        {
            var results = _databaseContext.MaksimumProjectValues.Where(x => x.ScenarioId == scenarioId && x.IsDelete == false && x.Id != id).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public void Update(MaksimumProjectValue model)
        {
            _databaseContext.MaksimumProjectValues.Attach(model);
            _databaseContext.Entry(model).State = EntityState.Modified;
        }

        public void Delete(int id, int deleteBy, DateTime deleteDate)
        {
            throw new NotImplementedException();
        }
    }
}
