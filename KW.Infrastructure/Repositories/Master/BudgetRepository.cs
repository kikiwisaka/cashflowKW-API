using KW.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace KW.Infrastructure.Repositories
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public BudgetRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Budget Get(int id)
        {
            return _databaseContext.Budgets.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Budget> GetAll()
        {
            return _databaseContext.Budgets.Where(x => x.IsDeleted == false).ToList();
        }

        public void Insert(Budget model)
        {
            _databaseContext.Budgets.Add(model);
        }

        public bool IsExist(string budgetName)
        {
            var result = _databaseContext.Budgets.Where(x => x.BudgetName == budgetName).ToList();
            if (result.Count > 0)
                return true;

            return false;
        }

        public bool IsExist(int id, string budgetName)
        {
            var result = _databaseContext.Budgets.Where(x => x.BudgetName == budgetName && x.Id != id).ToList();
            if (result.Count > 0)
                return true;

            return false;
        }

        public void Update(Budget model)
        {
            _databaseContext.Budgets.Attach(model);
            _databaseContext.Entry(model).State = EntityState.Modified;
        }
    }
}
