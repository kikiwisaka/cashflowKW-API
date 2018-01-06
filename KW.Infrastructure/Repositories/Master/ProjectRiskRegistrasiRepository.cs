using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;


namespace KW.Infrastructure.Repositories
{
    public class ProjectRiskRegistrasiRepository : IProjectRiskRegistrasiRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public ProjectRiskRegistrasiRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ProjectRiskRegistrasi Get(int id)
        {
            return _databaseContext.ProjectRiskRegistrasis.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<ProjectRiskRegistrasi> GetAll()
        {
            return _databaseContext.ProjectRiskRegistrasis.Where(x => x.IsDelete == false).ToList();
        }

        public IEnumerable<ProjectRiskRegistrasi> GetByProjectId(int projectId)
        {
            return _databaseContext.ProjectRiskRegistrasis.Where(x => x.ProjectId == projectId && x.IsDelete == false).ToList();
        }

        public void Insert(IList<ProjectRiskRegistrasi> collections)
        {
            foreach (var item in collections)
            {
                this.Insert(item);
            }
        }

        public void Insert(ProjectRiskRegistrasi model)
        {
            _databaseContext.ProjectRiskRegistrasis.Add(model);
        }

        public void DeleteByProjectId(int id)
        {
            var model = this.Get(id);
            if (model != null)
                _databaseContext.ProjectRiskRegistrasis.Remove(model);
        }

        public bool IsExist(int projectId, int riskRegistrasiId)
        {
            var results = _databaseContext.ProjectRiskRegistrasis.Where(x => x.ProjectId == projectId && x.RiskRegistrasiId == riskRegistrasiId && x.IsDelete == false).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }
    }
}
