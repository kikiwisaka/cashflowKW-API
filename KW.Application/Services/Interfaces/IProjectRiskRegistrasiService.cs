using KW.Application.Params;
using KW.Domain;
using System;
using System.Collections.Generic;

namespace KW.Application
{
    public interface IProjectRiskRegistrasiService
    {
        IEnumerable<ProjectRiskRegistrasi> GetAll();
        IEnumerable<ProjectRiskRegistrasi> GetByProjectId(int projectId);
        ProjectRiskRegistrasi Get(int id);
        void Add(IList<ProjectRiskRegistrasiParam> collections);
        void Delete(int id, int deleteBy, DateTime deleteDate);
    }
}
