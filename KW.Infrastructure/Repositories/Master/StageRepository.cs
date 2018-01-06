using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace KW.Infrastructure.Repositories
{
    public class StageRepository : IStageRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public StageRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Stage Get(int id)
        {
            return _databaseContext.Stages.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Stage> GetAll()
        {
            return _databaseContext.Stages.Where(x => x.IsDelete == false).ToList();
        }

        public void Insert(Stage model)
        {
            _databaseContext.Stages.Add(model);
        }

        public bool IsExist(string namaStage)
        {
            var results = _databaseContext.Stages.Where(x => x.NamaStage.ToLower() == namaStage.ToLower() && x.IsDelete == false).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public bool IsExist(int id, string namaStage)
        {
            var results = _databaseContext.Stages.Where(x => x.NamaStage.ToLower() == namaStage.ToLower() && x.IsDelete == false && x.Id != id).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public void Update(Stage model)
        {
            _databaseContext.Stages.Attach(model);
            _databaseContext.Entry(model).State = EntityState.Modified;
        }

        public void Delete(int id, int deleteBy, DateTime deleteDate)
        {
            throw new NotImplementedException();
        }
    }
}
