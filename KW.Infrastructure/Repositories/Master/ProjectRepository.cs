using KW.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace KW.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IDatabaseContext _databaseContext;
        public ProjectRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void Delete(int id, int? deleteBy, DateTime? deleteDate)
        {
            throw new NotImplementedException();
        }

        public Project Get(int id)
        {
            return _databaseContext.Projects.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Project> GetAll()
        {
            return _databaseContext.Projects.Where(x => x.IsDelete == false).ToList();
        }

        public void Insert(Project model)
        {
            _databaseContext.Projects.Add(model);
        }

        public bool IsExist(int id, string namaProject)
        {
            var results = _databaseContext.Projects.Where(x => x.NamaProject.ToLower() == namaProject.ToLower() && x.IsDelete == false && x.Id != id).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public bool IsExist(string namaProject)
        {
            var results = _databaseContext.Projects.Where(x => x.NamaProject.ToLower() == namaProject.ToLower() && x.IsDelete == false).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public void Update(Project model)
        {
            _databaseContext.Projects.Attach(model);
            _databaseContext.Entry(model).State = EntityState.Modified;
        }
    }
}
