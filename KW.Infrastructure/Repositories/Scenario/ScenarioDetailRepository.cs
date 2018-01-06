using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;

namespace KW.Infrastructure.Repositories
{
    public class ScenarioDetailRepository :IScenarioDetailRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public ScenarioDetailRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ScenarioDetail Get(int id)
        {
            return _databaseContext.ScenarioDetails.SingleOrDefault(x => x.Id == id);
        }
        public IEnumerable<ScenarioDetail> GetByScenarioId(int scenarioId)
        {
            return _databaseContext.ScenarioDetails.Where(x => x.ScenarioId == scenarioId && x.IsDelete == false).ToList();
        }

        public IEnumerable<ScenarioDetail> GetAll()
        {
            return _databaseContext.ScenarioDetails.Where(x => x.IsDelete == false).ToList();
        }

        public void Insert(ScenarioDetail model)
        {
            _databaseContext.ScenarioDetails.Add(model);
        }

        public void Update(ScenarioDetail model)
        {
            _databaseContext.ScenarioDetails.Attach(model);
            _databaseContext.Entry(model).State = EntityState.Modified;
        }

        public void Delete(int id, int deleteBy, DateTime deleteDate)
        {
            throw new NotImplementedException();
        }

        public void Insert(IList<ScenarioDetail> collections)
        {
            foreach (var item in collections)
            {
                this.Insert(item);
            }
        }

        public void Delete(int id)
        {
            var model = this.Get(id);
            if (model != null)
                _databaseContext.ScenarioDetails.Remove(model);
        }
    }
}
