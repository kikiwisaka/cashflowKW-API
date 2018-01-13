using System.Collections.Generic;

namespace KW.Domain
{
    public interface IIncomeRepository
    {
        Income Get(int id);
        IEnumerable<Income> GetAll();
        IEnumerable<Income> GetByMonthYear(int month, int year);
        IEnumerable<Income> GetByToday(int date, int month, int year);
        void Insert(Income model);
        void Update(Income model);
        bool IsExist(int id, string incomeName, int date, int month, int year);
        bool IsExist(string incomeName, int date, int month, int year);
    }
}
