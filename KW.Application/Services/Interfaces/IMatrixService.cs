using KW.Application.Params;
using KW.Domain;
using System;
using System.Collections.Generic;

namespace KW.Application
{
    public interface IMatrixService
    {
        IEnumerable<Matrix> GetAll();
        Matrix Get(int id);
        int Add(MatrixParam param);
        int Update(int id, MatrixParam param);
        int Delete(int id, int deleteBy, DateTime deleteDate);
    }
}
