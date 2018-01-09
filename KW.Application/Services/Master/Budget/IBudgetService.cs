using KW.Application.Params;
using KW.Domain;
using System.Collections.Generic;

namespace KW.Application
{
    public interface IBudgetService
    {
        IEnumerable<Budget> GetAll();
        Budget Get(int id);
        int Add(BudgetParam param);
        int Update(int id, BudgetParam param);
    }
}
