using System.Collections.Generic;

namespace KW.Domain
{
    public interface IProjectRiskStatusRepository
    {
        IEnumerable<ProjectRiskStatus> GetByProjectId(int projectId);
        ProjectRiskStatus Get(int id);
        void Insert(ProjectRiskStatus model);
        void Update(ProjectRiskStatus model);
        void Delete(int id);
        IEnumerable<ProjectRiskStatus> GetByKodeMRisk(string kodeMRisk);
        IEnumerable<ProjectRiskStatus> GetAll();
    }
}
