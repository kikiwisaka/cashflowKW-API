using KW.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System;

namespace KW.Infrastructure.Repositories
{
    public class IncomeRepository : IIncomeRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public IncomeRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Income Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Income> GetAll()
        {
            return _databaseContext.Incomes.AsQueryable();
        }

        public IEnumerable<Income> GetByMonthYear(int month, int year)
        {
            return _databaseContext.Incomes.Where(x => x.IncomeDate.Month == month && x.IncomeDate.Year == year && x.IsDeleted == false).ToList();
        }

        public IEnumerable<Income> GetByToday(DateTime date)
        {
            return _databaseContext.Incomes.Where(x => x.IncomeDate == date && x.IsDeleted == false).ToList();
        }

        public void Insert(Income model)
        {
            _databaseContext.Incomes.Add(model);
        }

        public bool IsExist(string incomeName, DateTime date)
        {
            var result = _databaseContext.Incomes.Where(x => x.IncomeName == incomeName && x.IncomeDate == date && x.IsDeleted == false).ToList();
            if (result.Count > 0)
                return true;

            return false;
        }

        public bool IsExist(int id, string incomeName, DateTime date)
        {
            var result = _databaseContext.Incomes.Where(x => x.Id != id && x.IncomeName == incomeName && x.IncomeDate == date && x.IsDeleted == false).ToList();
            if (result.Count > 0)
                return true;

            return false;
        }

        public void Update(Income model)
        {
            _databaseContext.Incomes.Attach(model);
            _databaseContext.Entry(model).State = EntityState.Modified;
        }
    }
}
