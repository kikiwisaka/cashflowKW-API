using System;
using System.Collections.Generic;
using KW.Application.Params;
using KW.Domain;

namespace KW.Application
{
    public interface ICorrelationMatrixService
    {
        IEnumerable<CorrelationMatrix> GetAll();
        CorrelationMatrix Get(int id);
        int Add(CorrelationMatrixParam param);
        int Update(int id, CorrelationMatrixParam param);
        int Delete(int id, int deleteBy, DateTime deleteDate);
    }
}
