using KW.Application.Params;
using KW.Domain;
using System;
using System.Collections.Generic;

namespace KW.Application
{
    public interface IExpenditureDetailService
    {
        IEnumerable<ExpenditureDetail> GetByDate(DateTime date);
        IEnumerable<ExpenditureDetail> GetByExpenditureId(int expenditureId);
        ExpenditureDetail Get(int id);
    }
}
