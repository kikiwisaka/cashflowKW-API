using System;
using System.Collections.Generic;

namespace KW.Domain
{
    public interface IProjectRiskRegistrasiRepository
    {
        IEnumerable<ProjectRiskRegistrasi> GetAll();
        IEnumerable<ProjectRiskRegistrasi> GetByProjectId(int projectId);
        ProjectRiskRegistrasi Get(int id);
        void Insert(ProjectRiskRegistrasi model);
        void Insert(IList<ProjectRiskRegistrasi> collections);
        void DeleteByProjectId(int id);
        bool IsExist(int projectId, int riskRegistrasiId);
    }
}
