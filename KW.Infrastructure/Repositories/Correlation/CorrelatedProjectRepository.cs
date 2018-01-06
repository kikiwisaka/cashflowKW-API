using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace KW.Infrastructure.Repositories
{
    public class CorrelatedProjectRepository : ICorrelatedProjectRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public CorrelatedProjectRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        
        public CorrelatedProject Get(int id)
        {
            return _databaseContext.CorrelatedProjects.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<CorrelatedProject> GetAll()
        {
            return _databaseContext.CorrelatedProjects.Where(x => x.IsDelete == false).ToList();
        }

        public IEnumerable<CorrelatedProject> GetByScenarioId(int scenarioId)
        {
            return _databaseContext.CorrelatedProjects.Where(x => x.ScenarioId == scenarioId && x.IsDelete == false).ToList();
        }

        public void Insert(CorrelatedProject model)
        {
            _databaseContext.CorrelatedProjects.Add(model);
        }

        public void Update(CorrelatedProject model)
        {
            _databaseContext.CorrelatedProjects.Attach(model);
            _databaseContext.Entry(model).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            CorrelatedProject model = this.Get(id);
            if (model == null)
                return;

            _databaseContext.CorrelatedProjects.Remove(model);
        }

    }
}
