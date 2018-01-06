using KW.Domain;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Data.Entity;

namespace KW.Infrastructure.Repositories
{
    public class CorrelatedProjectDetailRepository : ICorrelatedProjectDetailRepository
    {
        private readonly IDatabaseContext _databaseContext;
        public CorrelatedProjectDetailRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public CorrelatedProjectDetail Get(int id)
        {
            return _databaseContext.CorrelatedProjectDetails.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<CorrelatedProjectDetail> GetByCorrelatedProjectId(int correlatedProjectId)
        {
            return _databaseContext.CorrelatedProjectDetails.Where(x => x.CorrelatedProjectId== correlatedProjectId).ToList();
        }

        public void Insert(CorrelatedProjectDetail model)
        {
            _databaseContext.CorrelatedProjectDetails.Add(model);
        }

        public CorrelatedProjectDetail IsExisitOnAdding(int correlataedProjectId, int projectIdRow, int projectIdCol)
        {
            return _databaseContext.CorrelatedProjectDetails.Where(x => x.CorrelatedProjectId == correlataedProjectId && x.ProjectIdRow == projectIdRow && x.ProjectIdCol == projectIdCol).FirstOrDefault();
        }

        public void Update(CorrelatedProjectDetail model)
        {
            _databaseContext.CorrelatedProjectDetails.Attach(model);
            _databaseContext.Entry(model).State = EntityState.Modified;
        }
    }
}
