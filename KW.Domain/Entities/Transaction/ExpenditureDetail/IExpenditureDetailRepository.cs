using System.Collections.Generic;
using System;

namespace KW.Domain
{
    public interface IExpenditureDetailRepository
    {
        ExpenditureDetail Get(int id);
        IEnumerable<ExpenditureDetail> GetByExpenditureId(int expenditureId);
        ExpenditureDetail GetByExpenditureIdExpenditureDetailId(int expenditureId, int expenditureDetailId);
        IEnumerable<ExpenditureDetail> GetByMonthYear(DateTime month, DateTime year);
        void Insert(ExpenditureDetail model);
        void Update(ExpenditureDetail model);
        bool IsExist(int id, string expenditureName);
        bool IsExist(string expenditureName);
    }
}
