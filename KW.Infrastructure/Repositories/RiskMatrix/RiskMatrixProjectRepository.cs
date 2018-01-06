using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace KW.Infrastructure.Repositories
{
    public class RiskMatrixProjectRepository : IRiskMatrixProjectRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public RiskMatrixProjectRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public RiskMatrixProject Get(int id)
        {
            return _databaseContext.RiskMatrixProjects.SingleOrDefault(x => x.Id == id);
        }

        public RiskMatrixProject GetByScenarioIdProjectId(int scenarioId, int projectId)
        {
            return _databaseContext.RiskMatrixProjects.SingleOrDefault(x => x.ScenarioId == scenarioId && x.ProjectId == projectId);
        }

        public RiskMatrixProject GetByProjectId(int projectId)
        {
            return _databaseContext.RiskMatrixProjects.SingleOrDefault(x => x.ProjectId == projectId && x.IsDelete == false);
        }

        public IEnumerable<RiskMatrixProject> GetAll()
        {
            return _databaseContext.RiskMatrixProjects.Where(x => x.IsDelete == false).ToList();
        }

        public IEnumerable<RiskMatrixProject> GetAllData()
        {
            return _databaseContext.RiskMatrixProjects.AsQueryable();
        }

        public IEnumerable<RiskMatrixProject> GetByScenarioId(int scenarioId)
        {
            return _databaseContext.RiskMatrixProjects.Where(x => x.IsDelete == false && x.ScenarioId == scenarioId).ToList();
        }

        public IEnumerable<RiskMatrixProject> GetAllByScenarioId(int scenarioId)
        {
            return _databaseContext.RiskMatrixProjects.Where(x => x.ScenarioId == scenarioId).ToList();
        }

        public void Insert(RiskMatrixProject model)
        {
            _databaseContext.RiskMatrixProjects.Add(model);
        }

        public void Insert(IList<RiskMatrixProject> collections)
        {
            foreach (var item in collections)
            {
                this.Insert(item);
            }
        }

        public bool IsExist(int projectId)
        {
            var results = _databaseContext.RiskMatrixProjects.Where(x => x.ProjectId == projectId && x.IsDelete == false).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public bool IsExist(int id, int projectId)
        {
            var results = _databaseContext.RiskMatrixProjects.Where(x => x.ProjectId == projectId && x.IsDelete == false && x.Id != id).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public void Update(RiskMatrixProject model)
        {
            _databaseContext.RiskMatrixProjects.Attach(model);
            _databaseContext.Entry(model).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var result = _databaseContext.RiskMatrixProjects.Where(x => x.Id == id).FirstOrDefault();
            _databaseContext.RiskMatrixProjects.Remove(result);
        }

        
    }
}
