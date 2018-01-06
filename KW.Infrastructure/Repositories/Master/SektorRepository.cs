using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;


namespace KW.Infrastructure.Repositories
{
    public class SektorRepository : ISektorRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public SektorRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Sektor Get(int id)
        {
            return _databaseContext.Sektors.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Sektor> GetAll()
        {
            //return _databaseContext.Sektors.AsQueryable();
            return _databaseContext.Sektors.Where(x => x.IsDelete == false).ToList();

        }

        public void Insert(Sektor model)
        {
            _databaseContext.Sektors.Add(model);
        }

        public bool IsExist(int id, string namaSektor)
        {
            var results = _databaseContext.Sektors.Where(x => x.NamaSektor.ToLower() == namaSektor.ToLower() && x.IsDelete == false && x.Id != id).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public bool IsExist(string namaSektor)
        {
            var results = _databaseContext.Sektors.Where(x => x.NamaSektor.ToLower() == namaSektor.ToLower() && x.IsDelete == false).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public void Update(Sektor model)
        {
            _databaseContext.Sektors.Attach(model);
            _databaseContext.Entry(model).State = EntityState.Modified;
        }

        public void Delete(int id, int deleteBy, DateTime deleteDate)
        {
            throw new NotImplementedException();
        }
    }
}
