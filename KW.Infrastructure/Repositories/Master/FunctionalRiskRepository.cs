using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;


namespace KW.Infrastructure.Repositories
{
    public class FunctionalRiskRepository : IFunctionalRiskRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public FunctionalRiskRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public FunctionalRisk Get(int id)
        {
            return _databaseContext.FunctionalRisks.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<FunctionalRisk> GetAll()
        {
            //return _databaseContext.FunctionalRisks.AsQueryable();
            return _databaseContext.FunctionalRisks.Where(x => x.IsDelete == false).ToList();

        }

        public void Insert(FunctionalRisk model)
        {
            _databaseContext.FunctionalRisks.Add(model);
        }

        public bool IsExist(int id, string definisi)
        {
            var results = _databaseContext.FunctionalRisks.Where(x => x.Id != id && x.Definisi == definisi && x.IsDelete == false).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public bool IsExist(string definisi)
        {
            var results = _databaseContext.FunctionalRisks.Where(x => x.Definisi == definisi && x.IsDelete == false).ToList();
            if (results.Count > 0)
                return true;

            return false;
        }

        public void Update(FunctionalRisk model)
        {
            _databaseContext.FunctionalRisks.Attach(model);
            _databaseContext.Entry(model).State = EntityState.Modified;
        }

        public void Delete(int id, int deleteBy, DateTime deleteDate)
        {
            throw new NotImplementedException();
        }
    }
}
