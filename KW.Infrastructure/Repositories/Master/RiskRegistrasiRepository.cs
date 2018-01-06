using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;


namespace KW.Infrastructure.Repositories
{
    public class RiskRegistrasiRepository : IRiskRegistrasiRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public RiskRegistrasiRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public RiskRegistrasi Get(int id)
        {
            return _databaseContext.RiskRegistrasis.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<RiskRegistrasi> GetAll()
        {
            //return _databaseContext.Tahapans.AsQueryable();
            return _databaseContext.RiskRegistrasis.Where(x => x.IsDelete == false).OrderBy(o => o.KodeMRisk).ToList();

        }

        public void Insert(RiskRegistrasi model)
        {
            _databaseContext.RiskRegistrasis.Add(model);
        }

        public bool IsExist(int id, string namaCategoryRisk)
        {
            var results = _databaseContext.RiskRegistrasis.Where(x => x.NamaCategoryRisk.ToLower() == namaCategoryRisk.ToLower() && x.IsDelete == false && x.Id != id).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public bool IsExist(string kodeMRisk)
        {
            var results = _databaseContext.RiskRegistrasis.Where(x => x.KodeMRisk.ToLower() == kodeMRisk.ToLower() && x.IsDelete == false).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public void Update(RiskRegistrasi model)
        {
            _databaseContext.RiskRegistrasis.Attach(model);
            _databaseContext.Entry(model).State = EntityState.Modified;
        }

        public void Delete(int id, int deleteBy, DateTime deleteDate)
        {
            throw new NotImplementedException();
        }
    }
}
