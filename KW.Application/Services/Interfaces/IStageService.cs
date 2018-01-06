using KW.Application.Params;
using KW.Domain;
using System;
using System.Collections.Generic;

namespace KW.Application
{
    public interface IStageService
    {
        IEnumerable<Stage> GetAll();
        Stage Get(int id);
        int Add(StageParam param);
        int Update(int id, StageParam param);
        int Delete(int id, int deleteBy, DateTime deleteDate);
    }
}
