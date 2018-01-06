using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace KW.Infrastructure.Repositories
{
    public class StageTahunRiskMatrixRepository : IStageTahunRiskMatrixRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public StageTahunRiskMatrixRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public StageTahunRiskMatrix Get(int id)
        {
            return _databaseContext.StageTahunRiskMatrixs.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<StageTahunRiskMatrix> GetAll()
        {
            return _databaseContext.StageTahunRiskMatrixs.Where(x => x.IsDelete == false).ToList();
        }

        public IEnumerable<StageTahunRiskMatrix> GetByRiskMatrixProjectId(int riskMatrixProjectId)
        {
            return _databaseContext.StageTahunRiskMatrixs.Where(x => x.RiskMatrixProjectId == riskMatrixProjectId && x.IsDelete == false).ToList();
        }

        public StageTahunRiskMatrix GetByRiskMatrixProjectIdYear(int riskMatrixProjectId, int year)
        {
            return _databaseContext.StageTahunRiskMatrixs.SingleOrDefault(x => x.RiskMatrixProjectId == riskMatrixProjectId && x.IsDelete == false && x.Tahun == year);
        }

        public void Insert(StageTahunRiskMatrix model)
        {
            _databaseContext.StageTahunRiskMatrixs.Add(model);
        }

        public bool IsExist(int riskMatrixProjectId)
        {
            var results = _databaseContext.StageTahunRiskMatrixs.Where(x => x.RiskMatrixProjectId == riskMatrixProjectId && x.IsDelete == false).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public bool IsExist(int id, int riskMatrixProjectId)
        {
            var results = _databaseContext.StageTahunRiskMatrixs.Where(x => x.RiskMatrixProjectId == riskMatrixProjectId && x.IsDelete == false && x.Id != id).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public void Update(StageTahunRiskMatrix model)
        {
            _databaseContext.StageTahunRiskMatrixs.Attach(model);
            _databaseContext.Entry(model).State = EntityState.Modified;
        }

        public void Delete(int riskMatrixProjectId)
        {
            var result = _databaseContext.StageTahunRiskMatrixs.Where(x => x.RiskMatrixProjectId == riskMatrixProjectId).ToList();
            if (result.Count > 0)
            {
                foreach (var item in result)
                {
                    _databaseContext.StageTahunRiskMatrixs.Remove(item);
                }
            }
        }

        //public void Delete(int id, int deleteBy, DateTime deleteDate)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
