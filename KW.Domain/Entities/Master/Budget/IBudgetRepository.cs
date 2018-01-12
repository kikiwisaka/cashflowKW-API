using System.Collections.Generic;

namespace KW.Domain
{
    public interface IBudgetRepository
    {
        Budget Get(int id);
        IEnumerable<Budget> GetAll();
        void Insert(Budget model);
        void Update(Budget model);
        bool IsExist(int id, string budgetName);
        bool IsExist(string budgetName);
    }
}
