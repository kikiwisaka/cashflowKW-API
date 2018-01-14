using System.Collections.Generic;
using System;

namespace KW.Domain
{
    public interface IIncomeRepository
    {
        Income Get(int id);
        IEnumerable<Income> GetAll();
        IEnumerable<Income> GetByMonthYear(int month, int year);
        IEnumerable<Income> GetByToday(DateTime date);
        void Insert(Income model);
        void Update(Income model);
        bool IsExist(int id, string incomeName, DateTime date);
        bool IsExist(string incomeName, DateTime date);
    }
}
