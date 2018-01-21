using KW.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System;

namespace KW.Infrastructure.Repositories
{
    public class ExpenditureDetailRepository : IExpenditureDetailRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public ExpenditureDetailRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ExpenditureDetail Get(int id)
        {
            return _databaseContext.ExpenditureDetails.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<ExpenditureDetail> GetByExpenditureId(int expenditureId)
        {
            return _databaseContext.ExpenditureDetails.Where(x => x.ExpenditureId == expenditureId).ToList();
        }

        public ExpenditureDetail GetByExpenditureIdExpenditureDetailId(int expenditureId, int expenditureDetailId)
        {
            return _databaseContext.ExpenditureDetails.FirstOrDefault(x => x.ExpenditureId == expenditureId && x.Id == expenditureDetailId);
        }

        public IEnumerable<ExpenditureDetail> GetByMonthYear(DateTime month, DateTime year)
        {
            throw new NotImplementedException();
        }

        public void Insert(ExpenditureDetail model)
        {
            _databaseContext.ExpenditureDetails.Add(model);
        }

        public bool IsExist(string expenditureName)
        {
            throw new NotImplementedException();
        }

        public bool IsExist(int id, string expenditureName)
        {
            throw new NotImplementedException();
        }

        public void Update(ExpenditureDetail model)
        {
            _databaseContext.ExpenditureDetails.Attach(model);
            _databaseContext.Entry(model).State = EntityState.Modified;
        }
    }
}
