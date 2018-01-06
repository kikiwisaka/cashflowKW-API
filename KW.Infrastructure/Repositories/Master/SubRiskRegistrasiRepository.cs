using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;


namespace KW.Infrastructure.Repositories
{
    public class SubRiskRegistrasiRepository : ISubRiskRegistrasiRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public SubRiskRegistrasiRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public SubRiskRegistrasi Get(int id)
        {
            return _databaseContext.SubRiskRegistrasis.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<SubRiskRegistrasi> GetAll()
        {
            //return _databaseContext.Tahapans.AsQueryable();
            return _databaseContext.SubRiskRegistrasis.Where(x => x.IsDelete == false).ToList();

        }
        public IEnumerable<SubRiskRegistrasi> GetByRiskId(int riskId)
        {
            return _databaseContext.SubRiskRegistrasis.Where(x => x.RiskRegistrasiId == riskId && x.IsDelete == false).ToList();

        }
        public void Insert(SubRiskRegistrasi model)
        {
            _databaseContext.SubRiskRegistrasis.Add(model);
        }

        public bool IsExist(int id, string kodeRisk)
        {
            var results = _databaseContext.SubRiskRegistrasis.Where(x => x.KodeRisk.ToLower() == kodeRisk.ToLower() && x.IsDelete == false && x.Id != id).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public bool IsExist(string kodeRisk)
        {
            var results = _databaseContext.SubRiskRegistrasis.Where(x => x.KodeRisk.ToLower() == kodeRisk.ToLower() && x.IsDelete == false).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public void Update(SubRiskRegistrasi model)
        {
            _databaseContext.SubRiskRegistrasis.Attach(model);
            _databaseContext.Entry(model).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var model = this.Get(id);
            if (model != null)
                _databaseContext.SubRiskRegistrasis.Remove(model);
        }
    }
}
