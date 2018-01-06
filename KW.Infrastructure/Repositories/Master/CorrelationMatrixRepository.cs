using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace KW.Infrastructure.Repositories
{
    public class CorrelationMatrixRepository : ICorrelationMatrixRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public CorrelationMatrixRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public CorrelationMatrix Get(int id)
        {
            return _databaseContext.CorrelationMatrixs.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<CorrelationMatrix> GetAll()
        {
            return _databaseContext.CorrelationMatrixs.Where(x => x.IsDelete == false).OrderByDescending(o => o.Nilai).ToList();
        }

        public void Insert(CorrelationMatrix model)
        {
            _databaseContext.CorrelationMatrixs.Add(model);
        }

        public bool IsExist(string namaCorrelationMatrix)
        {
            var results = _databaseContext.CorrelationMatrixs.Where(x => x.NamaCorrelationMatrix == namaCorrelationMatrix && x.IsDelete == false).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public bool IsExist(int id, string namaCorrelationMatrix)
        {
            var results = _databaseContext.CorrelationMatrixs.Where(x => x.NamaCorrelationMatrix == namaCorrelationMatrix && x.IsDelete == false && x.Id != id).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public void Update(CorrelationMatrix model)
        {
            _databaseContext.CorrelationMatrixs.Attach(model);
            _databaseContext.Entry(model).State = EntityState.Modified;
        }
    }
}
