using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;


namespace KW.Infrastructure.Repositories
{
    public class PMNRepository : IPMNRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public PMNRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public PMN Get(int id)
        {
            return _databaseContext.PMNs.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<PMN> GetAll()
        {
            //return _databaseContext.PMNs.AsQueryable();
            return _databaseContext.PMNs.Where(x => x.IsDelete == false).ToList();

        }

        public void Insert(PMN model)
        {
            _databaseContext.PMNs.Add(model);
        }

        public bool IsExist(int id, int pmnToModalDasarCap)
        {
            var results = _databaseContext.PMNs.Where(x => x.PMNToModalDasarCap == pmnToModalDasarCap && x.IsDelete == false && x.Id != id).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public bool IsExist(int pmnToModalDasarCap)
        {
            var results = _databaseContext.PMNs.Where(x => x.PMNToModalDasarCap == pmnToModalDasarCap && x.IsDelete == false).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public void Update(PMN model)
        {
            _databaseContext.PMNs.Attach(model);
            _databaseContext.Entry(model).State = EntityState.Modified;
        }

        public void Delete(int id, int deleteBy, DateTime deleteDate)
        {
            throw new NotImplementedException();
        }
    }
}
