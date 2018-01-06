using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace KW.Infrastructure.Repositories
{
    public class ProjectRiskStatusRepository : IProjectRiskStatusRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public ProjectRiskStatusRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IEnumerable<ProjectRiskStatus> GetByProjectId(int projectId)
        {
            return _databaseContext.ProjectRiskStatus.Where(x => x.ProjectId == projectId).ToList();
        }

        public ProjectRiskStatus Get(int id)
        {
            return _databaseContext.ProjectRiskStatus.Where(x => x.Id == id).FirstOrDefault();
        }

        public void Insert(ProjectRiskStatus model)
        {
            _databaseContext.ProjectRiskStatus.Add(model);
        }

        public void Update(ProjectRiskStatus model)
        {
            _databaseContext.ProjectRiskStatus.Attach(model);
            _databaseContext.Entry(model).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            var model = this.Get(id);
            if (model != null)
                _databaseContext.ProjectRiskStatus.Remove(model);
        }

        public IEnumerable<ProjectRiskStatus> GetByKodeMRisk(string kodeMRisk)
        {
            return _databaseContext.ProjectRiskStatus.Where(x => x.KodeMRisk == kodeMRisk).ToList();
        }

        public IEnumerable<ProjectRiskStatus> GetAll()
        {
            return _databaseContext.ProjectRiskStatus.ToList();
        }
    }
}
