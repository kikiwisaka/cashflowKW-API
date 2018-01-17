using KW.Application.Params;
using KW.Domain;
using System;
using System.Collections.Generic;

namespace KW.Application
{
    public interface IExpenditureService
    {
        IEnumerable<Expenditure> GetAll();
        Expenditure Get(int id);
        int Add(ExpenditureParam param);
        int Update(int id, ExpenditureParam param);
        int Delete(int id, int updatedBy, DateTime updatedDate);
    }
}
