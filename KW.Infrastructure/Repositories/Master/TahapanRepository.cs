using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;


namespace KW.Infrastructure.Repositories
{
    public class TahapanRepository : ITahapanRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public TahapanRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Tahapan Get(int id)
        {
            return _databaseContext.Tahapans.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Tahapan> GetAll()
        {
            //return _databaseContext.Tahapans.AsQueryable();
            return _databaseContext.Tahapans.Where(x => x.IsDelete == false).ToList();

        }

        public void Insert(Tahapan model)
        {
            _databaseContext.Tahapans.Add(model);
        }

        public bool IsExist(int id, string namaTahapan)
        {
            var results = _databaseContext.Tahapans.Where(x => x.NamaTahapan.ToLower() == namaTahapan.ToLower() && x.IsDelete == false && x.Id != id).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public bool IsExist(string namaTahapan)
        {
            var results = _databaseContext.Tahapans.Where(x => x.NamaTahapan.ToLower() == namaTahapan.ToLower() && x.IsDelete == false).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public void Update(Tahapan model)
        {
            _databaseContext.Tahapans.Attach(model);
            _databaseContext.Entry(model).State = EntityState.Modified;
        }

        public void Delete(int id, int deleteBy, DateTime deleteDate)
        {
            throw new NotImplementedException();
        }
    }
}
