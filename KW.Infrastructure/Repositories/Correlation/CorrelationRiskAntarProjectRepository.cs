using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;

namespace KW.Infrastructure.Repositories
{
    public class CorrelationRiskAntarProjectRepository : ICorrelationRiskAntarProjectRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public CorrelationRiskAntarProjectRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public CorrelationRiskAntarProject Get(int id)
        {
            return _databaseContext.CorrelationRiskAntarProjects.SingleOrDefault(x => x.Id == id);
        }
        public IEnumerable<CorrelationRiskAntarProject> GetByCorrelationRiskAntarSectorId(int correlationSectorId)
        {
            return _databaseContext.CorrelationRiskAntarProjects.Where(x => x.CorrelationRiskAntarSektorId == correlationSectorId && x.IsDelete == false).ToList();
        }

        public IEnumerable<CorrelationRiskAntarProject> GetByScenarioDefaultId(int scenarioId)
        {
            return _databaseContext.CorrelationRiskAntarProjects.Where(x => x.CorrelationRiskAntarSektorId == scenarioId && x.IsDelete == false).ToList();
        }

        public IEnumerable<CorrelationRiskAntarProject> GetAll()
        {
            return _databaseContext.CorrelationRiskAntarProjects.Where(x => x.IsDelete == false).ToList();
        }

        public IEnumerable<CorrelationRiskAntarProject> GetByCorrelationRiskAntarSektorId(int correlationRiskAntarSektorId)
        {
            return _databaseContext.CorrelationRiskAntarProjects.Where(x => x.IsDelete == false && x.CorrelationRiskAntarSektorId == correlationRiskAntarSektorId).ToList();
        }

        public void Insert(CorrelationRiskAntarProject model)
        {
            _databaseContext.CorrelationRiskAntarProjects.Add(model);
        }

        public void Update(CorrelationRiskAntarProject model)
        {
            _databaseContext.CorrelationRiskAntarProjects.Attach(model);
            _databaseContext.Entry(model).State = EntityState.Modified;
        }

        public void Delete(int id, int deleteBy, DateTime deleteDate)
        {
            throw new NotImplementedException();
        }

        public void Insert(IList<CorrelationRiskAntarProject> collections)
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
                _databaseContext.CorrelationRiskAntarProjects.Remove(model);
        }

        public CorrelationRiskAntarProject isExistOnAdding(int correlationRiskAntarSektorId, int projectIdRow, int projectIdCol)
        {
            return _databaseContext.CorrelationRiskAntarProjects.Where(x => x.CorrelationRiskAntarSektorId == correlationRiskAntarSektorId && x.ProjectIdRow == projectIdRow && x.ProjectIdCol == projectIdRow).FirstOrDefault();
        }

    }
}
