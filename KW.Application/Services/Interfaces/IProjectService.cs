using KW.Application.Params;
using KW.Domain;
using System;
using System.Collections.Generic;

namespace KW.Application
{
    public interface IProjectService
    {
        IEnumerable<Project> GetAll();
        Project Get(int id);
        int Add(ProjectParam param);
        int Update(int id, ProjectParam param);
        int Delete(int id, int deleteBy, DateTime deleteDate);
    }
}
