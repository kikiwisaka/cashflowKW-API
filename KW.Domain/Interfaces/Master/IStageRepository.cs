using System;
using System.Collections.Generic;

namespace KW.Domain
{
    public interface IStageRepository
    {
        Stage Get(int id);
        IEnumerable<Stage> GetAll();
        void Insert(Stage model);
        void Update(Stage model);
        void Delete(int id, int deleteBy, DateTime deleteDate);
        bool IsExist(int id, string namaStage);
        bool IsExist(string namaStage);
    }
}
