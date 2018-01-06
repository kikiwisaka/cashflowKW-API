using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace KW.Infrastructure.Repositories
{
    public class CorrelatedSektorRepository : ICorrelatedSektorRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public CorrelatedSektorRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        
        public CorrelatedSektor Get(int id)
        {
            return _databaseContext.CorrelatedSektors.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<CorrelatedSektor> GetAll()
        {
            return _databaseContext.CorrelatedSektors.Where(x => x.IsDelete == false).ToList();
        }

        public IEnumerable<CorrelatedSektor> GetByScenarioDefaultId(int scenarioId)
        {
            return _databaseContext.CorrelatedSektors.Where(x => x.IsDelete == false && x.ScenarioId == scenarioId).ToList();
            //return _databaseContext.CorrelatedSektors.Where(x => x.IsDelete == false).ToList();
        }

        public IEnumerable<CorrelatedSektor> GetByScenarioIdIsZero()
        {
            return _databaseContext.CorrelatedSektors.Where(x => x.IsDelete == false && x.ScenarioId == 0).ToList();
        }

        public void Insert(CorrelatedSektor model)
        {
            _databaseContext.CorrelatedSektors.Add(model);
        }

        public void Insert(IList<CorrelatedSektor> collections)
        {
            foreach (var item in collections)
            {
                this.Insert(item);
            }
        }

        public void Update(CorrelatedSektor model)
        {
            _databaseContext.CorrelatedSektors.Attach(model);
            _databaseContext.Entry(model).State = EntityState.Modified;
        }

        public void Delete(int id, int deleteBy, DateTime deleteDate)
        {
            var model = this.Get(id);
            if(model != null)
                _databaseContext.CorrelatedSektors.Remove(model);
        }

    }
}
