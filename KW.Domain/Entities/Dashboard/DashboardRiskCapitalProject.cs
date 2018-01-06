using KW.Core;

namespace KW.Domain
{
    public class DashboardRiskCapitalProject : Entity
    {
        public int Year { get; set; }
        public int Total { get; set; }
        public string ProjectName{ get; set; }

        public DashboardRiskCapitalProject() { }
    }

    public class DashboardUndiversifiedRiskCapitalProject : Entity
    {
        public int RiskRegistrasiId { get; set; }
        public int ScenarioId { get; set; }
        public int LikehoodId { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int Year { get; set; }
        public decimal? ValueUndiversified { get; set; }

        public DashboardUndiversifiedRiskCapitalProject() { }

        public DashboardUndiversifiedRiskCapitalProject(int scenarioId, int likehoodId, int riskRegistrasiId, int projectId, string projectName, int year, decimal? valueUndiversified)
        {
            this.ScenarioId = scenarioId;
            this.LikehoodId = likehoodId;
            this.RiskRegistrasiId = riskRegistrasiId;
            this.ProjectId = projectId;
            this.ProjectName = projectName;
            this.Year = year;
            this.ValueUndiversified = valueUndiversified;
        }
    }
}
