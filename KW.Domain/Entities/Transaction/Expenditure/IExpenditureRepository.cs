using System.Collections.Generic;
using System;

namespace KW.Domain
{
    public interface IExpenditureRepository
    {
        Expenditure Get(int id);
        IEnumerable<Expenditure> GetAll();
        IEnumerable<Expenditure> GetByMonthYear(int month, int year);
        Expenditure GetByExpenditureDate(DateTime date);
        void Insert(Expenditure model);
        void Update(Expenditure model);
        bool IsExist(int id, DateTime expenditureDate);
        bool IsExist(DateTime expenditureDate);
    }
}
