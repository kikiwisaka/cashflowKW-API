using KW.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System;

namespace KW.Infrastructure.Repositories
{
    public class ExpenditureRepository : IExpenditureRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public ExpenditureRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Expenditure Get(int id)
        {
            return _databaseContext.Expenditures.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Expenditure> GetAll()
        {
            return _databaseContext.Expenditures.Where(x => x.IsDeleted == false).ToList();
        }

        public IEnumerable<Expenditure> GetByMonthYear(int month, int year)
        {
            return _databaseContext.Expenditures.Where(x => x.ExpenditureDate.Month == month && x.ExpenditureDate.Year == year && x.IsDeleted == false).ToList();
        }

        public Expenditure GetByExpenditureDate(DateTime date)
        {
            return _databaseContext.Expenditures.FirstOrDefault(x => x.ExpenditureDate.Date == date && x.IsDeleted == false);
        }

        public void Insert(Expenditure model)
        {
            _databaseContext.Expenditures.Add(model);
        }

        public bool IsExist(DateTime expenditureDate)
        {
            var result = _databaseContext.Expenditures.Where(x => x.ExpenditureDate == expenditureDate && x.IsDeleted == false).ToList();
            if (result.Count > 0)
                return true;

            return false;
        }

        public bool IsExist(int id, DateTime expenditureDate)
        {
            var result = _databaseContext.Expenditures.Where(x => x.Id != id && x.ExpenditureDate == expenditureDate && x.IsDeleted == false).ToList();
            if (result.Count > 0)
                return true;

            return false;
        }
        
        public void Update(Expenditure model)
        {
            _databaseContext.Expenditures.Attach(model);
            _databaseContext.Entry(model).State = EntityState.Modified;
        }
    }
}
