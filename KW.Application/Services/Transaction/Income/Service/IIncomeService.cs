using KW.Application.Params;
using KW.Domain;
using System;
using System.Collections.Generic;

namespace KW.Application
{
    public interface IIncomeService
    {
        IEnumerable<Income> GetAll();
        Income Get(int id);
        int Add(IncomeParam param);
        int Update(int id, IncomeParam param);
        int Delete(int id, int updatedBy, DateTime updatedDate);
    }
}
