using KW.Domain;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Data.Entity;

namespace KW.Infrastructure.Repositories
{
    public class CorrelatedSektorDetailRepository : ICorrelatedSektorDetailRepository
    {
        private readonly IDatabaseContext _databaseContext;
        public CorrelatedSektorDetailRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public CorrelatedSektorDetail Get(int id)
        {
            return _databaseContext.CorrelatedSektorDetails.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<CorrelatedSektorDetail> GetByCorrelatedSektorId(int correlateedSektorId)
        {
            return _databaseContext.CorrelatedSektorDetails.Where(x => x.CorrelatedSektorId == correlateedSektorId).ToList();
        }

        public void Insert(IList<CorrelatedSektorDetail> collection)
        {
            foreach (var item in collection)
            {
                this.Insert(item);
            }
        }

        public void Insert(CorrelatedSektorDetail model)
        {
            _databaseContext.CorrelatedSektorDetails.Add(model);
        }

        public CorrelatedSektorDetail IsExisitOnAdding(int correlataedSektor, int riskRegistrasiIdRow, int riskRegistrasiIdCol)
        {
            return _databaseContext.CorrelatedSektorDetails.Where(x => x.CorrelatedSektorId == correlataedSektor && x.RiskRegistrasiIdRow == riskRegistrasiIdRow && x.RiskRegistrasiIdCol == riskRegistrasiIdCol).FirstOrDefault();
        }

        public void Update(CorrelatedSektorDetail model)
        {
            _databaseContext.CorrelatedSektorDetails.Attach(model);
            _databaseContext.Entry(model).State = EntityState.Modified;
        }
    }
}
