using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;


namespace KW.Infrastructure.Repositories
{
    public class MatrixRepository : IMatrixRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public MatrixRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Matrix Get(int? id)
        {
            return _databaseContext.Matrixs.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Matrix> GetAll()
        {
            //return _databaseContext.Matrixs.AsQueryable();
            return _databaseContext.Matrixs.Where(x => x.IsDelete == false).ToList();

        }

        public void Insert(Matrix model)
        {
            _databaseContext.Matrixs.Add(model);
        }

        public bool IsExist(int id, string namaMatrix)
        {
            var results = _databaseContext.Matrixs.Where(x => x.NamaMatrix == namaMatrix && x.IsDelete == false && x.Id != id).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public bool IsExist(string namaMatrix)
        {
            var results = _databaseContext.Matrixs.Where(x => x.NamaMatrix == namaMatrix && x.IsDelete == false).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public void Update(Matrix model)
        {
            _databaseContext.Matrixs.Attach(model);
            _databaseContext.Entry(model).State = EntityState.Modified;
        }

        public void Delete(int id, int deleteBy, DateTime deleteDate)
        {
            throw new NotImplementedException();
        }
    }
}
