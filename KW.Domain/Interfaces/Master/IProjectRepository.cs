using System;
using System.Collections.Generic;

namespace KW.Domain
{
    public interface IProjectRepository
    {
        Project Get(int id);
        IEnumerable<Project> GetAll();
        void Insert(Project model);
        void Update(Project model);
        void Delete(int id, int? deleteBy, DateTime? deleteDate);
        bool IsExist(int id, string namaProject);
        bool IsExist(string namaProject);
    }
}
